using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineControl.Lib
{
    public enum MinerMode
    {
        AlwaysOff = 0,
        AlwaysOn = 1,
        DontControl = 2,
        Schedule = 3
    }

    public enum MinerState
    {
        Uninitialized,
        Running,
        DisabledByUser,
        DisabledBySchedule,
        DisabledByUnknownTemp,
        DisabledByOverheating,
        DisabledByUserActivity,
        DisabledUnknownError,
        DisabledClosing
    }

    public enum LogType
    {
        Info,
        Warning,
        Error,
        Input,
        Debug
    }

    public enum LogSource
    {
        Internal,
        GPUMiner,
        CPUMiner
    }

    public enum SysTrayIconTextMode
    {
        MinerActiveStatus = 0,
        GPUPowerStep = 1
    }

    public enum ScheduleAction
    {
        MinerOn,
        MinerOff
    }

    public enum MetricType
    {
        Number,
        Selection
    }

    /// <summary>
    /// Source of metric data to be queried
    /// </summary>
    public enum MetricSource
    {
        GPUMiner,
        CPUMiner,
        MineControl,
        SysTray
    }

    /// <summary>
    /// Method of querying metric data
    /// </summary>
    public enum MetricMethod
    {
        RegEx,
        UserValue,
        InternalValue
    }

    /// <summary>
    /// Method of calculating an average or rate
    /// </summary>
    public enum CalculationMethod
    {
        /// <summary>
        /// Lookahead: Counts time to the right of a data point at the Y value of the data point.
        /// Includes time between last data point and now in calculation.        
        /// </summary>
        Lookahead,
        /// <summary>
        /// Lookbehind: Counts time to the left of a data point at the Y value of the data point
        /// Does not include time between last data point and now in calculation.        
        /// </summary>
        Lookbehind
    }

    public enum ProfileStepMode
    {
        UserValues,
        AutoOptimize,
        External
    }

    public enum ProfileOptimizationStatus
    {
        NotStarted,
        InProgress,
        Complete
    }
}
