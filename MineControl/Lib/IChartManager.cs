using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace MineControl.Lib
{
    public interface IChartManager
    {
        public void CreateChartSeriesForMetric(string chartName, Metric metricToBeCharted, SeriesChartType seriesChartType, AxisType yAxisType);
    }
}
