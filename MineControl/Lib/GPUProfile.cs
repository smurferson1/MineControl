using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MineControl.Lib
{
    public class GPUProfile
    {
        [JsonIgnore]
        public bool IsVisible { get; set; } = true;

        public int PowerPercent { get; set; } = 100;

        public int UserCoreOffset { get; set; } = 0;

        public int OptimizedCoreOffset { get; set; } = 0;

        public int UserMemoryOffset { get; set; } = 0;

        public int OptimizedMemoryOffset { get; set; } = 0;

        public ProfileOptimizationStatus OptimizationStatus { get; set; } = ProfileOptimizationStatus.NotStarted;

        public DateTime LastOptimized { get; set; } = DateTime.MinValue;

        public double MaxRecordedUserHashRate { get; set; } = double.NaN;

        public double MaxRecordedOptimizedHashRate { get; set; } = double.NaN;  
    }
}
