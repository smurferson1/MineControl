using MineControl.Lib.WinAPI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MineControl.Lib.Utils
{
    public static class ProcessUtils
    {
        public static Job Job { get; } = new Job();

        /// <summary>
        /// Refreshes process info and returns running status
        /// </summary>
        /// <param name="process"></param>
        /// <returns> True if _process is running as a sub-process of current process </returns>
        public static bool IsProcessRunningFromObject(Process process)
        {
            try
            {
                process.Refresh();

                // may raise an exception if process is not running from the process object
                return !Process.GetProcessById(process.Id).HasExited;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Returns true if 1+ instances of a process with _processName exist
        /// </summary>
        /// <param name="_processName"></param>
        /// <returns> True if at least 1 instance of _processName is running on the system </returns>
        public static bool IsProcessRunningByName(string _processName)
        {
            Process[] procname = Process.GetProcessesByName(_processName);
            return procname.Length > 0;
        }

        /// <summary>
        /// Kills all instances of processes with _processName
        /// </summary>
        /// <param name="_processName"></param>
        /// <returns> True if all instances were killed successfully, false if no instances were found </returns>
        public static bool KillProcessInstancesByName(string _processName)
        {
            // kill any external instances (not ours)
            Process[] procs = Process.GetProcessesByName(_processName);
            foreach (Process proc in procs)
            {
                proc.Kill();
            }
            return procs.Length > 0;
        }

        public static void LaunchProcess(Process process, string path, string name, string logName, ref bool isRunning, bool fullControl, ref string status,
            DataReceivedEventHandler dataReceivedEventHandler, ILog log)
        {            
            if (File.Exists(path) && (name.Trim() != string.Empty))
            {
                if (fullControl)
                {
                    // only mess with it if we're not already running it as a sub-process
                    if (!IsProcessRunningFromObject(process))
                    {
                        try
                        {
                            // kill any external instances (not ours)
                            if (KillProcessInstancesByName(name))
                            {
                                log?.Append($"{logName} app \"{name}\" external instance(s) killed");
                            }

                            // this avoids exceptions if an output read was erroneously left in place
                            try
                            {
                                process.CancelOutputRead();
                            }
                            catch
                            {
                                // will raise exceptions in most cases that should be ignored
                            }

                            // start our instance                            
                            process.StartInfo.FileName = path;
                            process.StartInfo.WorkingDirectory = Path.GetDirectoryName(path);
                            process.StartInfo.RedirectStandardOutput = true;
                            process.StartInfo.UseShellExecute = false;
                            process.StartInfo.CreateNoWindow = true;
                            process.StartInfo.Verb = "runas";
                            process.OutputDataReceived += dataReceivedEventHandler;
                            process.Start();
                            isRunning = true;
                            status = "Running";
                            process.BeginOutputReadLine();

                            // add to job so it will close if MC crashes
                            bool isCrashSafe = false;
                            try
                            {
                                isCrashSafe = Job.AddProcess(process.Id);
                            }
                            catch
                            {
                                // eat any failures here
                            }

                            log?.Append(
                                $"{logName} app \"{name}\" started at path \"{path}\" under full control {(isCrashSafe ? "with" : "WITHOUT")} crash safety",
                                isCrashSafe ? LogType.Info : LogType.Warning);
                        }
                        catch (Exception ex)
                        {
                            status = "Unknown (Error)";
                            log?.Append($"{logName} app \"{name}\" kill/start failed with the following exception: ({ex.GetType()}) {ex.Message}", LogType.Error);
                        }
                    }
                    else
                    {
                        status = "Running";
                    }
                }
                else
                {
                    if (!IsProcessRunningByName(name))
                    {
                        process.StartInfo.FileName = path;
                        process.StartInfo.UseShellExecute = true;
                        process.Start();
                        isRunning = true;
                        status = "Running";
                        log?.Append($"{logName} app \"{name}\" started at path \"{path}\"");
                    }
                    else if (status != "Running (ext)")
                    {
                        status = "Running (ext)";
                        log?.Append($"{logName} app \"{name}\" was already running");
                    }
                }
            }
            else
            {
                log?.Append($"{logName} app info is invalid, so it wasn't executed or verified", LogType.Error);
            }
        }

        public static void CloseProcess(Process process, string path, string name, string logName, ref bool isRunning, 
            ref string status, DataReceivedEventHandler dataReceivedEventHandler, ILog log)
        {
            if (IsProcessRunningFromObject(process))
            {
                try
                {
                    if (process.StartInfo.RedirectStandardOutput)
                    {
                        process.CancelOutputRead();
                        process.OutputDataReceived -= dataReceivedEventHandler;
                    }
                    process.Kill();
                    log?.Append($"{logName} app \"{Path.GetFileNameWithoutExtension(path)}\" killed");
                }
                catch (Exception ex)
                {
                    log?.Append($"{logName} app \"{name}\" kill may have failed due to exception: {ex.GetType()} - {ex.Message}");
                }
            }

            try
            {
                // in case process was a batch file, kill the process by name as well
                if (KillProcessInstancesByName(name))
                {
                    log?.Append($"{logName} app \"{name}\" extra instance(s) killed");
                }
            }
            catch (Exception ex)
            {
                log?.Append($"{logName} app \"{name}\" extra instance kill may have failed due to exception: {ex.GetType()} - {ex.Message}");
            }

            isRunning = false;
            status = "Stopped";
        }
        
        public static void OpenLinkInDefaultBrowser(string link)
        {
            using (Process p = new Process())
            {
                p.StartInfo.UseShellExecute = true;
                p.StartInfo.FileName = link;
                p.Start();
            }
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern bool TerminateProcess(IntPtr hProcess, uint uExitCode);
    }
}
