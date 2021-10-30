using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace MineControl.Lib.Utils
{
    /// <summary>
    /// Charting-related utilities.
    /// Note: charts are considered non-essential, so most exceptions aren't raised.
    /// </summary>
    public static class ChartUtils
    {
        private static ILog Log { get; set; } = null;

        public static void Setup(ILog log)
        {
            Log = log;
        }

        /// <summary>
        /// Get min and max Y value for the given chart and axis. 0 is always the floor.
        /// </summary>
        /// <param name="padding">How much padding will be added to min and max values. Does NOT come into play with a scale restriction</param>
        /// <param name="scaleRestrictions">Optional floor for the min value of a specific series on the chart.</param>
        /// <returns></returns>
        public static (double, double) GetMinAndMaxYValue(Chart chart, AxisType axisType, int padding, (Series series, int minFloor)[] scaleRestrictions = null)
        {
            double min = double.NaN;
            double max = double.NaN;
            double seriesMinFloor = 0;
            try
            {
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
                        min = min.Equals(double.NaN) ?
                            Math.Max(GetMinYValue(series, chart.ChartAreas[0].AxisX.Minimum) - padding, seriesMinFloor) :
                            Math.Min(min, Math.Max(GetMinYValue(series, chart.ChartAreas[0].AxisX.Minimum) - padding, seriesMinFloor));
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
            }
            catch (Exception ex)
            {
                Log?.Append($"Exception in ChartUtils.GetMinAndMaxYValue: {ex.GetType()} - {ex.Message}", LogType.Warning);
            }

            return (min, max);
        }

        /// <summary>
        /// Gets smallest Y value from the series.
        /// </summary>
        /// <param name="series">Series to find minimum Y value for</param>
        /// <param name="minXValue">Constrains search by a minimum X value</param>
        /// <returns>The min value, or double.NaN if no valid points were found.</returns>
        public static double GetMinYValue(Series series, double minXValue = 0)
        {
            DataPoint point;
            try
            {
                point = series.Points.FindMinByValue("Y1", series.Points.IndexOf(series.Points.First(x => x.XValue >= minXValue)));
            }
            catch
            {
                point = null;
            }

            return point == null ? double.NaN : point.YValues[0];
        }

        public static double GetMaxYValue(Series series)
        {
            DataPoint point;
            try
            {
                point = series.Points.FindMaxByValue();
            }
            catch
            {
                point = null;
            }

            return point == null ? double.NaN : point.YValues[0];
        }

        /// <summary>
        /// Update chart axis scale to fit the data on that axis. 
        /// </summary>
        /// <param name="chart">Chart containing the axis.</param>
        /// <param name="axisType">Represents axis to scale. When null, both axes are scaled.</param>
        public static void UpdateChartYAxisScale(Chart chart, AxisType? axisType = null, (Series series, int minFloor)[] scaleRestrictions = null)
        {
            try
            {
                double min;
                double max;
                if (axisType == AxisType.Primary || axisType == null)
                {
                    (min, max) = GetMinAndMaxYValue(chart, AxisType.Primary, 2, scaleRestrictions);
                    SetChartYAxisScale(chart.ChartAreas[0].AxisY, min, max, chart.Height);
                }
                if (axisType == AxisType.Secondary || axisType == null)
                {
                    (min, max) = GetMinAndMaxYValue(chart, AxisType.Secondary, 2, scaleRestrictions);
                    SetChartYAxisScale(chart.ChartAreas[0].AxisY2, min, max, chart.Height);
                }
            }
            catch (Exception ex)
            {
                Log?.Append($"Exception in ChartUtils.UpdateChartYAxisScale: {ex.GetType()} - {ex.Message}", LogType.Warning);
            }
        }

        public static void SetChartYAxisScale(Axis chartAxis, double min, double max, double height)
        {
            try
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
            catch (Exception ex)
            {
                Log?.Append($"Exception in ChartUtils.SetChartYAxisScale: {ex.GetType()} - {ex.Message}", LogType.Warning);
            }
        }

        /// <summary>
        /// Checks for data points from the exact x and y coordinates, moving out to maxDistance.
        /// </summary>
        /// <param name="chart"></param>
        /// <param name="maxDistance"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>HitTestResult for closest DataPoint to x and y within maxDistance, or null if not found</returns>
        public static HitTestResult NearHitTest(Chart chart, int maxDistance, int x, int y)
        {
            HitTestResult hitTestResult;
            try
            {
                for (int distance = 0; distance <= maxDistance; ++distance)
                {
                    for (int curX = x - distance; curX <= x + distance; ++curX)
                    {
                        for (int curY = y - distance; curY <= y + distance; ++curY)
                        {
                            hitTestResult = chart.HitTest(curX, curY);
                            if (hitTestResult.Object is DataPoint)
                            {
                                return hitTestResult;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log?.Append($"Exception in ChartUtils.NearHitTest: {ex.GetType()} - {ex.Message}", LogType.Warning);
            }
            return null;
        }

        /// <summary>
        /// For a chart series with time on X axis, finds total area and total time with the given calculation method
        /// </summary>
        /// <param name="series"></param>
        /// <param name="calculationMethod"></param>
        /// <returns></returns>
        public static (double, double) CalculateAreaAndTotalTime(Series series, CalculationMethod calculationMethod)
        {             
            if (series == null || !series.Points.Any())
            {
                return (double.NaN, double.NaN);
            }
                        
            DataPoint previousPoint = null;
            double totalArea = 0.0;
            double totalTime = 0.0;
            double lastTime;
            try
            {
                foreach (DataPoint point in series.Points)
                {
                    if (previousPoint != null)
                    {
                        lastTime = (DateTime.FromOADate(point.XValue) - DateTime.FromOADate(previousPoint.XValue)).TotalSeconds;
                        totalTime += lastTime;
                        totalArea += (calculationMethod == CalculationMethod.Lookahead ? previousPoint.YValues[0] : point.YValues[0]) * lastTime;
                    }
                    previousPoint = point;
                }

                // include area after last point if we're using the lookahead method            
                DataPoint lastP = series.Points.Last();
                if (calculationMethod == CalculationMethod.Lookahead && !double.IsNaN(lastP.YValues[0]))
                {
                    lastTime = (DateTime.Now - DateTime.FromOADate(lastP.XValue)).TotalSeconds;
                    totalTime += lastTime;
                    totalArea += lastP.YValues[0] * lastTime;
                }
            }
            catch (Exception ex)
            {
                Log?.Append($"Exception in ChartUtils.CalculateAreaAndTotalTime: {ex.GetType()} - {ex.Message}", LogType.Warning);
            }

            return (totalArea, totalTime);
        }

        public static double CalculateAverage(Series series, CalculationMethod calculationMethod)
        {
            double totalArea;
            double totalTime;
            (totalArea, totalTime) = CalculateAreaAndTotalTime(series, calculationMethod);

            // calculate average as (previous total area + total area since last data point [if doing Lookahead]) / total elapsed time            
            return Math.Round(totalArea / totalTime, 2);
        }

        public static object CalculateRate(Series series, double denominator, CalculationMethod calculationMethod)
        {
            double totalArea;
            (totalArea, _) = CalculateAreaAndTotalTime(series, calculationMethod);

            // calculate rate as (previous total area + total area since last data point [if doing Lookahead]) / denominator
            return Math.Round(totalArea / denominator, 3);
        }

        public static double GetMinXValue(Chart chart)
        {
            double min = double.NaN;
            try
            {
                foreach (Series series in chart.Series)
                {
                    if (series.Points.Count > 0)
                    {
                        min = double.IsNaN(min) ? series.Points.FindMinByValue("X").XValue : Math.Min(min, series.Points.FindMinByValue("X").XValue);
                    }
                }
            }
            catch (Exception ex)
            {
                Log?.Append($"Exception in ChartUtils.GetMinXValue: {ex.GetType()} - {ex.Message}", LogType.Warning);
            }
            return min;
        }
    }
}
