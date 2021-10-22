using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace MineControl
{
    public static class ChartUtils
    {
        public static (double, double) GetMinAndMaxYValue(Chart chart, AxisType axisType)
        {
            // update resource axis to fit current data            
            double min = double.NaN;
            double max = double.NaN;
            foreach (Series series in chart.Series)
            {
                if (series.Points.Count > 0 && series.YAxisType == axisType)
                {
                    min = min.Equals(double.NaN) ? GetMinYValue(series) : Math.Min(min, GetMinYValue(series));
                    max = max.Equals(double.NaN) ? GetMaxYValue(series) : Math.Max(max, GetMaxYValue(series));
                }
            }
            return (min, max);
        }       

        public static double GetMinYValue(Series series)
        {
            using (DataPoint point = series.Points.FindMinByValue())
            {
                return point == null ? double.NaN : point.YValues[0];
            }
        }

        public static double GetMaxYValue(Series series)
        {
            using (DataPoint point = series.Points.FindMaxByValue())
            {
                return point == null ? double.NaN : point.YValues[0];
            }
        }

        /// <summary>
        /// Update chart axis scale to fit the data on that axis. 
        /// </summary>
        /// <param name="chart">Chart containing the axis.</param>
        /// <param name="axisType">Represents axis to scale. When null, both axes are scaled.</param>
        public static void UpdateChartAxisScale(Chart chart, AxisType? axisType = null)
        {
            double min;
            double max;
            if (axisType == AxisType.Primary || axisType == null)
            {
                (min, max) = GetMinAndMaxYValue(chart, AxisType.Primary);
                SetChartAxisScale(chart.ChartAreas[0].AxisY, min, max, chart.Height, 2);
            }
            if (axisType == AxisType.Secondary || axisType == null)
            {
                (min, max) = GetMinAndMaxYValue(chart, AxisType.Secondary);
                SetChartAxisScale(chart.ChartAreas[0].AxisY2, min, max, chart.Height, 2);
            }
        }
        
        public static void SetChartAxisScale(Axis chartAxis, double min, double max, double height, double padding)
        {
            if (!min.Equals(double.NaN) && !max.Equals(double.NaN))
            {
                chartAxis.Minimum = Math.Round(Math.Max(min - padding, 0), 0);
                chartAxis.Maximum = Math.Round(Math.Max(max + padding, 0), 0);
                chartAxis.Interval = Math.Max(Math.Round((chartAxis.Maximum - chartAxis.Minimum) / (height / 15)), 1);
            }
            else
            {
                // dummy values
                chartAxis.Minimum = 0;
                chartAxis.Maximum = 1;
                chartAxis.Interval = 1;
            }
        }
    }
}
