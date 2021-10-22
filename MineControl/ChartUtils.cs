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
        /// <summary>
        /// Get min and max Y value for the given chart and axis. 0 is always the floor.
        /// </summary>
        /// <param name="padding">How much padding will be added to min and max values. Does NOT come into play with a scale restriction</param>
        /// <param name="scaleRestrictions">Optional floor for the min value of a specific series on the chart.</param>
        /// <returns></returns>
        public static (double, double) GetMinAndMaxYValue(Chart chart, AxisType axisType, int padding, (Series series, int minFloor)[] scaleRestrictions = null)
        {
            // TODO: make this work when visible points have a different min and max than the full set
            double min = double.NaN;
            double max = double.NaN;
            double seriesMinFloor = 0;
            foreach (Series series in chart.Series)
            {
                if (series.Points.Count > 0 && series.YAxisType == axisType)
                {
                    if (scaleRestrictions != null && scaleRestrictions.Any(x => x.series == series))
                    {
                        seriesMinFloor = scaleRestrictions.Last(x => x.series == series).minFloor;
                    }
                    else
                    {
                        seriesMinFloor = 0;
                    }
                    min = min.Equals(double.NaN) ? Math.Max(GetMinYValue(series) - padding, seriesMinFloor) : Math.Min(min, Math.Max(GetMinYValue(series) - padding, seriesMinFloor));
                    max = max.Equals(double.NaN) ? GetMaxYValue(series) + padding : Math.Max(max, GetMaxYValue(series) + padding);
                }
            }

            // avoid invalid conditions resulting from scale restrictions
            if (min > max)
            {
                if (max >= padding + 1)
                {
                    min = max - 1 - padding;
                }
                else
                {
                    max = min + 1 + padding;
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
        public static void UpdateChartAxisScale(Chart chart, AxisType? axisType = null, (Series series, int minFloor)[] scaleRestrictions = null)
        {
            double min;
            double max;
            if (axisType == AxisType.Primary || axisType == null)
            {
                (min, max) = GetMinAndMaxYValue(chart, AxisType.Primary, 2, scaleRestrictions);
                SetChartAxisScale(chart.ChartAreas[0].AxisY, min, max, chart.Height);
            }
            if (axisType == AxisType.Secondary || axisType == null)
            {
                (min, max) = GetMinAndMaxYValue(chart, AxisType.Secondary, 2, scaleRestrictions);
                SetChartAxisScale(chart.ChartAreas[0].AxisY2, min, max, chart.Height);
            }
        }
        
        public static void SetChartAxisScale(Axis chartAxis, double min, double max, double height)
        {
            if (!min.Equals(double.NaN) && !max.Equals(double.NaN))
            {
                chartAxis.Minimum = Math.Round(Math.Max(min, 0), 0);
                chartAxis.Maximum = Math.Round(Math.Max(max, 0), 0);
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
