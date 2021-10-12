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
        public void CreateChartSeries(string chartName, Metric metricNumber, SeriesChartType seriesChartType, AxisType yAxisType);
    }
}
