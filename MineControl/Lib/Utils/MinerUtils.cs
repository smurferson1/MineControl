using MineControl.Lib.WinAPI;
using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace MineControl.Lib.Utils
{
    [SupportedOSPlatform("windows")]
    public static class MinerUtils
    {
        private static readonly Properties.Settings Settings = Properties.Settings.Default;
        private static IActiveSchedules SelectedSchedules { get; set; }
        private static ILog Log { get; set; }
        private static DataReceivedEventHandler ReceivedProcessData { get; set; }
        private static DateTime GPUOverheatStartTime { get; set; } = DateTime.MinValue;
        private static DateTime GPUOverheatShutoffTime { get; set; } = DateTime.MinValue;
        public static MinerState GPUState { get; set; } = MinerState.Uninitialized;
        public static MinerState CPUState { get; set; } = MinerState.Uninitialized;
        public static Process ProcessGPUMiner { get; } = new Process();
        private static bool isGPUMinerRunning = false;
        public static Process ProcessCPUMiner { get; } = new Process();
        private static bool isCPUMinerRunning = false;

        // TODO: extra bad UI coupling
        private static NotifyIcon StatusIcon { get; set; }
        private static DataGridViewCell GPUStatusCell { get; set; }
        private static DataGridViewCell CPUStatusCell { get; set; }

        public static void Setup(IActiveSchedules selectedSchedules, ILog log, NotifyIcon statusIcon, DataGridViewCell gpuStatusCell, 
            DataGridViewCell cpuStatusCell, DataReceivedEventHandler receivedProcessData)
        {
            SelectedSchedules = selectedSchedules;
            Log = log;
            StatusIcon = statusIcon;
            GPUStatusCell = gpuStatusCell;
            CPUStatusCell = cpuStatusCell;
            ReceivedProcessData = receivedProcessData;
        }

        public static void DisposeChildren()
        {
            ProcessGPUMiner.Dispose();
            ProcessCPUMiner.Dispose();
        }

        public static void UpdateMinerState(bool isGPU, string gpuTempStr = "")
        {
            MinerMode minerMode = isGPU ? (MinerMode)Settings.minerGPUMode : (MinerMode)Settings.minerCPUMode;
            if (Settings.controlRunning && Settings.minerEnableAutomation)
            {
                // find state based on the miner mode
                // Note: just gathering the state info and not applying it yet. we need to account for it in the conditions after before deciding our final state.                
                MinerState minerState = MinerState.Uninitialized;
                string reasonToLog = "";
                switch (minerMode)
                {
                    case MinerMode.AlwaysOff:
                        minerState = MinerState.DisabledByUser;
                        break;
                    case MinerMode.AlwaysOn:
                        minerState = MinerState.Running;
                        break;
                    case MinerMode.DontControl:
                        // do nothing                       
                        break;
                    case MinerMode.Schedule:
                        if (isGPU)
                        {
                            if ((SelectedSchedules.GPU != null) && SelectedSchedules.GPU.Evaluate().Count > 0)
                            {
                                minerState = SelectedSchedules.GPU.LastEvaluatedActions.Last() == ScheduleAction.MinerOn ? MinerState.Running : MinerState.DisabledBySchedule;
                                reasonToLog = "as per its schedule";
                            }
                            else
                            {
                                minerState = MinerState.DisabledUnknownError;
                                reasonToLog = "due to missing or incomplete schedule";
                            }
                        }
                        else
                        {
                            if ((SelectedSchedules.CPU != null) && SelectedSchedules.CPU.Evaluate().Count > 0)
                            {
                                minerState = SelectedSchedules.CPU.LastEvaluatedActions.Last() == ScheduleAction.MinerOn ? MinerState.Running : MinerState.DisabledBySchedule;
                                reasonToLog = "as per its schedule";
                            }
                            else
                            {
                                minerState = MinerState.DisabledUnknownError;
                                reasonToLog = "due to missing or incomplete schedule";
                            }
                        }
                        break;
                    default:
                        minerState = MinerState.DisabledUnknownError;
                        reasonToLog = "due to unknown state";
                        break;
                }

                // GPU failsafes
                if (isGPU)
                {
                    // note: check overheating FIRST every time, since it's complex and it manages variables
                    if (Settings.tempStopWhenOverheat)
                    {
                        DateTime now = DateTime.Now;
                        if (GPUOverheatShutoffTime != DateTime.MinValue)
                        {
                            // we're already shut off due to overheating
                            if (now > GPUOverheatShutoffTime.AddSeconds(Settings.tempStopWhenOverheatSecs))
                            {
                                // overheating wait time exceeded, reset it
                                GPUOverheatShutoffTime = DateTime.MinValue;
                            }
                            else
                            {
                                // inside overheating wait time, ensure we're shut off
                                if (minerState == MinerState.Running)
                                {
                                    SyncMinerState(isGPU, MinerState.DisabledByOverheating, "due to overheating");
                                    return;
                                }
                            }
                        }
                        else
                        {
                            if (!(new[] { Const.UnknownTemp, string.Empty }).Contains(gpuTempStr))
                            {
                                if ((gpuTempStr != Const.BlankInfo) && (Convert.ToInt32(gpuTempStr) > Settings.tempMax) && (Settings.tempSpeedStep == 1))
                                {
                                    // we're overheated: temp is over the max and we're at lowest power
                                    if (GPUOverheatStartTime == DateTime.MinValue)
                                    {
                                        // we weren't overheated before, so record the start time
                                        GPUOverheatStartTime = now;
                                    }
                                    else if (now > GPUOverheatStartTime.AddSeconds(Settings.tempStopWhenOverheatThresholdSecs))
                                    {
                                        // we were overheated before, and we've exceeded the threshold for shutoff
                                        // reset the threshold time and start the shutoff timer
                                        GPUOverheatStartTime = DateTime.MinValue;
                                        GPUOverheatShutoffTime = now;
                                        // inside overheating wait time, ensure we're shut off
                                        if (minerState == MinerState.Running)
                                        {
                                            SyncMinerState(isGPU, MinerState.DisabledByOverheating, "due to overheating");
                                        }
                                        return;
                                    }
                                }
                                else
                                {
                                    // we're not overheated, so reset the start time
                                    GPUOverheatStartTime = DateTime.MinValue;
                                }
                            }
                        }
                    }

                    if (Settings.tempStopWhenTempUnknown && (gpuTempStr == Const.UnknownTemp) && (minerState == MinerState.Running))
                    {
                        // inside overheating wait time, ensure we're shut off                        
                        SyncMinerState(isGPU, MinerState.DisabledByUnknownTemp, "due to unknown temp");
                        return;
                    }
                }

                // user activity -- last in priority, so we only disable if nobody else did
                if (isGPU && Settings.minerGPUUserActivityShutoff && (minerState == MinerState.Running) && (LastUserInput.GetLastInputTimeInSecs() < (Settings.minerUserActivityTimeoutMins * 60)))
                {
                    SyncMinerState(isGPU, MinerState.DisabledByUserActivity, "due to user activity");
                    return;
                }
                if (!isGPU && Settings.minerCPUUserActivityShutoff && (minerState == MinerState.Running) && (LastUserInput.GetLastInputTimeInSecs() < (Settings.minerUserActivityTimeoutMins * 60)))
                {
                    SyncMinerState(isGPU, MinerState.DisabledByUserActivity, "due to user activity");
                    return;
                }

                // default to normal miner mode
                if (minerState != MinerState.Uninitialized)
                    SyncMinerState(isGPU, minerState, reasonToLog);
            }
            else
            {
                if (minerMode != MinerMode.DontControl)
                {
                    SyncMinerState(isGPU, MinerState.DisabledByUser, "");
                }
            }
        }

        /// <summary>
        /// Synchronizes miner state with input values. ONLY does work if the input values would change miner state.
        /// </summary>
        /// <param name="doOnGPU">Act on GPU miner if true, CPU miner if false</param>
        /// <param name="newMinerState">Desired state of the miner</param>
        /// <param name="reasonToLog">Explanation for the state change to be logged. If blank, no reason will be logged</param>
        [SupportedOSPlatform("windows")]
        public static void SyncMinerState(bool doOnGPU, MinerState newMinerState, string reasonToLog)
        {
            bool runMiner = newMinerState == MinerState.Running;
            if (doOnGPU)
            {
                GPUState = newMinerState;
                if (runMiner && !ProcessUtils.IsProcessRunningFromObject(ProcessGPUMiner))
                {
                    if (reasonToLog.Length > 0)
                    {
                        Log.Append($"GPU Miner launching {reasonToLog}");
                    }

                    string status = (string)GPUStatusCell.Value;
                    ProcessUtils.LaunchProcess(ProcessGPUMiner, Settings.appGPUMinerPath, Settings.appGPUMinerName,
                        Const.GPUMiner, ref isGPUMinerRunning, true, ref status, ReceivedProcessData, Log);
                    GPUStatusCell.Value = status;
                }
                else if (!runMiner && ProcessUtils.IsProcessRunningByName(Settings.appGPUMinerName))
                {
                    if (reasonToLog.Length > 0)
                    {
                        Log.Append($"GPU Miner closing {reasonToLog}");
                    }

                    string status = (string)GPUStatusCell.Value;
                    ProcessUtils.CloseProcess(ProcessGPUMiner, Settings.appGPUMinerPath, Settings.appGPUMinerName, Const.GPUMiner, ref isGPUMinerRunning,
                        ref status, ReceivedProcessData, Log);
                    GPUStatusCell.Value = status;
                }
            }
            else
            {
                CPUState = newMinerState;
                if (runMiner && !ProcessUtils.IsProcessRunningFromObject(ProcessCPUMiner))
                {
                    if (reasonToLog.Length > 0)
                    {
                        Log.Append($"CPU Miner launching {reasonToLog}");
                    }

                    string status = (string)CPUStatusCell.Value;
                    ProcessUtils.LaunchProcess(ProcessCPUMiner, Settings.appCPUMinerPath, Settings.appCPUMinerName,
                        Const.CPUMiner, ref isCPUMinerRunning, true, ref status, ReceivedProcessData, Log);
                    CPUStatusCell.Value = status;
                }
                else if (!runMiner && ProcessUtils.IsProcessRunningByName(Settings.appCPUMinerName))
                {
                    if (reasonToLog.Length > 0)
                    {
                        Log.Append($"CPU Miner closing {reasonToLog}");
                    }

                    string status = (string)CPUStatusCell.Value;
                    ProcessUtils.CloseProcess(ProcessCPUMiner, Settings.appCPUMinerPath, Settings.appCPUMinerName, Const.CPUMiner, ref isCPUMinerRunning,
                        ref status, ReceivedProcessData, Log);
                    CPUStatusCell.Value = status;
                }
            }
            SysTrayIcon.UpdateTextIconFromSettings(StatusIcon);
        }
    }
}
