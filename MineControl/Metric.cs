using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace MineControl
{
    /// <summary>
    /// Represents a single data metric including its configuration and most recent retrieved result
    /// </summary>
    public class Metric
    {
        public Metric() : this(false, "", MetricType.Number, MetricSource.SysTray, MetricMethod.RegEx, "") { }
        
        public Metric(bool isEnabled, string name, MetricType type, MetricSource source, MetricMethod method, string query)
        { 
            IsEnabled = isEnabled; 
            Name = name; 
            Type = type; 
            Source = source; 
            Method = method;             
            Query = query;            
        }

        /// <summary>
        /// Whether this metric is being tracked
        /// </summary>
        private bool isEnabled;
        public bool IsEnabled 
        {
            get { return isEnabled; }
            set
            {
                isEnabled = value;
                if (Series != null)
                {
                    Series.Enabled = isEnabled;
                }
            } 
        }

        /// <summary>
        /// Display name of the metric
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The metric type (numeric or string-based selection like a mining algo)
        /// </summary>
        public MetricType Type { get; set; }

        /// <summary>
        /// Where the metric is read from
        /// </summary>
        private MetricSource source;
        public MetricSource Source 
        { 
            get { return source; }
            set
            {
                source = value;
                if (source == MetricSource.MineControl && Method == MetricMethod.RegEx)
                {
                    // regex is invalid if we're the source, so we update the method based on the metric state
                    Method = IsInternal ? MetricMethod.InternalValue : MetricMethod.UserValue;                    
                }
                else if (source != MetricSource.MineControl && Method != MetricMethod.RegEx)
                {
                    // the only valid method for an external source
                    Method = MetricMethod.RegEx;
                }               
            }
        }

        /// <summary>
        /// How the value is queried (i.e. search approach)
        /// </summary>
        private MetricMethod method;
        public MetricMethod Method 
        { 
            get { return method; }
            set
            {
                method = value;
                if (method != MetricMethod.RegEx)
                {
                    // the only valid source for non-regex methods
                    Source = MetricSource.MineControl;
                }
                else if (Source == MetricSource.MineControl)
                {
                    Source = MetricSource.SysTray;
                }
            } 
        }
                
        /// <summary>
        /// The query string that is applied using the Method, for example a regex
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// A "parent" metric that we use to group this one, for example grouping by a metric that retrieves the mining algorithm
        /// </summary>   
        private Metric groupedBy;
        [JsonIgnore]
        public Metric GroupedBy
        {
            get { return groupedBy; }
            set
            {
                if (groupedBy != null)
                {
                    groupedBy.ResultChanged -= ChildResultChanged;
                }
                groupedBy = value;
                if (groupedBy != null)
                {
                    UpdateSeries();
                    groupedBy.ResultChanged += ChildResultChanged;
                }
            }
        }

        /// <summary>
        /// The metric providing the unit for this metric, if applicable
        /// </summary>        
        private Metric unit = null;
        [JsonIgnore]
        public Metric Unit 
        {
            get { return unit; }
            set 
            { 
                if (unit != null)
                {
                    unit.ResultChanged -= ChildResultChanged;
                }
                unit = value; 
                if (unit != null)
                {
                    UpdateSeries();
                    unit.ResultChanged += ChildResultChanged;
                }
            } 
        }

        /// <summary>
        /// Triggers after result has changed
        /// </summary>        
        public event EventHandler ResultChanged;

        private void ChildResultChanged(object sender, EventArgs e)
        {
            UpdateSeries();
        }

        /// <summary>
        /// True if metric is internally retrieved and non-customizable. Ignored by serializer since it's always set internally.
        /// </summary>
        [JsonIgnore]
        public bool IsInternal { get; set; } = false;

        /// <summary>
        /// Reference to the data series, if present
        /// </summary>
        [JsonIgnore]
        public Chart Chart { get; set; } = null;

        /// <summary>
        /// Reference to the data series, if present
        /// </summary>
        private Series series = null;
        [JsonIgnore]
        public Series Series 
        {
            get { return series; }
            set
            { 
                if (series != value)
                {
                    series = value;
                    UpdateSeries();
                }
            } 
        }

        /// <summary>
        /// For storing and retrieving the last selection result for this metric
        /// Note: for user values, Query represents the user value, thus Query is returned
        /// </summary>        
        private string selectionResult = "";
        [JsonIgnore]
        public string SelectionResult 
        { 
            get { return Method == MetricMethod.UserValue ? Query : selectionResult; }            
            private set 
            {
                if (selectionResult != value)
                {
                    selectionResult = value;
                    ResultChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// For storing and retrieving the last numeric result for this metric
        /// Note: for user values, Query represents the user value, thus Query is returned
        /// </summary>
        private double numericResult = double.NaN;
        [JsonIgnore]
        public double NumericResult
        {
            get { return Method == MetricMethod.UserValue ? Convert.ToDouble(Query) : numericResult; }
            private set
            {
                if (numericResult != value)
                {
                    numericResult = value;
                    ResultChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        [JsonIgnore]
        public DateTime LastResultTime { get; private set; } = DateTime.MinValue;

        [JsonIgnore]
        public IChartManager ChartManager = null;

        /// <summary>
        /// Assigns properties of given metric to this metric
        /// </summary>
        /// <param name="metric"></param>
        /// <param name="includeJsonIgnores"></param>
        public void Assign(Metric metric, bool includeJsonIgnores = false)
        {
            IsEnabled = metric.IsEnabled;
            Name = metric.Name;
            Type = metric.Type;
            Source = metric.Source;
            Method = metric.Method;            
            Query = metric.Query;
            if (includeJsonIgnores)
            {
                IsInternal = metric.IsInternal;
                Chart = metric.Chart;
                Series = metric.Series;
                SelectionResult = metric.SelectionResult;
                NumericResult = metric.NumericResult;
                LastResultTime = metric.LastResultTime;
                GroupedBy = metric.GroupedBy;
                Unit = metric.Unit;
            }
            UpdateSeries();
        }

        /// <summary>
        /// Updates series info and adds/switches series if needed based on grouping.
        /// Assumes that Chart and Series are not null.
        /// </summary>
        private void UpdateSeries()
        {
            if (Series == null)
            {
                return;
            }

            string unit = Unit == null || Unit.SelectionResult.Length == 0 ? "" : $" ({Unit.SelectionResult})";
            string grouping = GroupedBy == null || GroupedBy.SelectionResult.Length == 0 ? "" : $" w/ {GroupedBy.SelectionResult}";
            
            // handle grouping if series name has been set
            if (GroupedBy != null && Series.Name.Length > 1)
            {
                // check grouping metric in case we need to change to a different series
                if (!Series.Name.Contains(grouping) && Series.Name.Contains("/w"))
                {
                    bool found = false; 

                    // first look for another series on the same chart that fits this metric and grouping
                    foreach (Series series in Chart.Series)
                    {
                        if (!found && series.Name.Contains(Name) && series.Name.Contains(grouping))
                        {
                            Series = series;
                            found = true;
                        }
                    }

                    // if not found, create a new series
                    if (!found)
                    {
                        ChartManager.CreateChartSeries(Chart.Name, this, Series.ChartType, Series.YAxisType);
                    }
                }
                Series.Name = $"{this.Name}{unit}{grouping}";
            }
            
            // set info
            Series.Name = $"{this.Name}{unit}{grouping}";
            Series.Enabled = IsEnabled && Series.Points.Count > 0;       
        }

        /// <summary>
        /// Updates the metric from the given string input. Raises exception if multiple matches found.
        /// Disabled metrics are not charted.
        /// </summary>
        /// <param name="input">string input (note: does NOT check for correct input source)</param>
        /// <param name="chartChangesOnly"></param>
        /// <returns>true if metric was updated, false otherwise</returns>
        public bool UpdateFromInput(string input, bool chartChangesOnly = true, bool chartMissingValues = true)
        {
            try
            {
                if (!IsEnabled && !IsInternal)
                {
                    return false;
                }
                
                // TODO: simplify this code -- much redundancy
                bool result = false;

                if (Method == MetricMethod.RegEx)
                {
                    if (Query.Trim() != string.Empty)
                    {
                        // handle as a regex
                        Regex rx = new Regex(Query, RegexOptions.Compiled);

                        MatchCollection matches = rx.Matches(input);

                        if (matches.Count > 1)
                        {
                            throw new Exception($"Multiple {Name} matches found in the same input, so none were kept");
                        }

                        if (Type == MetricType.Number)
                        {
                            // handle as double
                            foreach (Match match in matches)
                            {
                                double matchResult = Convert.ToDouble(match.Value);
                                if (matchResult > 0.0)
                                {
                                    result = true;
                                    DateTime now = DateTime.Now;
                                    LastResultTime = now;
                                    NumericResult = matchResult;
                                    
                                    if (Series != null && IsEnabled)
                                    {                                        
                                        if (!chartChangesOnly || (Series.Points.Count == 0) || (Series.Points.Last().YValues[0] != matchResult))
                                        {
                                            Series.Points.AddXY(now, matchResult);
                                        }
                                    }
                                    Series.Enabled = IsEnabled && Series.Points.Count > 0;
                                }
                            }
                        }
                        else
                        {
                            // handle as string
                            foreach (Match match in matches)
                            {
                                string newResult = match.Value;

                                result = true;
                                DateTime now = DateTime.Now;
                                LastResultTime = now;
                                SelectionResult = newResult;
                            }
                        }
                    }
                }
                else
                {
                    // handle as a literal
                    if (Type == MetricType.Number)
                    {
                        // handle as a double
                        double convertedResult = Convert.ToDouble(input);
                        if (convertedResult > 0.0)
                        {
                            result = true;
                            DateTime now = DateTime.Now;
                            LastResultTime = now;
                            NumericResult = convertedResult;

                            if (Series != null && IsEnabled)
                            {
                                if (!chartChangesOnly || (Series.Points.Count == 0) || (Series.Points.Last().YValues[0] != convertedResult))
                                {
                                    Series.Points.AddXY(now, convertedResult);
                                    Series.Enabled = IsEnabled && Series.Points.Count > 0;
                                }
                            }
                        }
                    }
                    else
                    {
                        // handle as a string
                        string newResult = input;
                        result = true;
                        DateTime now = DateTime.Now;
                        LastResultTime = now;
                        SelectionResult = newResult;
                    }
                }

                // chart missing values as an X
                if (!result && Series != null && IsEnabled && chartMissingValues)
                {
                    if ((Series.Points.Count == 0) || (!Series.Points.Last().IsEmpty))
                    {
                        Series.Points.AddXY(DateTime.Now, double.NaN);
                        Series.Points[Series.Points.Count - 1].IsEmpty = true;
                        Series.Points[Series.Points.Count - 1].Label = "X";
                        Series.Points[Series.Points.Count - 1].LabelForeColor = Color.Red;
                        Series.Enabled = IsEnabled && Series.Points.Count > 0;
                    }
                }
                
                return result;
            }
            catch (Exception ex)
            {
                throw;
                //return false;
                // TODO: see if this can be logged instead of thrown, then return false
            }
        }
    }    
}
