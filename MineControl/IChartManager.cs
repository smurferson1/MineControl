using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace MineControl
{
    public interface IChartManager
    {
        public void CreateChartSeriesForMetric(string chartName, Metric metric, SeriesChartType seriesChartType, AxisType yAxisType);
    }
}
