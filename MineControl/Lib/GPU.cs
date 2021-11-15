using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace MineControl.Lib
{
    public class GPU
    {
        public BindingList<GPUProfile> Profiles { get; set; } = new();

        public ProfileStepMode StepMode { get; set; } = ProfileStepMode.External;        

        public int MinPowerPercent { get; set; } = 70;

        public int MaxPowerPercent { get; set; } = 100;

        public int MinCoreOffset { get; set; } = -500;

        public int MaxCoreOffset { get; set; } = 1000;

        public int MinMemoryOffset { get; set; } = -500;

        public int MaxMemoryOffset { get; set; } = 1500;

        public int CoreIncrement { get; set; } = 5;

        public int MemoryIncrement { get; set; } = 10;

        public int FanSpeedPercent { get; set; } = 70;

        public string ConfigIniPath { get; set; } = "";

        public string ConfigIniSection { get; set; } = "";

        public string ConfigIniContentTemplate { get; set; } = "";

        [JsonIgnore]
        public Chart Chart { get; set; } = null;

        public GPU()
        {
            for (int i = MinPowerPercent; i <= MaxPowerPercent; ++i)
            {
                Profiles.Add(new());
                Profiles.Last().PowerPercent = i;
            }
        }
    }
}
