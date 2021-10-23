using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace MineControl
{    
    // TODO: provide a way to remember settings across versions, since right now it starts over each time version number changes    
    public partial class FormMineControl : Form, IChartManager, ILog
    {
        #region Local Consts
        // consts for app grid rows
        private const int cGPUMinerIndex = 0;
        private const int cCPUMinerIndex = 1;
        private const int cHWMonitorIndex = 2;
        private const int cGPUControllerIndex = 3;

        // consts for charts
        private const string cGPU = "GPU";
        private const string cCPU = "CPU";
        private const string cResources = "Resources";

        // consts for metrics and stats
        private const string cGPUPowerStep = "GPU Power Step";
        private const string cAvgGPUPowerStep = "Avg GPU Power Step";
        private const string cGPUHashRate = "GPU Hash Rate";
        private const string cGPUHashRateUnit = "GPU Hash Rate Unit";
        private const string cAvgGPUHashRate = "Avg GPU Hash Rate";
        private const string cGPUHashAlgo = "GPU Hash Algo";
        private const string cGPUMemJuncTemp = "GPU/Mem Junc Temp";
        private const string cGPUMemJuncTempUnit = "GPU/Mem Junc Temp Unit";
        private const string cAvgGPUMemJuncTemp = "Avg GPU/Mem Junc Temp";
        private const string cCPUHashRate = "CPU Hash Rate";
        private const string cCPUHashRateUnit = "CPU Hash Rate Unit";
        private const string cAvgCPUHashRate = "Avg CPU Hash Rate";
        private const string cCPUHashAlgo = "CPU Hash Algo";
        private const string cCPUTemp = "CPU Temp";
        private const string cCPUTempUnit = "CPU Temp Unit";
        private const string cAvgCPUTemp = "Avg CPU Temp";
        private const string cTotalPower = "Total Power (W)";
        private const string cAvgTotalPower = "Avg Total Power (W)";
        private const string cTotalPowerUse = "Total Power Use (kWh)";
        private const string cGPUPower = "GPU Power (W)";
        private const string cAvgGPUPower = "Avg GPU Power (W)";
        private const string cGPUPowerUse = "GPU Power Use (kWh)";
        private const string cCPUPower = "CPU Power (W)";
        private const string cAvgCPUPower = "Avg CPU Power (W)";
        private const string cCPUPowerUse = "CPU Power Use (kWh)";
        private const string cRuntime = "Runtime";
        #endregion

        #region Local Vars And Props
        // application
        private bool IsInitializing { get; set; } = true;
        private bool CanQuit { get; set; } = false;
        private DateTime StartupTimestamp { get; } = DateTime.Now;
                
        // temp management                
        private DateTime GPUOverheatStartTime { get; set; } = DateTime.MinValue;
        private DateTime GPUOverheatShutoffTime { get; set; } = DateTime.MinValue;
        private DateTime LastGPUStepChange { get; set; } = DateTime.Now;

        // miner management        
        private MinerState GPUState { get; set; } = MinerState.Uninitialized;
        private MinerState CPUState { get; set; } = MinerState.Uninitialized;
        
        // schedules        
        private BindingList<Schedule> Schedules { get; set; } = new BindingList<Schedule>();
        private string[] StartMonthDays { get; set; }
        private string[] EndMonthDays { get; set; }
        private BindingSource bindingSourceSchedules;
        private BindingSource bindingSourceGPUSchedule;
        private BindingSource bindingSourceCPUSchedule;

        // processes
        Job job = new Job();
        private Process ProcessGPUMiner { get; } = new Process();
        private bool isGPUMinerRunning= false;
        private Process ProcessCPUMiner { get; } = new Process();
        private bool isCPUMinerRunning = false;
        private Process ProcessHardwareMonitor { get; } = new Process();
        private bool isHardwareMonitorRunning  = false;
        private Process ProcessGPUController { get; } = new Process();
        private bool isGPUControllerRunning = false;
                
        // metrics
        private BindingList<Metric> Metrics { get; } = new BindingList<Metric>();
        private List<Chart> Charts { get; } = new List<Chart>(); 
        
        // settings
        private Properties.Settings Settings { get; } = Properties.Settings.Default;
        private List<string> ChangedSettings { get; } = new List<string>();
        private bool configNeedsArchiving { get; set; } = false;

        // logs
        private DataTable dataTableLog;

        // archives
        private DateTime LastArchiveEval { get; set; } = DateTime.Now;

        // serialization
        JsonSerializerOptions jsonOptionsScheduleNodes = new JsonSerializerOptions
        {
            Converters = { new ScheduleNodeConverter() },
            WriteIndented = true,
            PropertyNameCaseInsensitive = true,
            IncludeFields = true
        };
        JsonSerializerOptions jsonOptionsMetrics = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true,
            ReferenceHandler = ReferenceHandler.Preserve
        };
        JsonSerializerOptions jsonOptionsMetricQueryOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true
        };
        #endregion

        public FormMineControl()
        {
            InitializeComponent();            
            InitializeUI();
            LoadSettingsToUI(true);            
            IsInitializing = false;            
        }

        private void InitializeInternals()
        {
            Settings.SettingChanging += Settings_SettingChanging;
            Settings.PropertyChanged += Settings_PropertyChanged;
        }        

        private void InitializeUI()
        {   
            // display version
            this.Text += $" v{Assembly.GetEntryAssembly().GetName().Version}";
            labelAboutVersion.Text = Assembly.GetEntryAssembly().GetName().Version.ToString();

            // set up grids and their data sources
            dataGridViewApps.Rows.Add("GPU Miner", "", "Unknown", "");
            dataGridViewApps.Rows.Add("CPU Miner", "", "Unknown", "");
            dataGridViewApps.Rows.Add("Hardware Monitor", "", "Unknown", "");
            dataGridViewApps.Rows.Add("GPU Controller", "", "Unknown", "");
            dataTableLog = new DataTable();
            dataTableLog.Columns.Add("Source");
            dataTableLog.Columns.Add("Type");
            dataTableLog.Columns.Add("Time");
            dataTableLog.Columns["Time"].DataType = typeof(DateTime);
            dataTableLog.Columns.Add("Message");
            dataGridViewLog.AutoGenerateColumns = false;
            dataGridViewLog.DataSource = dataTableLog;
            dataGridViewLog.Columns[0].DataPropertyName = "Source";
            dataGridViewLog.Columns[1].DataPropertyName = "Type";
            dataGridViewLog.Columns[2].DataPropertyName = "Time";
            dataGridViewLog.Columns[3].DataPropertyName = "Message";            
            dataGridViewMetrics.AutoGenerateColumns = false;
            dataGridViewMetrics.DataSource = Metrics;
            dataGridViewMetrics.Columns[0].DataPropertyName = "IsEnabled";            
            dataGridViewMetrics.Columns[1].DataPropertyName = "Name";            
            dataGridViewMetrics.Columns[2].DataPropertyName = "Type";
            dataGridViewMetrics.Columns[3].DataPropertyName = "Source";
            dataGridViewMetrics.Columns[4].DataPropertyName = "Method";                      
            dataGridViewMetrics.Columns[5].DataPropertyName = "Query";
            ColDataMethod.DataSource = Enum.GetValues(typeof(MetricMethod));
            ColDataInputSource.DataSource = Enum.GetValues(typeof(MetricSource));
            ColDataType.DataSource = Enum.GetValues(typeof(MetricType));            
            // system-defined metrics (always present)
            Metrics.Add(new Metric(true, cGPUPowerStep, MetricType.Number, MetricSource.MineControl, MetricMethod.InternalValue, ""));
            Metric(cGPUPowerStep).IsInternal = true;            
            Metrics.Add(new Metric(false, cGPUHashRate, MetricType.Number, MetricSource.GPUMiner, MetricMethod.RegEx, ""));
            Metrics.Add(new Metric(false, cGPUHashRateUnit, MetricType.Selection, MetricSource.GPUMiner, MetricMethod.RegEx, ""));
            Metric(cGPUHashRate).Unit = Metric(cGPUHashRateUnit);
            Metrics.Add(new Metric(false, cGPUHashAlgo, MetricType.Selection, MetricSource.GPUMiner, MetricMethod.RegEx, ""));
            Metric(cGPUHashRate).GroupedBy = Metric(cGPUHashAlgo);
            Metric(cGPUHashRate).ChartManager = this;
            Metric(cGPUHashRate).Log = this;
            Metrics.Add(new Metric(false, cGPUMemJuncTemp, MetricType.Number, MetricSource.SysTray, MetricMethod.RegEx, ""));
            Metrics.Add(new Metric(false, cGPUMemJuncTempUnit, MetricType.Selection, MetricSource.SysTray, MetricMethod.RegEx, ""));
            Metric(cGPUMemJuncTemp).Unit = Metric(cGPUMemJuncTempUnit);
            Metrics.Add(new Metric(false, cCPUHashRate, MetricType.Number, MetricSource.CPUMiner, MetricMethod.RegEx, ""));
            Metrics.Add(new Metric(false, cCPUHashRateUnit, MetricType.Selection, MetricSource.CPUMiner, MetricMethod.RegEx, ""));
            Metric(cCPUHashRate).Unit = Metric(cCPUHashRateUnit);
            Metrics.Add(new Metric(false, cCPUHashAlgo, MetricType.Selection, MetricSource.CPUMiner, MetricMethod.RegEx, ""));
            Metric(cCPUHashRate).GroupedBy = Metric(cCPUHashAlgo);
            Metric(cCPUHashRate).ChartManager = this;
            Metric(cCPUHashRate).Log = this;
            Metrics.Add(new Metric(false, cCPUTemp, MetricType.Number, MetricSource.SysTray, MetricMethod.RegEx, ""));
            Metrics.Add(new Metric(false, cCPUTempUnit, MetricType.Selection, MetricSource.SysTray, MetricMethod.RegEx, ""));
            Metric(cCPUTemp).Unit = Metric(cCPUTempUnit);
            Metrics.Add(new Metric(false, cTotalPower, MetricType.Number, MetricSource.SysTray, MetricMethod.RegEx, ""));
            Metrics.Add(new Metric(false, cGPUPower, MetricType.Number, MetricSource.SysTray, MetricMethod.RegEx, ""));
            Metrics.Add(new Metric(false, cCPUPower, MetricType.Number, MetricSource.SysTray, MetricMethod.RegEx, ""));
            // create chart series for metrics that need one
            CreateChartSeriesForMetric(cGPU, Metric(cGPUPowerStep), SeriesChartType.StepLine, AxisType.Primary);
            CreateChartSeriesForMetric(cGPU, Metric(cGPUMemJuncTemp), SeriesChartType.Line, AxisType.Secondary);
            CreateChartSeriesForMetric(cGPU, Metric(cGPUHashRate), SeriesChartType.Line, AxisType.Secondary);
            CreateChartSeriesForMetric(cCPU, Metric(cCPUTemp), SeriesChartType.Line, AxisType.Primary);
            CreateChartSeriesForMetric(cCPU, Metric(cCPUHashRate), SeriesChartType.Line, AxisType.Secondary);
            CreateChartSeriesForMetric(cResources, Metric(cTotalPower), SeriesChartType.Line, AxisType.Secondary);
            CreateChartSeriesForMetric(cResources, Metric(cGPUPower), SeriesChartType.Line, AxisType.Secondary);
            CreateChartSeriesForMetric(cResources, Metric(cCPUPower), SeriesChartType.Line, AxisType.Secondary);                        

            // init stats in a logical order            
            SetStat(cGPUPowerStep, "");
            SetStat(cAvgGPUPowerStep, "");
            SetStat(cGPUMemJuncTemp, "");
            SetStat(cAvgGPUMemJuncTemp, "");
            SetStat(cGPUHashRate, "");
            SetStat(cAvgGPUHashRate, "");
            SetStat(cCPUTemp, "");
            SetStat(cAvgCPUTemp, "");
            SetStat(cCPUHashRate, "");
            SetStat(cAvgCPUHashRate, "");
            SetStat(cTotalPower, "");
            SetStat(cAvgTotalPower, "");
            SetStat(cTotalPowerUse, "");
            SetStat(cGPUPower, "");
            SetStat(cAvgGPUPower, "");
            SetStat(cGPUPowerUse, "");
            SetStat(cCPUPower, "");
            SetStat(cAvgCPUPower, "");
            SetStat(cCPUPowerUse, "");
            SetStat(cRuntime, "");
            
            // log filters            
            comboBoxLogFilter.SelectedIndex = 0;            

            // default to hours
            numericUpDownChartShowLastX.Value = 3;
            comboBoxChartShowLastUnit.SelectedIndex = comboBoxChartShowLastUnit.Items.IndexOf("Hours");

            // bind other data sources
            bindingSourceSchedules = new BindingSource();
            bindingSourceSchedules.DataSource = Schedules;
            comboBoxScheduleSchedules.DataSource = bindingSourceSchedules;
            comboBoxScheduleSchedules.DisplayMember = "Name";
            comboBoxScheduleSchedules.ValueMember = "Id";

            bindingSourceGPUSchedule = new BindingSource();
            bindingSourceGPUSchedule.DataSource = Schedules;
            comboBoxMinerGPUSchedule.DataSource = bindingSourceGPUSchedule;
            comboBoxMinerGPUSchedule.DisplayMember = "Name";
            comboBoxMinerGPUSchedule.ValueMember = "Id";

            bindingSourceCPUSchedule = new BindingSource();
            bindingSourceCPUSchedule.DataSource = Schedules;
            comboBoxMinerCPUSchedule.DataSource = bindingSourceCPUSchedule;
            comboBoxMinerCPUSchedule.DisplayMember = "Name";
            comboBoxMinerCPUSchedule.ValueMember = "Id";

            // schedule tab
            // align schedule node type UIs to calendar UI
            groupBoxScheduleWeek.Location = groupBoxScheduleCalendar.Location;
            groupBoxScheduleTime.Location = groupBoxScheduleCalendar.Location;
            groupBoxScheduleResult.Location = groupBoxScheduleCalendar.Location;
            // resize groupbox
            // initial radioButton
            radioButtonScheduleCalendar.Checked = true;
            // calendar            
            comboBoxScheduleStartMonth.DataSource = CultureInfo.InvariantCulture.DateTimeFormat.MonthNames.Take(12).ToList();
            comboBoxScheduleStartMonth.SelectedIndex = 0;
            comboBoxScheduleEndMonth.DataSource = CultureInfo.InvariantCulture.DateTimeFormat.MonthNames.Take(12).ToList();
            comboBoxScheduleEndMonth.SelectedText = CultureInfo.InvariantCulture.DateTimeFormat.MonthNames.Take(12).Last();
            UpdateScheduleDaysComboBoxesFromUI();
            PositionNodeButtons();
        }

        private void PositionNodeButtons()
        {            
            GroupBox visibleGroupBox = null;
            foreach (Control control in groupBoxScheduleNodeDetails.Controls)
            {
                if (control is RadioButton && (control as RadioButton).Checked)
                {
                    var radioButton = control as RadioButton;
                    if (radioButton.Equals(radioButtonScheduleCalendar))
                    {
                        visibleGroupBox = groupBoxScheduleCalendar;
                    }
                    else if (radioButton.Equals(radioButtonScheduleDaysOfWeek))
                    {
                        visibleGroupBox = groupBoxScheduleWeek;
                    }
                    else if (radioButton.Equals(radioButtonScheduleResult))
                    {
                        visibleGroupBox = groupBoxScheduleResult;
                    }
                    else if (radioButton.Equals(radioButtonScheduleTime))
                    {
                        visibleGroupBox = groupBoxScheduleTime;
                    }
                }
            }

            if (visibleGroupBox != null)
            {
                panelScheduleNodeButtons.Top = visibleGroupBox.Location.Y + visibleGroupBox.Height + 6;
            }            
        }

        public void CreateChartSeriesForMetric(string chartName, Metric metricToBeCharted, SeriesChartType seriesChartType, AxisType yAxisType)
        {
            TabPage tab = null;
            Chart chart = null;

            // look for the tab, which should always match the chart name
            foreach (TabPage tabPage in tabControlCharts.TabPages)
            {
                if (tabPage.Text == chartName)
                {
                    tab = tabPage;
                }
            }

            if (tab == null)
            {
                // make the tab and chart controls if not found
                // tab page
                tabControlCharts.TabPages.Add(chartName);
                tab = tabControlCharts.TabPages[tabControlCharts.TabPages.Count - 1];

                // chart
                chart = new Chart();
                chart.Name = chartName;
                Charts.Add(chart);
                tab.Controls.Add(chart);
                chart.Cursor = Cursors.Cross;
                chart.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                chart.Dock = DockStyle.Fill;
                chart.Dock = DockStyle.Fill;
                chart.GetToolTipText += new EventHandler<ToolTipEventArgs>(chartMain_GetToolTipText);
                chart.Palette = ChartColorPalette.None;

                // chart area
                ChartArea chartArea = chart.ChartAreas.Add("chartArea1");
                chartArea.AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
                chartArea.AxisX.IsLabelAutoFit = false;
                chartArea.AxisX.LabelStyle.Format = "M/d hh:mm.ss tt";
                chartArea.AxisX.LabelStyle.IsStaggered = true;
                chartArea.AxisX.MajorGrid.LineColor = Color.Silver;
                chartArea.AxisX.MinorGrid.Enabled = true;
                chartArea.AxisX.MinorGrid.LineColor = Color.Gainsboro;
                chartArea.AxisY.Interval = 1D;
                chartArea.AxisY.IntervalType = DateTimeIntervalType.Number;
                chartArea.AxisY.IsLabelAutoFit = false;
                chartArea.AxisY.LabelStyle.ForeColor = Color.Blue;
                chartArea.AxisY.LabelStyle.Interval = 1D;
                chartArea.AxisY.MajorGrid.LineColor = Color.SkyBlue;
                chartArea.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
                chartArea.AxisY.Maximum = 5D;
                chartArea.AxisY.Minimum = 1D;
                chartArea.AxisY2.Interval = 1D;
                chartArea.AxisY2.IntervalAutoMode = IntervalAutoMode.VariableCount;
                chartArea.AxisY2.IntervalType = DateTimeIntervalType.Number;
                chartArea.AxisY2.IsLabelAutoFit = false;
                chartArea.AxisY2.LabelStyle.ForeColor = Color.Red;
                chartArea.AxisY2.MajorGrid.LineColor = Color.MistyRose;
                chartArea.AxisY2.MajorGrid.LineDashStyle = ChartDashStyle.Dot;

                // legend
                Legend legend = chart.Legends.Add("legend1");
                legend.Docking = Docking.Top;
            }
            else
            { 
                // find the chart if it was already created
                foreach (Control control in tab.Controls)
                {
                    chart = control as Chart;
                }
            }

            if (chart != null)
            {
                Series series;
                series = chart.Series.Add("");

                series.BorderWidth = seriesChartType == SeriesChartType.Line ? 1 : 2;
                series.ChartArea = "chartArea1";
                series.ChartType = seriesChartType;
                series.Legend = "legend1";
                series.MarkerStyle = MarkerStyle.Circle;
                series.MarkerSize = seriesChartType == SeriesChartType.Line ? 4 : 6;
                series.SmartLabelStyle.IsOverlappedHidden = false;
                series.Name = chart.Series.Count.ToString(); // placeholder name
                series.XValueType = ChartValueType.DateTime;
                series.YAxisType = yAxisType;
                // color is autocalculated to match axis color scheme, with Y1 blue-based, Y2 red-based
                series.Color = yAxisType == AxisType.Primary ? Color.Blue : Color.Red;     
                while (chart.Series.Count(x => x.Color == series.Color) > 1)
                {
                    series.Color = ControlPaint.Dark(series.Color, 15);                    
                    // alternate marker style for further clarity
                    series.MarkerStyle = series.MarkerStyle == MarkerStyle.Circle ? MarkerStyle.Triangle : MarkerStyle.Circle;
                }
                series.MarkerColor = series.Color;

                metricToBeCharted.Chart = chart;
                metricToBeCharted.Series = series;                
            }
            else
            {
                AddLogEntry("Chart series could not be created due to missing chart", LogType.Error, LogSource.Internal);
            }            
        }        

        /// <summary>
        /// Loads all settings from settings object, optionally including schedules
        /// </summary>
        private void LoadSettingsToUI(bool includeSchedules = false)
        {
            // load schedules FIRST if needed
            if (includeSchedules)
            {
                LoadSchedulesToUI();
                textBoxScheduleName.Text = SelectedSchedule.Name;
                LoadScheduleToTreeView(SelectedSchedule);
            }

            // apps grid
            dataGridViewApps.Rows[cGPUMinerIndex].Cells[ColAppName.Index].Value = Settings.appGPUMinerName;
            dataGridViewApps.Rows[cGPUMinerIndex].Cells[ColAppPath.Index].Value = Settings.appGPUMinerPath;
            dataGridViewApps.Rows[cCPUMinerIndex].Cells[ColAppName.Index].Value = Settings.appCPUMinerName;
            dataGridViewApps.Rows[cCPUMinerIndex].Cells[ColAppPath.Index].Value = Settings.appCPUMinerPath;
            dataGridViewApps.Rows[cHWMonitorIndex].Cells[ColAppName.Index].Value = Settings.appHardwareMonitorName;            
            dataGridViewApps.Rows[cHWMonitorIndex].Cells[ColAppPath.Index].Value = Settings.appHardwareMonitorPath;
            dataGridViewApps.Rows[cGPUControllerIndex].Cells[ColAppName.Index].Value = Settings.appGPUControllerName;
            dataGridViewApps.Rows[cGPUControllerIndex].Cells[ColAppPath.Index].Value = Settings.appGPUControllerPath;

            // miner management
            checkBoxEnableMinerAutomation.Checked = Settings.minerEnableAutomation;
            switch ((MinerMode)Settings.minerGPUMode)
            {
                case MinerMode.AlwaysOff:
                    radioButtonGPUModeAlwaysOff.Checked = true;
                    break;
                case MinerMode.AlwaysOn:
                    radioButtonGPUModeAlwaysOn.Checked = true;
                    break;
                case MinerMode.DontControl:
                    radioButtonGPUModeDontControl.Checked = true;
                    break;
                case MinerMode.Schedule:
                    radioButtonGPUModeSchedule.Checked = true;
                    break;
            }
            switch ((MinerMode)Settings.minerCPUMode)
            {
                case MinerMode.AlwaysOff:
                    radioButtonCPUModeAlwaysOff.Checked = true;
                    break;
                case MinerMode.AlwaysOn:
                    radioButtonCPUModeAlwaysOn.Checked = true;
                    break;
                case MinerMode.DontControl:
                    radioButtonCPUModeDontControl.Checked = true;
                    break;
                case MinerMode.Schedule:
                    radioButtonCPUModeSchedule.Checked = true;
                    break;
            }
            if (!Settings.minerGPUSchedule.Equals(Guid.Empty))
            {
                comboBoxMinerGPUSchedule.SelectedValue = Schedules.First(x => x.Id.Equals(Settings.minerGPUSchedule)).Id;
            }
            if (!Settings.minerCPUSchedule.Equals(Guid.Empty))
            {
                comboBoxMinerCPUSchedule.SelectedValue = Schedules.First(x => x.Id.Equals(Settings.minerCPUSchedule)).Id;
            }
            checkBoxMinerGPUUserActivityShutoff.Checked = Settings.minerGPUUserActivityShutoff;
            checkBoxMinerCPUUserActivityShutoff.Checked = Settings.minerCPUUserActivityShutoff;
            numericUpDownMinerUserActivityTimeout.Value = Settings.minerUserActivityTimeoutMins;
            checkBoxMinerGPUShowLogs.Checked = Settings.minerGPUShowLogs;
            checkBoxMinerCPUShowLogs.Checked = Settings.minerCPUShowLogs;

            // temp management
            checkBoxEnableTempControl.Checked = Settings.tempEnableAutomation;
            numericUpDownTempMin.Value = Settings.tempMin;
            numericUpDownTempMax.Value = Settings.tempMax;
            numericUpDownTempPollingInterval.Value = Settings.tempPollingIntervalMillisecs;
            textBoxTempPowerStepParam1.Text = Settings.tempPowerStepParam1;
            textBoxTempPowerStepParam2.Text = Settings.tempPowerStepParam2;
            textBoxTempPowerStepParam3.Text = Settings.tempPowerStepParam3;
            textBoxTempPowerStepParam4.Text = Settings.tempPowerStepParam4;
            textBoxTempPowerStepParam5.Text = Settings.tempPowerStepParam5;
            checkBoxStopWhenOverheat.Checked = Settings.tempStopWhenOverheat;
            numericUpDownTempGPUShutOffSecs.Value = Settings.tempStopWhenOverheatSecs;
            numericUpDownTempGPUShutOffThresholdSecs.Value = Settings.tempStopWhenOverheatThresholdSecs;
            numericUpDownTempSteppingBuffer.Value = Settings.tempSpeedStepBuffer;
            trackBarGPUPowerStep.Value = Settings.tempSpeedStep;
            checkBoxTempTryStepUp.Checked = Settings.tempTryStepUp;
            numericUpDownTempTryStepUpSecs.Value = Settings.tempTryStepUpSecs;
            checkBoxTempStopWhenTempUnknown.Checked = Settings.tempStopWhenTempUnknown;

            // control/general
            checkBoxStartupMinimize.Checked = Settings.controlStartupMinimize;
            checkBoxStartUpAutomation.Checked = Settings.controlStartupRememberAutomation;            
            checkBoxMinimizeToSysTray.Checked = Settings.controlMinimizeToSysTray;
            
            // chart
            checkBoxChartMinGPUTempOnYAxisEnabled.Checked = Settings.chartMinTempOnYAxisEnabled;
            numericUpDownChartMinGPUTempOnYAxisValue.Value = Settings.chartMinTempOnYAxisValue;

            // archives and data retention
            checkBoxArchivesArchiveConfig.Checked = Settings.archivesArchiveConfig;
            numericUpDownArchivesArchiveInterval.Value = Settings.archivesArchiveInterval;
            comboBoxArchivesArchiveIntervalUnit.Text = Settings.archivesArchiveIntervalUnit;
            checkBoxArchivesLogManagement.Checked = Settings.archivesLogManagement;
            comboBoxArchivesLogManagementType.Text = Settings.archivesLogManagementType;
            numericUpDownArchivesLogManagementValue.Value = Settings.archivesLogManagementValue;
            comboBoxArchivesLogManagementUnit.Text = Settings.archivesLogManagementUnit;
            textBoxArchivesArchiveFolder.Text = GetArchiveFolder();
            checkBoxArchivesDeleteOldFiles.Checked = Settings.archivesDeleteOldFiles;
            numericUpDownArchivesDeleteOldFilesDays.Value = Settings.archivesDeleteOldFilesDays;
            checkBoxArchivesClearOldCharts.Checked = Settings.archivesClearOldCharts;
            numericUpDownArchivesClearOldChartsValue.Value = Settings.archivesClearOldChartsValue;
            comboBoxArchivesClearOldChartsUnit.Text = Settings.archivesClearOldChartsUnit;

            // metrics
            if (Settings.metricsSerializedMetricQueryOptions.Trim().Length > 0)
            {
                BindingList<string> loadedQueryOptions = JsonSerializer.Deserialize<BindingList<string>>(Settings.metricsSerializedMetricQueryOptions);
                foreach (string option in loadedQueryOptions)
                {                     
                    if (!ColDataQuery.Items.Contains(option))
                    {
                        ColDataQuery.Items.Add(option);
                    }
                }                
            }
            if (Settings.metricsSerializedMetrics.Trim().Length > 0)
            {
                BindingList<Metric> loadedMetrics = JsonSerializer.Deserialize<BindingList<Metric>>(Settings.metricsSerializedMetrics, jsonOptionsMetrics);
                
                foreach (Metric metric in loadedMetrics)
                {   
                    if (!Metrics.Any(x => x.Name == metric.Name))
                    {
                        Metrics.Add(metric);
                    }
                    else if (Metric(metric.Name).IsInternal)
                    {
                        // just get enabled status for internal metrics
                        Metric(metric.Name).IsEnabled = metric.IsEnabled;
                    }
                    else
                    {
                        // pull in external metrics fully
                        Metric(metric.Name).Assign(metric);
                    }
                }
            }            
        }

        /// <summary>
        /// Loads schedules from persistent storage
        /// </summary>
        private void LoadSchedulesToUI()
        {
            try
            {
                Guid? selectedScheduleId = null;
                if (comboBoxScheduleSchedules.SelectedIndex >= 0)
                {
                    selectedScheduleId = Schedules[comboBoxScheduleSchedules.SelectedIndex].Id;
                }               
                if (Settings.scheduleSerializedSchedules.Trim().Length > 0)
                {
                    Schedules = JsonSerializer.Deserialize<BindingList<Schedule>>(Settings.scheduleSerializedSchedules, jsonOptionsScheduleNodes);
                    bindingSourceSchedules.DataSource = Schedules;
                    bindingSourceGPUSchedule.DataSource = Schedules;
                    bindingSourceCPUSchedule.DataSource = Schedules;
                    bindingSourceSchedules.ResetBindings(true);
                    bindingSourceGPUSchedule.ResetBindings(true);
                    bindingSourceCPUSchedule.ResetBindings(true);

                    // reselect previous schedule if present
                    if (selectedScheduleId != null)
                    {
                        comboBoxScheduleSchedules.SelectedIndex = Schedules.IndexOf(Schedules.Last(x => x.Id == selectedScheduleId));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Schedules could not be loaded due to an exception:\n{ex.GetType()}\n{ex.Message}");                
            }
        }

        /// <summary>
        /// Saves all settings to persistent storage, optionally including schedules
        /// </summary>
        private void SaveSettingsFromUI(bool includeSchedules = false)
        {
            // don't save anything while we're initializing
            if (IsInitializing)
            {
                return;
            }

            // schedules
            if (includeSchedules)
                SaveSchedulesFromList();

            // apps grid
            Settings.appGPUMinerName = dataGridViewApps.Rows[cGPUMinerIndex].Cells[ColAppName.Index].Value?.ToString() ?? string.Empty;
            Settings.appGPUMinerPath = dataGridViewApps.Rows[cGPUMinerIndex].Cells[ColAppPath.Index].Value?.ToString() ?? string.Empty;
            Settings.appCPUMinerName = dataGridViewApps.Rows[cCPUMinerIndex].Cells[ColAppName.Index].Value?.ToString() ?? string.Empty;
            Settings.appCPUMinerPath = dataGridViewApps.Rows[cCPUMinerIndex].Cells[ColAppPath.Index].Value?.ToString() ?? string.Empty;
            Settings.appHardwareMonitorName = dataGridViewApps.Rows[cHWMonitorIndex].Cells[ColAppName.Index].Value?.ToString() ?? string.Empty;
            Settings.appHardwareMonitorPath = dataGridViewApps.Rows[cHWMonitorIndex].Cells[ColAppPath.Index].Value?.ToString() ?? string.Empty;
            Settings.appGPUControllerName = dataGridViewApps.Rows[cGPUControllerIndex].Cells[ColAppName.Index].Value?.ToString() ?? string.Empty;
            Settings.appGPUControllerPath = dataGridViewApps.Rows[cGPUControllerIndex].Cells[ColAppPath.Index].Value?.ToString() ?? string.Empty;

            // miner management            
            Settings.minerEnableAutomation = checkBoxEnableMinerAutomation.Checked;
            Settings.minerGPUMode = radioButtonGPUModeAlwaysOn.Checked ? (int)MinerMode.AlwaysOn :
                radioButtonGPUModeAlwaysOff.Checked ? (int)MinerMode.AlwaysOff :
                radioButtonGPUModeDontControl.Checked ? (int)MinerMode.DontControl : 3;
            Settings.minerCPUMode = radioButtonCPUModeAlwaysOn.Checked ? (int)MinerMode.AlwaysOn :
                radioButtonCPUModeAlwaysOff.Checked ? (int)MinerMode.AlwaysOff :
                radioButtonCPUModeDontControl.Checked ? (int)MinerMode.DontControl : 3;
            Settings.minerGPUSchedule = comboBoxMinerGPUSchedule.SelectedValue == null ? Guid.Empty : (Guid)comboBoxMinerGPUSchedule.SelectedValue;
            Settings.minerCPUSchedule = comboBoxMinerCPUSchedule.SelectedValue == null ? Guid.Empty : (Guid)comboBoxMinerCPUSchedule.SelectedValue;
            Settings.minerGPUUserActivityShutoff = checkBoxMinerGPUUserActivityShutoff.Checked;
            Settings.minerCPUUserActivityShutoff = checkBoxMinerCPUUserActivityShutoff.Checked;
            Settings.minerUserActivityTimeoutMins = (int)numericUpDownMinerUserActivityTimeout.Value;
            Settings.minerGPUShowLogs = checkBoxMinerGPUShowLogs.Checked;
            Settings.minerCPUShowLogs = checkBoxMinerCPUShowLogs.Checked;

            // temp management
            Settings.tempEnableAutomation = checkBoxEnableTempControl.Checked;
            Settings.tempMin = (int)numericUpDownTempMin.Value;
            Settings.tempMax = (int)numericUpDownTempMax.Value;
            Settings.tempPollingIntervalMillisecs = (int)numericUpDownTempPollingInterval.Value;
            Settings.tempPowerStepParam1 = textBoxTempPowerStepParam1.Text;
            Settings.tempPowerStepParam2 = textBoxTempPowerStepParam2.Text;
            Settings.tempPowerStepParam3 = textBoxTempPowerStepParam3.Text;
            Settings.tempPowerStepParam4 = textBoxTempPowerStepParam4.Text;
            Settings.tempPowerStepParam5 = textBoxTempPowerStepParam5.Text;
            Settings.tempStopWhenOverheat = checkBoxStopWhenOverheat.Checked;
            Settings.tempStopWhenOverheatSecs = (int)numericUpDownTempGPUShutOffSecs.Value;
            Settings.tempStopWhenOverheatThresholdSecs = (int)numericUpDownTempGPUShutOffThresholdSecs.Value;
            Settings.tempSpeedStepBuffer = (int)numericUpDownTempSteppingBuffer.Value;
            Settings.tempSpeedStep = trackBarGPUPowerStep.Value;
            Settings.tempTryStepUp = checkBoxTempTryStepUp.Checked;
            Settings.tempTryStepUpSecs = (int)numericUpDownTempTryStepUpSecs.Value;
            Settings.tempStopWhenTempUnknown = checkBoxTempStopWhenTempUnknown.Checked;

            // control/general
            Settings.controlStartupMinimize = checkBoxStartupMinimize.Checked;
            Settings.controlStartupRememberAutomation = checkBoxStartUpAutomation.Checked;
            Settings.controlMinimizeToSysTray = checkBoxMinimizeToSysTray.Checked;
            
            // chart            
            Settings.chartMinTempOnYAxisEnabled = checkBoxChartMinGPUTempOnYAxisEnabled.Checked;
            Settings.chartMinTempOnYAxisValue = (int)numericUpDownChartMinGPUTempOnYAxisValue.Value;

            // archives and data retention
            Settings.archivesArchiveConfig = checkBoxArchivesArchiveConfig.Checked;
            Settings.archivesArchiveInterval = (int)numericUpDownArchivesArchiveInterval.Value;
            Settings.archivesArchiveIntervalUnit = comboBoxArchivesArchiveIntervalUnit.Text;
            Settings.archivesLogManagement = checkBoxArchivesLogManagement.Checked;
            Settings.archivesLogManagementType = comboBoxArchivesLogManagementType.Text;
            Settings.archivesLogManagementValue = (int)numericUpDownArchivesLogManagementValue.Value;
            Settings.archivesLogManagementUnit = comboBoxArchivesLogManagementUnit.Text;
            Settings.archivesArchiveFolder = textBoxArchivesArchiveFolder.Text;
            Settings.archivesDeleteOldFiles = checkBoxArchivesDeleteOldFiles.Checked;
            Settings.archivesDeleteOldFilesDays = (int)numericUpDownArchivesDeleteOldFilesDays.Value;
            Settings.archivesClearOldCharts = checkBoxArchivesClearOldCharts.Checked;
            Settings.archivesClearOldChartsValue = (int)numericUpDownArchivesClearOldChartsValue.Value;
            Settings.archivesClearOldChartsUnit = comboBoxArchivesClearOldChartsUnit.Text;

            // metrics
            Settings.metricsSerializedMetrics = JsonSerializer.Serialize(Metrics, jsonOptionsMetrics);
            Settings.metricsSerializedMetricQueryOptions = JsonSerializer.Serialize(ColDataQuery.Items, jsonOptionsMetricQueryOptions);
            
            // commit the changes
            Settings.Save();    

            try
            {
                if (Settings.archivesArchiveConfig && !Directory.GetFiles(GetConfigArchiveFolder()).Any())
                {
                    ArchiveChangedConfig(true);
                }
            }
            catch (Exception ex)
            {
                AddLogEntry($"Exception archiving config: {ex.GetType()} - {ex.Message}");
            }
        }
        
        /// <summary>
        /// Save schedules to persistent storage
        /// </summary>
        private void SaveSchedulesFromList()
        {
           Settings.scheduleSerializedSchedules = JsonSerializer.Serialize(Schedules, jsonOptionsScheduleNodes);
           Settings.Save();
        }

        private void Settings_SettingChanging(object sender, System.Configuration.SettingChangingEventArgs e)
        {
            Invoke(new Action(() =>
            {
                // flag the setting as changing if its value is actually going to be different
                if (!Settings.PropertyValues[e.SettingName].PropertyValue.Equals(e.NewValue))
                {
                    ChangedSettings.Add(e.SettingName);
                }
            }));
        }

        private void Settings_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Invoke(new Action(() =>
            {
#if DEBUG
                if (ChangedSettings.Count > 1)
                {
                    AddLogEntry($"Settings_PropertyChanged: settingsToBeApplied.Count was > 1 ({ChangedSettings.Count})", LogType.Debug);
                }
#endif
                // apply/notify changed settings here, since this has to be done AFTER the change is saved to the setting
                string changedSetting;
                while (ChangedSettings.Count > 0)
                {
                    try
                    {
                        // we need to remove the changed setting BEFORE we try to apply it in order to avoid possible recursion
                        changedSetting = ChangedSettings[0];
                        ChangedSettings.RemoveAt(0);

                        ApplySettingAndNotify(changedSetting);
                    }
                    catch (Exception ex)
                    {
                        AddLogEntry($"Exception applying changed setting '{ChangedSettings[0]}': {ex.GetType()} - {ex.Message}");
                    }
                }
            }));            
        }

        /// <summary>
        /// Applies the given setting and notifies the user, when appropriate to the setting
        /// </summary>
        /// <param name="settingName">EXACT name of the setting</param>
        /// <param name="doNotify">If true, user will be appropriately notified of the change and any instructions needed</param>
        private void ApplySettingAndNotify(string settingName)
        {
            Invoke(new Action(() =>
            {                
                bool settingApplied = true;
                switch (settingName)
                {
                    // chart updates required
                    case nameof(Settings.chartMinTempOnYAxisEnabled):
                    case nameof(Settings.chartMinTempOnYAxisValue):
                        UpdateChartScales(false);
                        configNeedsArchiving = true;
                        break;

                    // applied automatically but needs archiving
                    case nameof(Settings.controlMinimizeToSysTray):
                    case nameof(Settings.controlStartupMinimize):
                    case nameof(Settings.controlStartupRememberAutomation):
                    case nameof(Settings.generalSysTrayDisplayMode):                    
                    case nameof(Settings.minerCPUSchedule):                    
                    case nameof(Settings.minerGPUSchedule):
                    case nameof(Settings.minerCPUUserActivityShutoff):
                    case nameof(Settings.minerGPUUserActivityShutoff):
                    case nameof(Settings.tempPowerStepParam1):
                    case nameof(Settings.tempPowerStepParam2):
                    case nameof(Settings.tempPowerStepParam3):
                    case nameof(Settings.tempPowerStepParam4):
                    case nameof(Settings.tempPowerStepParam5):
                    case nameof(Settings.tempStopWhenOverheat):
                    case nameof(Settings.tempStopWhenOverheatSecs):
                    case nameof(Settings.tempStopWhenOverheatThresholdSecs):
                    case nameof(Settings.tempStopWhenTempUnknown):
                    case nameof(Settings.tempSpeedStepBuffer):
                    case nameof(Settings.tempTryStepUp):
                    case nameof(Settings.tempTryStepUpSecs):
                    case nameof(Settings.minerUserActivityTimeoutMins):
                    case nameof(Settings.minerGPUShowLogs):
                    case nameof(Settings.minerCPUShowLogs):
                    case nameof(Settings.archivesArchiveConfig):
                    case nameof(Settings.archivesArchiveInterval):
                    case nameof(Settings.archivesArchiveIntervalUnit):
                    case nameof(Settings.archivesLogManagement):
                    case nameof(Settings.archivesLogManagementType):
                    case nameof(Settings.archivesLogManagementValue):
                    case nameof(Settings.archivesLogManagementUnit):
                    case nameof(Settings.archivesArchiveFolder):
                    case nameof(Settings.archivesDeleteOldFiles):
                    case nameof(Settings.archivesDeleteOldFilesDays):
                    case nameof(Settings.scheduleSerializedSchedules):
                    case nameof(Settings.tempMax):
                    case nameof(Settings.tempMin):
                    case nameof(Settings.metricsSerializedMetrics):
                    case nameof(Settings.metricsSerializedMetricQueryOptions):
                    case nameof(Settings.archivesClearOldCharts):
                    case nameof(Settings.archivesClearOldChartsValue):
                    case nameof(Settings.archivesClearOldChartsUnit):
                        configNeedsArchiving = true;
                        break;

                    // nothing required
                    case nameof(Settings.controlRunning):    
                        break;

                    // miner changes that can be applied immediately
                    case nameof(Settings.minerCPUMode):
                        UpdateMinerState(false);
                        configNeedsArchiving = true;
                        break;
                    case nameof(Settings.minerGPUMode):
                        UpdateMinerState(true);
                        configNeedsArchiving = true;
                        break;
                    case nameof(Settings.minerEnableAutomation):
                    case nameof(Settings.tempEnableAutomation):
                        UpdateMinerState(true);
                        UpdateMinerState(false);        
                        // note: basic control setting that does not trigger archiving
                        break;
                    case nameof(Settings.tempSpeedStep):  // Note: applying this setting is handled elsewhere                    
                        SysTrayIcon.UpdateTextIcon(notifyIconMain, false, GPUState, CPUState);
                        break;

                    // other changes that can be applied immediately
                    case nameof(Settings.tempPollingIntervalMillisecs):
                        timerMain.Interval = Settings.tempPollingIntervalMillisecs;
                        configNeedsArchiving = true;
                        break;

                    default:
                        settingApplied = false;
                        configNeedsArchiving = true;
                        break;
                }

                if (!settingApplied && Settings.controlRunning)
                {
                    ShowTooltipNotification("Setting will be applied next time automation is restarted", ToolTipIcon.Warning);
                }
            }));
        }     

        private void ShowTooltipNotification(string message, ToolTipIcon toolTipIcon = ToolTipIcon.Info)
        {
            // hide any previous notification first -- this is needed to avoid timing issues leading to early closure of new tooltips
            toolTipNotification.Hide(this.groupBoxControl);

            toolTipNotification.ToolTipIcon = toolTipIcon;
            toolTipNotification.Show(message, groupBoxControl, 0, 0, 5000);
        }

        private string GetStat(string rowText)
        {
            foreach (DataGridViewRow row in dataGridViewStats.Rows)
            {
                if (row.Cells[0].Value.ToString() == rowText)
                {
                    return row.Cells[1].Value.ToString();
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Sets the stat with the given name, creating it if it doesn't exist. Blanks are replaced with cBlankStat. 
        /// </summary>
        /// <param name="statName">Name of the stat, or "*SetAll*" if all stats are to be updated</param>
        /// <param name="value">Value that will be set for the stat(s)</param>
        /// <returns>true if a stat was updated</returns>
        private void SetStat(string statName, string value)
        {            
            string effectiveValue = value.Trim() == string.Empty ? Const.BlankInfo : value;

            foreach (DataGridViewRow row in dataGridViewStats.Rows)
            {
                if (statName == "*SetAll*")
                {
                    row.Cells[1].Value = effectiveValue;
                    row.Cells[ColStatsLastUpdate.Index].Value = DateTime.Now;
                    row.DefaultCellStyle.ForeColor = Color.Black;
                }
                else if (row.Cells[0].Value.ToString() == statName)
                {
                    row.Cells[1].Value = effectiveValue;
                    row.Cells[ColStatsLastUpdate.Index].Value = DateTime.Now;
                    row.DefaultCellStyle.ForeColor = Color.Black;
                    return;
                }
            }

            // add as a new row if it doesn't exist
            if (statName != "*SetAll*")
            {
                int idx = dataGridViewStats.Rows.Add(statName, effectiveValue, DateTime.Now);
                dataGridViewStats.Rows[idx].DefaultCellStyle.ForeColor = Color.Black;
            }
        }

        private void InitializeAutomationFromSavedState()
        {
            if (Settings.controlStartupRememberAutomation && Settings.controlRunning)
            {
                StartAutomation();
            }
            else
            {
                StopAutomation();
            }
        }

        private void LaunchHardwareManagementApps()
        {   
            // hardware monitor
            LaunchProcess(ProcessHardwareMonitor, Settings.appHardwareMonitorPath, Settings.appHardwareMonitorName, 
                "Hardware Monitor", ref isHardwareMonitorRunning, cHWMonitorIndex, false);

            // gpu controller
            LaunchProcess(ProcessGPUController, Settings.appGPUControllerPath, Settings.appGPUControllerName,
                "GPU Controller", ref isGPUControllerRunning, cGPUControllerIndex, false);           
        }

        private void LaunchProcess(Process process, string path, string name, string logName, ref bool isRunning, int gridRow, bool fullControl)
        { 
            if (File.Exists(path) && (name.Trim() != string.Empty))
            {
                if (fullControl)
                {
                    // only mess with it if we're not already running it as a sub-process
                    if (!ProcessUtils.IsProcessRunningFromObject(process))
                    {
                        try
                        {
                            // kill any external instances (not ours)
                            if (ProcessUtils.KillProcessInstancesByName(name))
                            {
                                AddLogEntry($"{logName} app \"{name}\" external instance(s) killed");
                            }

                            // this avoids exceptions if an output read was erroneously left in place
                            try
                            {
                                process.CancelOutputRead();
                            }
                            catch
                            {
                                // will raise exceptions in most cases that should be ignored
                            }

                            // start our instance                            
                            process.StartInfo.FileName = path;
                            process.StartInfo.WorkingDirectory = Path.GetDirectoryName(path);
                            process.StartInfo.RedirectStandardOutput = true;
                            process.StartInfo.UseShellExecute = false;
                            process.StartInfo.CreateNoWindow = true;
                            process.StartInfo.Verb = "runas";
                            process.OutputDataReceived += ProcessOutputReceived;
                            process.Start();
                            isRunning = true;
                            dataGridViewApps.Rows[gridRow].Cells[ColAppStatus.Index].Value = "Running";
                            process.BeginOutputReadLine();

                            // add to job so it will close if MC crashes
                            bool isCrashSafe = false;
                            try
                            {
                                isCrashSafe = job.AddProcess(process.Id);                                
                            }
                            catch
                            { 
                                // eat any failures here
                            }                                              
                            
                            AddLogEntry(
                                $"{logName} app \"{name}\" started at path \"{path}\" under full control {(isCrashSafe ? "with" : "WITHOUT")} crash safety", 
                                isCrashSafe ? LogType.Info : LogType.Warning);                      
                        }
                        catch (Exception ex)
                        {
                            dataGridViewApps.Rows[gridRow].Cells[ColAppStatus.Index].Value = "Unknown (Error)";
                            AddLogEntry($"{logName} app \"{name}\" kill/start failed with the following exception: ({ex.GetType()}) {ex.Message}", LogType.Error);
                        }
                    }
                    else
                    {
                        dataGridViewApps.Rows[gridRow].Cells[ColAppStatus.Index].Value = "Running";
                    }
                }
                else
                {
                    if (!ProcessUtils.IsProcessRunningByName(name))
                    {
                        process.StartInfo.FileName = path;
                        process.StartInfo.UseShellExecute = true;
                        process.Start();
                        isRunning = true;
                        dataGridViewApps.Rows[gridRow].Cells[ColAppStatus.Index].Value = "Running";
                        AddLogEntry($"{logName} app \"{name}\" started at path \"{path}\"");
                    }
                    else if ((string)dataGridViewApps.Rows[gridRow].Cells[ColAppStatus.Index].Value != "Running (ext)")
                    {
                        dataGridViewApps.Rows[gridRow].Cells[ColAppStatus.Index].Value = "Running (ext)";
                        AddLogEntry($"{logName} app \"{name}\" was already running");
                    }
                }
            }
            else
            {
                AddLogEntry($"{logName} app info is invalid, so it wasn't executed or verified", LogType.Error);
            }
        }        

        private void CloseProcess(Process process, string path, string name, string logName, int gridRow, ref bool isRunning)
        {
            if (ProcessUtils.IsProcessRunningFromObject(process))
            {
                try
                {
                    if (process.StartInfo.RedirectStandardOutput)
                    {
                        process.CancelOutputRead();
                        process.OutputDataReceived -= ProcessOutputReceived;
                    }
                    process.Kill();                    
                    AddLogEntry($"{logName} app \"{Path.GetFileNameWithoutExtension(path)}\" killed");
                }
                catch (Exception ex)
                {
                    AddLogEntry($"{logName} app \"{name}\" kill may have failed due to exception: {ex.GetType()} - {ex.Message}");
                }
            }

            try
            {
                // in case process was a batch file, kill the process by name as well
                if (ProcessUtils.KillProcessInstancesByName(name))
                {
                    AddLogEntry($"{logName} app \"{name}\" extra instance(s) killed");
                }
            }
            catch (Exception ex)
            {
                AddLogEntry($"{logName} app \"{name}\" extra instance kill may have failed due to exception: {ex.GetType()} - {ex.Message}");
            }

            isRunning = false;
            dataGridViewApps.Rows[gridRow].Cells[ColAppStatus.Index].Value = "Stopped";            
        }

        private void ProcessOutputReceived(object sender, DataReceivedEventArgs e)
        {   
            // requires main thread execution since it affects the UI
            Invoke(new Action(() =>
                {
                // send non-blank data from non-null objects to the log
                if ((e != null) && (e.Data != null) && (e.Data.Trim().Length > 0))
                {
                    MetricSource applicableSource;
                    string logName;

                    if (sender == ProcessGPUMiner)
                    {
                        applicableSource = MetricSource.GPUMiner;
                        logName = cGPU;
                    }
                    else if (sender == ProcessCPUMiner)
                    {
                        applicableSource = MetricSource.CPUMiner;
                        logName = cCPU;
                    }
                    else
                    {
                        // don't know what to do with unknown sources
                        AddLogEntry($"Output received from Unknown source '{sender}'", LogType.Warning);
                        return;
                    }

                    // process the input
                    bool inputFound = false;
                    string inputValue;
                    StringBuilder inputLogValues = new StringBuilder();
                    try
                    {
                        foreach (Metric metric in Metrics)
                        {
                            if (metric.Source == applicableSource && metric.IsEnabled && metric.UpdateFromInput(e.Data, false, false))
                            {
                                inputFound = true;
                                inputValue = metric.Type == MetricType.Number ? metric.NumericResult.ToString() : metric.SelectionResult;
                                inputLogValues.Append($"({metric.Name}={inputValue})");
                                if (!(metric.Name.Contains("Unit") || metric.Name.Contains("Algo")))
                                {
                                    SetStat(metric.Name, inputValue);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        AddLogEntry($"Exception reading {logName} hash rate (this may negatively affect {logName} hash rate calculations): {ex.GetType()} - {ex.Message}", LogType.Error);
                    }
                    finally
                    {
                        string logMsg = inputLogValues.ToString() == string.Empty ? e.Data : $"{e.Data} <MineControl inputs: {inputLogValues}>";

                        if (applicableSource == MetricSource.GPUMiner && Settings.minerGPUShowLogs)
                            AddLogEntry(logMsg, inputFound ? LogType.Input : LogType.Info, LogSource.GPUMiner);
                        else if (applicableSource == MetricSource.CPUMiner && Settings.minerCPUShowLogs)
                            AddLogEntry(logMsg, inputFound ? LogType.Input : LogType.Info, LogSource.CPUMiner);
                    }
                }
            }));
        }

       
        private void StartAutomation()
        {
            AddLogEntry("Automation started");           
            Settings.controlRunning = true;
            labelStatusDisplay.Text = "Running";
            labelStatusDisplay.ForeColor = Color.Green;
            LaunchHardwareManagementApps();
            UpdateMinerState(true);
            UpdateMinerState(false);
            timerMain.Interval = Settings.tempPollingIntervalMillisecs;
            timerMain.Enabled = true;
            SaveSettingsFromUI();
        }

        private void StopAutomation()
        {
            AddLogEntry("Automation stopped");
            Settings.controlRunning = false;
            labelStatusDisplay.Text = "Stopped";
            labelStatusDisplay.ForeColor = Color.Red;
            timerMain.Enabled = false;
            SetStat(cGPUMemJuncTemp, Const.UnknownTemp);            
            UpdateMinerState(true);
            UpdateMinerState(false);
            SaveSettingsFromUI();
        }

        /// <summary>
        /// Processes all rules affecting or affected by GPU temp
        /// </summary>
        private void DoGPUTempRules()
        {            
            int temp;
            int step;

            // adjust power step based on GPU temp
            if (Int32.TryParse(GetStat(cGPUMemJuncTemp), out temp) && LastGPUStepChange.AddSeconds((double)Settings.tempSpeedStepBuffer) < DateTime.Now)
            {
                if ((temp < Settings.tempMin))
                {
                    if (Settings.tempSpeedStep < trackBarGPUPowerStep.Maximum)
                    {
                        step = Settings.tempSpeedStep + 1;
                        AddLogEntry(string.Format("GPU temp below minimum, stepping up to {0}", step.ToString()));                        
                        StepGPUPower(step);
                    }
                }
                else if (temp > Settings.tempMax)
                {
                    if (Settings.tempSpeedStep > trackBarGPUPowerStep.Minimum)
                    {
                        step = Settings.tempSpeedStep - 1;
                        AddLogEntry(string.Format("GPU temp above maximum, stepping down to {0}", step.ToString()));                        
                        StepGPUPower(step);
                    }
                }

                // try stepping power up based on user settings
                if (Settings.tempTryStepUp && 
                    (LastGPUStepChange.AddSeconds((double)Settings.tempTryStepUpSecs) < DateTime.Now) && 
                    (Settings.tempSpeedStep < trackBarGPUPowerStep.Maximum))
                {
                    step = Settings.tempSpeedStep + 1;
                    AddLogEntry(string.Format("GPU power step unchanged for {0} seconds, stepping up to {1}", Settings.tempTryStepUpSecs, step.ToString()));
                    StepGPUPower(step);
                }
            }            
        }

        public void Append(string entry, LogType logType = LogType.Info, LogSource logSource = LogSource.Internal)
        {
            AddLogEntry(entry, logType, logSource);
        }

        private void AddLogEntry(string entry, LogType logType = LogType.Info, LogSource logSource = LogSource.Internal)
        {
            // can't invoke until control has been created, so entries at this stage are lost
            if (!this.Created)
            {
                return;
            }
            
            Invoke(new Action(() =>
            {
                string logSourceText;
                switch (logSource)
                {
                    case LogSource.Internal:
                        logSourceText = "MineControl";
                        break;
                    case LogSource.GPUMiner:
                        logSourceText = "GPU Miner";
                        break;
                    case LogSource.CPUMiner:
                        logSourceText = "CPU Miner";
                        break;
                    default:
                        logSourceText = "MineControl";
                        break;
                }

                // add the row                
                dataTableLog.Rows.Add(logSourceText, Enum.GetName(typeof(LogType), logType), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), entry);

                // scroll if needed
                ScrollLogToEnd();
            }));
        }

        private void ScrollLogToEnd()
        {
            if (checkBoxLogAutoScroll.Checked && (dataGridViewLog.Rows.GetLastRow(DataGridViewElementStates.Visible) >= 0))
            {
                dataGridViewLog.FirstDisplayedScrollingRowIndex = dataGridViewLog.Rows.GetLastRow(DataGridViewElementStates.Visible);
            }
        }

        private void StepGPUPower(int step)
        {
            // get user's customized profile number
            string processParams = string.Empty;
            if (step == 1)
            {
                processParams = Settings.tempPowerStepParam1;
            }
            else if (step == 2)
            {
                processParams = Settings.tempPowerStepParam2;
            }
            else if (step == 3)
            {
                processParams = Settings.tempPowerStepParam3;
            }
            else if (step == 4)
            {
                processParams = Settings.tempPowerStepParam4;
            }
            else if (step == 5)
            {
                processParams = Settings.tempPowerStepParam5;
            }
            
            using (Process p = new Process())
            {
                p.StartInfo.FileName = Settings.appGPUControllerPath;
                p.StartInfo.Arguments = processParams;
                p.StartInfo.UseShellExecute = true;
                p.Start();
            }

            LastGPUStepChange = DateTime.Now;
            trackBarGPUPowerStep.Value = step;            
        }

        /// <summary>
        /// Processes one polling cycle in priority order
        /// </summary>
        private void DoPollingCycle()
        {
            if (UpdateGPUTemp() && Settings.controlRunning && Settings.tempEnableAutomation)
            {
                DoGPUTempRules();
            }

            // update miner run states based on current conditions
            UpdateMinerState(true);
            UpdateMinerState(false);

            // other UI updates
            UpdatePolledMetrics();
            UpdateChartScales(false);            
            UpdatePolledStats();
            UpdateStatColors();            

            try
            {
                if (StartArchiveAndClear(out DateTime evalStartTime))
                {
                    ArchiveAndClearOldLogs(evalStartTime);
                    ArchiveChangedConfig();
                    ClearOldChartData(evalStartTime);
                    DeleteOldArchives(evalStartTime);
                }
            }
            catch (Exception ex)
            {
                AddLogEntry($"Exception archiving/clearing data: {ex.GetType()} - {ex.Message}");
            }
        }

        private void DeleteOldArchives(DateTime evalStartTime)
        {
            if (Settings.archivesDeleteOldFiles)
            {
                DateTime archiveAgeCutoff = evalStartTime.AddDays(-Settings.archivesDeleteOldFilesDays);
                int deletedLogCount = 0;
                int deletedConfigCount = 0;

                // delete old log archives
                DirectoryInfo directory = new DirectoryInfo(GetArchiveFolder());
                var files = directory.GetFiles("*.*").Where(f => f.LastWriteTime < archiveAgeCutoff && Path.GetExtension(f.Name) == ".txt");
                foreach (var file in files)
                {
                    file.Delete();
                    ++deletedLogCount;
                }

                // delete old config archives
                directory = new DirectoryInfo(GetConfigArchiveFolder());
                files = directory.GetFiles("*.*").Where(f => f.LastWriteTime < archiveAgeCutoff && Path.GetExtension(f.Name) == ".config");
                foreach (var file in files)
                {
                    file.Delete();
                    ++deletedConfigCount;
                }

                if (deletedLogCount + deletedConfigCount > 0)
                {
                    AddLogEntry($"Deleted {deletedLogCount} log archive files and {deletedConfigCount} config archive files older than {Settings.archivesDeleteOldFilesDays} days");
                }
            }
        }

        private bool StartArchiveAndClear(out DateTime evalStartTime)
        {
            // first make sure an archive or clear option is enabled
            if (!(Settings.archivesArchiveConfig || Settings.archivesDeleteOldFiles || Settings.archivesLogManagement || Settings.archivesClearOldCharts))
            {
                evalStartTime = DateTime.MaxValue;
                return false;
            }
            
            // find next archive time
            DateTime now = DateTime.Now;
            DateTime nextArchiveEval;            
            switch (Settings.archivesArchiveIntervalUnit)
            {
                case "Days":
                    nextArchiveEval = LastArchiveEval.AddDays(Settings.archivesArchiveInterval);
                    break;
                case "Hours":
                    nextArchiveEval = LastArchiveEval.AddHours(Settings.archivesArchiveInterval);
                    break;
                case "Minutes":
                    nextArchiveEval = LastArchiveEval.AddMinutes(Settings.archivesArchiveInterval);
                    break;
                default:
                    throw new Exception("Unknown archive interval unit");
            }

            if (now >= nextArchiveEval)
            {
                evalStartTime = now;
                LastArchiveEval = now;
                return true;
            }
            else
            {
                evalStartTime = nextArchiveEval;
                return false;
            }            
        }

        private void ClearOldChartData(DateTime evalStartTime)
        {
            DateTime timeCutoff = GetTimeCutoff(evalStartTime, Settings.archivesClearOldChartsUnit, Settings.archivesClearOldChartsValue);
            bool logged = false;
            List<DataPoint> points;
            foreach (Chart chart in Charts)
            {
                foreach (Series series in chart.Series)
                {
                    points = series.Points.Where(p => DateTime.FromOADate(p.XValue) <= timeCutoff).ToList();
                    for (int i = points.Count - 1; i >= 0; i--)
                    {
                        if (!logged)
                        {
                            // only log the clearing if we actually found something to clear
                            AddLogEntry($"Clearing chart data older than {timeCutoff}");
                            logged = true;
                        }
                        series.Points.Remove(points[i]);                        
                    }
                }
            }
        }

        private void ArchiveChangedConfig(bool forceArchive = false)
        {
            if (Settings.archivesArchiveConfig && (configNeedsArchiving || forceArchive))
            {
                configNeedsArchiving = false;
                ExportConfig(GetNextConfigArchiveFilePath());
            }
        }

        private string GetNextConfigArchiveFilePath()
        {
            string configArchivePath;
            int i = 1;
            do
            {
                configArchivePath = Path.Combine(GetConfigArchiveFolder(), $"userArchive{i:000}.config");
                ++i;
            }
            while (File.Exists(configArchivePath));
            return configArchivePath;
        }

        private void ArchiveAndClearOldLogs(DateTime evalStartTime, bool archiveAll = false)
        {
            if (Settings.archivesLogManagement && dataGridViewLog.RowCount > 0)
            {
                // find date/time cutoff          
                DateTime timeCutoff = archiveAll ? DateTime.MaxValue : GetTimeCutoff(evalStartTime, Settings.archivesLogManagementUnit, Settings.archivesLogManagementValue);
                
                // find any log entries in scope                
                DataRow[] rowsToActOn = dataTableLog.Select($"Time <= #{timeCutoff}#");

                if (rowsToActOn.Any())
                {
                    StreamWriter writer = null;
                    bool archiveEnabled = Settings.archivesLogManagementType.Contains("Archive");
                    bool clearEnabled = Settings.archivesLogManagementType.Contains("Clear");
                    if (archiveEnabled)
                    {
                        // one archive file per day
                        string archiveFile = Path.Combine(GetArchiveFolder(), $"LogArchive{evalStartTime.ToString("yyyy-MM-dd")}.txt");

                        if (File.Exists(archiveFile))
                        {
                            writer = File.AppendText(archiveFile);
                        }
                        else
                        {
                            writer = File.CreateText(archiveFile);
                        }
                        AddLogEntry($"Archiving and clearing logs older than {timeCutoff}");
                    }
                    else if (clearEnabled)
                    {
                        AddLogEntry($"Clearing logs older than {timeCutoff}");
                    }

                    try
                    {
                        foreach (DataRow row in rowsToActOn)
                        {
                            if (archiveEnabled)
                            {
                                writer.WriteLine($"\"{row[0]}\",\"{row[1]}\",\"{row[2]}\",\"{row[3]}\"");
                            }
                            if (clearEnabled)
                            {
                                row.Delete();
                            }
                        }
                    }
                    finally
                    {
                        writer?.Close();
                        writer?.Dispose();
                    }
                }
            }
        }

        private DateTime GetTimeCutoff(DateTime evalStartTime, string unit, int value)
        {
            switch (unit)
            {
                case "Days":
                    return evalStartTime.AddDays(-value);                    
                case "Hours":
                    return evalStartTime.AddHours(-value);                    
                case "Minutes":
                    return evalStartTime.AddMinutes(-value);                    
                default:
                    throw new Exception("Unknown unit");
            }
        }

        private string GetArchiveFolder()
        {
            string folder = Settings.archivesArchiveFolder;
            if (!Directory.Exists(folder))            
            {
                try
                {
                    if (folder.Length > 0)
                    {
                        Directory.CreateDirectory(folder);
                    }
                    else
                    {
                        folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), typeof(Program).Assembly.GetName().Name);
                        if (!Directory.Exists(folder))
                        {
                            Directory.CreateDirectory(folder);
                        }                        
                    }
                    if (!Directory.Exists(folder))
                    {
                        throw new Exception("Couldn't create a valid archive folder");
                    }
                }
                catch (Exception ex)
                {
                    AddLogEntry($"Exception getting archive folder. Archives may be negatively impacted. Exception: {ex.GetType()} - {ex.Message}", LogType.Error);                    
                }
            }
            return folder;
        }

        private string GetConfigArchiveFolder()
        {
            return Path.Combine(GetArchiveFolder(), "ConfigArchives");
        }


        /// <summary>
        /// Color-codes outdated statistics so user is aware of this
        /// </summary>
        private void UpdateStatColors()
        {
            DateTime lastUpdate;
            foreach (DataGridViewRow row in dataGridViewStats.Rows)
            {
                lastUpdate = (DateTime)(row.Cells[ColStatsLastUpdate.Index].Value);
                if (lastUpdate <= DateTime.Now.AddMinutes(-1.05))
                {
                    // MORE than a minute old
                    row.DefaultCellStyle.ForeColor = Color.LightGray;
                }
                else if (lastUpdate <= (DateTime.Now.AddMilliseconds(-(Settings.tempPollingIntervalMillisecs * 2))))
                {                    
                    // 2 polling cycles old
                    row.DefaultCellStyle.ForeColor = Color.Gray;
                }
            }
        }

        private void UpdateMinerState(bool isGPU)
        {
            if (Settings.controlRunning && Settings.minerEnableAutomation)
            {
                // find state based on the miner mode
                // Note: just gathering the state info and not applying it yet. we need to account for it in the conditions after before deciding our final state.
                MinerMode minerMode = isGPU ? (MinerMode)Settings.minerGPUMode : (MinerMode)Settings.minerCPUMode;
                MinerState minerState = MinerState.Uninitialized;
                string reasonToLog = "";
                switch (minerMode)
                {
                    case MinerMode.AlwaysOff:
                        minerState = MinerState.DisabledByUser;
                        break;
                    case MinerMode.AlwaysOn:
                        minerState = MinerState.Running;
                        break;
                    case MinerMode.DontControl:
                        // do nothing                       
                        break;
                    case MinerMode.Schedule:
                        if (isGPU)
                        {
                            if ((ScheduleGPU != null) && ScheduleGPU.Evaluate().Count > 0)
                            {
                                minerState = ScheduleGPU.LastEvaluatedActions.Last() == ScheduleAction.MinerOn ? MinerState.Running : MinerState.DisabledBySchedule;
                                reasonToLog = "as per its schedule";
                            }
                            else
                            {
                                minerState = MinerState.DisabledUnknownError;
                                reasonToLog = "due to missing or incomplete schedule";
                            }
                        }
                        else
                        {
                            if ((ScheduleCPU != null) && ScheduleCPU.Evaluate().Count > 0)
                            {
                                minerState = ScheduleCPU.LastEvaluatedActions.Last() == ScheduleAction.MinerOn ? MinerState.Running : MinerState.DisabledBySchedule;
                                reasonToLog = "as per its schedule";
                            }
                            else
                            {
                                minerState = MinerState.DisabledUnknownError;
                                reasonToLog = "due to missing or incomplete schedule";
                            }
                        }
                        break;
                    default:
                        minerState = MinerState.DisabledUnknownError;
                        reasonToLog = "due to unknown state";
                        break;
                }

                // GPU failsafes
                if (isGPU)
                {
                    // note: check overheating FIRST every time, since it's complex and it manages variables
                    if (Settings.tempStopWhenOverheat)
                    {
                        DateTime now = DateTime.Now;
                        if (GPUOverheatShutoffTime != DateTime.MinValue)
                        {
                            // we're already shut off due to overheating
                            if (now > GPUOverheatShutoffTime.AddSeconds(Settings.tempStopWhenOverheatSecs))
                            {
                                // overheating wait time exceeded, reset it
                                GPUOverheatShutoffTime = DateTime.MinValue;
                            }
                            else
                            {
                                // inside overheating wait time, ensure we're shut off
                                if (minerState == MinerState.Running)
                                {
                                    SyncMinerState(isGPU, MinerState.DisabledByOverheating, "due to overheating");
                                    return;
                                }
                            }
                        }
                        else
                        {                            
                            if (!(new[] { Const.UnknownTemp, string.Empty }).Contains(GetStat(cGPUMemJuncTemp)))
                            {
                                if ((GetStat(cGPUMemJuncTemp) != Const.BlankInfo) && (Convert.ToInt32(GetStat(cGPUMemJuncTemp)) > Settings.tempMax) && (Settings.tempSpeedStep == 1))
                                {
                                    // we're overheated: temp is over the max and we're at lowest power
                                    if (GPUOverheatStartTime == DateTime.MinValue)
                                    {
                                        // we weren't overheated before, so record the start time
                                        GPUOverheatStartTime = now;
                                    }
                                    else if (now > GPUOverheatStartTime.AddSeconds(Settings.tempStopWhenOverheatThresholdSecs))
                                    {
                                        // we were overheated before, and we've exceeded the threshold for shutoff
                                        // reset the threshold time and start the shutoff timer
                                        GPUOverheatStartTime = DateTime.MinValue;
                                        GPUOverheatShutoffTime = now;
                                        // inside overheating wait time, ensure we're shut off
                                        if (minerState == MinerState.Running)
                                        {
                                            SyncMinerState(isGPU, MinerState.DisabledByOverheating, "due to overheating");
                                        }                                        
                                        return;
                                    }
                                }
                                else
                                {
                                    // we're not overheated, so reset the start time
                                    GPUOverheatStartTime = DateTime.MinValue;
                                }
                            }
                        }                        
                    }

                    if (Settings.tempStopWhenTempUnknown && (GetStat(cGPUMemJuncTemp) == Const.UnknownTemp) && (minerState == MinerState.Running))
                    {
                        // inside overheating wait time, ensure we're shut off                        
                        SyncMinerState(isGPU, MinerState.DisabledByUnknownTemp, "due to unknown temp");
                        return;                        
                    }                    
                }                

                // user activity -- last in priority, so we only disable if nobody else did
                if (isGPU && Settings.minerGPUUserActivityShutoff && (minerState == MinerState.Running) && (LastUserInput.GetLastInputTimeInSecs() < (Settings.minerUserActivityTimeoutMins * 60)))
                {
                    SyncMinerState(isGPU, MinerState.DisabledByUserActivity, "due to user activity");
                    return;
                }
                if (!isGPU && Settings.minerCPUUserActivityShutoff && (minerState == MinerState.Running) && (LastUserInput.GetLastInputTimeInSecs() < (Settings.minerUserActivityTimeoutMins * 60)))
                {                    
                    SyncMinerState(isGPU, MinerState.DisabledByUserActivity, "due to user activity");
                    return;
                }

                // default to normal miner mode
                if (minerState != MinerState.Uninitialized)
                    SyncMinerState(isGPU, minerState, reasonToLog);
            }
            else
            {
                SyncMinerState(isGPU, MinerState.DisabledByUser, "");
            }
        }

        /// <summary>
        /// Synchronizes miner state with input values. ONLY does work if the input values would change miner state.
        /// </summary>
        /// <param name="doOnGPU">Act on GPU miner if true, CPU miner if false</param>
        /// <param name="newMinerState">Desired state of the miner</param>
        /// <param name="reasonToLog">Explanation for the state change to be logged. If blank, no reason will be logged</param>
        private void SyncMinerState(bool doOnGPU, MinerState newMinerState, string reasonToLog)
        {            
            bool runMiner = newMinerState == MinerState.Running;    
            if (doOnGPU)
            {
                GPUState = newMinerState;                
                if (runMiner && !ProcessUtils.IsProcessRunningFromObject(ProcessGPUMiner))
                {
                    if (reasonToLog.Length > 0)                    
                        AddLogEntry($"GPU Miner launching {reasonToLog}");
                                        
                    LaunchProcess(ProcessGPUMiner, Settings.appGPUMinerPath, Settings.appGPUMinerName, 
                        "GPU Miner", ref isGPUMinerRunning, cGPUMinerIndex, true);
                }
                else if (!runMiner && ProcessUtils.IsProcessRunningByName(Settings.appGPUMinerName))
                {
                    if (reasonToLog.Length > 0)
                        AddLogEntry($"GPU Miner closing {reasonToLog}");
                                        
                    CloseProcess(ProcessGPUMiner, Settings.appGPUMinerPath, Settings.appGPUMinerName, "GPU Miner", cGPUMinerIndex, ref isGPUMinerRunning);
                }
            }
            else
            {
                CPUState = newMinerState;                
                if (runMiner && !ProcessUtils.IsProcessRunningFromObject(ProcessCPUMiner))
                {
                    if (reasonToLog.Length > 0)
                        AddLogEntry($"CPU Miner launching {reasonToLog}");
                                        
                    LaunchProcess(ProcessCPUMiner, Settings.appCPUMinerPath, Settings.appCPUMinerName,
                        "CPU Miner", ref isCPUMinerRunning, cCPUMinerIndex, true);
                }
                else if (!runMiner && ProcessUtils.IsProcessRunningByName(Settings.appCPUMinerName))
                {
                    if (reasonToLog.Length > 0)
                        AddLogEntry($"CPU Miner closing {reasonToLog}");
                                        
                    CloseProcess(ProcessCPUMiner, Settings.appCPUMinerPath, Settings.appCPUMinerName, "CPU Miner", cCPUMinerIndex, ref isCPUMinerRunning);
                }
            }
            SysTrayIcon.UpdateTextIcon(notifyIconMain, false, GPUState, CPUState);
        }

        /// <summary>
        /// Updates stats for items controlled on a polling cycle
        /// </summary>
        private void UpdatePolledStats()
        {
            // current GPU power step (always shown)
            if (Metric(cGPUPowerStep).IsEnabled)
            {
                SetStat(cGPUPowerStep, Settings.tempSpeedStep.ToString());
            }
            
            // average GPU power step
            // calculate average
            if (Series(cGPUPowerStep).Points.Count > 0 && Metric(cGPUPowerStep).IsEnabled)
            {
                SetStat(cAvgGPUPowerStep, CalculateAverage(Series(cGPUPowerStep), CalculationMethod.Lookahead).ToString());                
            }

            // current GPU temp - covered elsewhere            

            // average GPU temp
            if (Series(cGPUMemJuncTemp).Points.Count > 0 && Metric(cGPUMemJuncTemp).IsEnabled)
            {
                SetStat(cAvgGPUMemJuncTemp, CalculateAverage(Series(cGPUMemJuncTemp), CalculationMethod.Lookahead).ToString());
            }

            // average GPU hash rate
            if (Series(cGPUHashRate).Points.Count > 0 && Metric(cGPUHashRate).IsEnabled)
            {
                SetStat(cAvgGPUHashRate, CalculateAverage(Series(cGPUHashRate), CalculationMethod.Lookbehind).ToString());
            }

            // CPU temp and average
            if (Series(cCPUTemp).Points.Count > 0 && Metric(cCPUTemp).IsEnabled)
            {
                SetStat(cCPUTemp, Metric(cCPUTemp).NumericResult.ToString());
                SetStat(cAvgCPUTemp, CalculateAverage(Series(cCPUTemp), CalculationMethod.Lookahead).ToString());
            }

            // average CPU hash rate
            if (Series(cCPUHashRate).Points.Count > 0 && Metric(cCPUHashRate).IsEnabled)
            {
                SetStat(cAvgCPUHashRate, CalculateAverage(Series(cCPUHashRate), CalculationMethod.Lookbehind).ToString());
            }

            // total power
            if (Metric(cTotalPower).IsEnabled)
            {
                SetStat(cTotalPower, Metric(cTotalPower).NumericResult.ToString());
            }

            // average total power and kWh
            if (Series(cTotalPower).Points.Count > 0 && Metric(cTotalPower).IsEnabled)
            {
                SetStat(cAvgTotalPower, CalculateAverage(Series(cTotalPower), CalculationMethod.Lookbehind).ToString());
                // kWh = sum of watts per second / 60 (seconds in a minute) * 60 (minutes in an hour) * 1000 (kilo)
                SetStat(cTotalPowerUse, CalculateRate(Series(cTotalPower), 60 * 60 * 1000, CalculationMethod.Lookbehind).ToString());
            }

            // current GPU power
            if (Metric(cGPUPower).IsEnabled)
            {
                SetStat(cGPUPower, Metric(cGPUPower).NumericResult.ToString());
            }

            // average GPU power and kWh
            if (Series(cGPUPower).Points.Count > 0 && Metric(cGPUPower).IsEnabled)
            {
                SetStat(cAvgGPUPower, CalculateAverage(Series(cGPUPower), CalculationMethod.Lookbehind).ToString());
                // kWh = sum of watts per second / 60 (seconds in a minute) * 60 (minutes in an hour) * 1000 (kilo)
                SetStat(cGPUPowerUse, CalculateRate(Series(cGPUPower), 60 * 60 * 1000, CalculationMethod.Lookbehind).ToString());
            }

            // current GPU power
            if (Metric(cCPUPower).IsEnabled)
            {
                SetStat(cCPUPower, Metric(cCPUPower).NumericResult.ToString());
            }

            // average GPU power and kWh
            if (Series(cCPUPower).Points.Count > 0 && Metric(cCPUPower).IsEnabled)
            {
                SetStat(cAvgCPUPower, CalculateAverage(Series(cCPUPower), CalculationMethod.Lookbehind).ToString());
                // kWh = sum of watts per second / 60 (seconds in a minute) * 60 (minutes in an hour) * 1000 (kilo)
                SetStat(cCPUPowerUse, CalculateRate(Series(cCPUPower), 60 * 60 * 1000, CalculationMethod.Lookbehind).ToString());
            }
            
            // runtime (elapsed time since application start)         
            TimeSpan runtime = DateTime.Now - StartupTimestamp;                        
            string formattedRuntime = String.Format("{0:0}D {1:00}:{2:00}:{3:00}.{4:000}",
                runtime.Days, runtime.Hours, runtime.Minutes, runtime.Seconds, runtime.Milliseconds);    
            SetStat(cRuntime, formattedRuntime);
        }
        
        private (double, double) CalculateAreaAndTotalTime(Series series, CalculationMethod calculationMethod)
        {            
            if (series == null || !series.Points.Any())
            {
                return (double.NaN, double.NaN);
            }

            // get total time and x+y area            
            DataPoint previousPoint = null;
            double totalArea = 0.0;
            double totalTime = 0.0;
            double lastTime;
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

            return (totalArea, totalTime);
        }

        private double CalculateAverage(Series series, CalculationMethod calculationMethod)
        {            
            double totalArea;
            double totalTime;
            (totalArea, totalTime) = CalculateAreaAndTotalTime(series, calculationMethod);
            
            // calculate average as (previous total area + total area since last data point [if doing Lookahead]) / total elapsed time            
            return Math.Round(totalArea / totalTime, 2);
        }

        private object CalculateRate(Series series, double denominator, CalculationMethod calculationMethod)
        {
            double totalArea;
            double totalTime;
            (totalArea, totalTime) = CalculateAreaAndTotalTime(series, calculationMethod);

            // calculate rate as (previous total area + total area since last data point [if doing Lookahead]) / denominator
            return Math.Round(totalArea / denominator, 3);
        }

        private void UpdateChartScales(bool includeGPUPowerStep, bool forceUpdate = false)
        {   
            // don't bother updating scales if user won't see the chart
            if (!forceUpdate && (WindowState == FormWindowState.Minimized || tabControlMain.SelectedTab != tabPageAnalytics))
            {
                return;
            }

            ChartUtils.UpdateChartAxisScale(Chart(cGPU), AxisType.Secondary,
                Settings.chartMinTempOnYAxisEnabled ? new(Series, int)[] {(Series(cGPUMemJuncTemp), Settings.chartMinTempOnYAxisValue)} : null);

            if (includeGPUPowerStep)
            {
                // always set GPU power step axis to the available power steps
                ChartUtils.SetChartAxisScale(Chart(cGPU).ChartAreas[0].AxisY, trackBarGPUPowerStep.Minimum, trackBarGPUPowerStep.Maximum, Chart(cGPU).Height);                
            }

            // update CPU axes to fit current data            
            ChartUtils.UpdateChartAxisScale(Chart(cCPU));

            ChartUtils.UpdateChartAxisScale(Chart(cResources));
        }

        private void UpdatePolledMetrics()
        {
            // GPU power step
            Metric(cGPUPowerStep).UpdateFromInput(Settings.tempSpeedStep.ToString());

            // all systray metrics
            string sysTrayToolbarText = SysTrayTooltipReader.GetAllSysTrayToolbarText();
            foreach (Metric metric in Metrics)
            {
                // note: GPU mem junc temp is covered elsewhere
                if ((metric.IsEnabled || metric.IsInternal) && (metric.Source == MetricSource.SysTray) && (metric.Name != cGPUMemJuncTemp))
                {
                    metric.UpdateFromInput(sysTrayToolbarText);
                }
            }          
        }

        /// <summary>
        /// Retrieves and displays GPU temp
        /// </summary>
        /// <returns>True if temp was retrieved and updated on display</returns>
        private bool UpdateGPUTemp()
        {
            Metric metric = Metric(cGPUMemJuncTemp);
            if (metric.UpdateFromInput(SysTrayTooltipReader.GetAllSysTrayToolbarText()))
            {
                SetStat(cGPUMemJuncTemp, metric.NumericResult.ToString());
                return true;
            }            
            else
            {
                SetStat(cGPUMemJuncTemp, Const.UnknownTemp);                
                return false;
            }
        }

        /// <summary>
        /// Attempts to create schedule node(s) from the current UI values within the parentNodes collection
        /// </summary>
        /// <param name="parentNodes">New node will be created as a sub-node of this collection</param>
        private void CreateScheduleNodeFromUI(TreeNodeCollection parentNodes, TreeNode parentNode)
        {
            try
            {
                Schedule schedule = SelectedSchedule;
                Guid parentId = parentNode == null ? Guid.Empty : (Guid)parentNode.Tag;

                ScheduleNode scheduleNode = null;
                TreeNode treeNode = null;                
                if (parentNodes.Count == 0)
                {
                    if (radioButtonScheduleCalendar.Checked || radioButtonScheduleDaysOfWeek.Checked || radioButtonScheduleTime.Checked)
                    {
                        if (radioButtonScheduleCalendar.Checked)
                        {
                            scheduleNode = CreateCalendarNodeFromUI();
                        }
                        else if (radioButtonScheduleDaysOfWeek.Checked)
                        {
                            scheduleNode = CreateWeekNodeFromUI();
                        }
                        else if (radioButtonScheduleTime.Checked)
                        {
                            scheduleNode = CreateTimeNodeFromUI();
                        }
                        schedule.AddNode(parentId, scheduleNode, null);
                        treeNode = parentNodes.Add(schedule.GetNodeDescription(scheduleNode?.Id));
                        treeNode.Tag = scheduleNode?.Id;

                        scheduleNode = new ElseNode(Guid.Empty);
                        schedule.AddNode(parentId, scheduleNode, null);
                        treeNode = parentNodes.Add(schedule.GetNodeDescription(scheduleNode?.Id));
                        treeNode.Tag = scheduleNode?.Id;
                    }
                    else if (radioButtonScheduleResult.Checked)
                    {
                        scheduleNode = CreateActionNodeFromUI();
                        schedule.AddNode(parentId, scheduleNode, null);
                        treeNode = parentNodes.Add(schedule.GetNodeDescription(scheduleNode?.Id));
                        treeNode.Tag = scheduleNode?.Id;
                    }

                    // reload to show the new node
                    LoadScheduleToTreeView(schedule, treeViewSchedule.SelectedNode == null ? Guid.Empty : (Guid)treeViewSchedule.SelectedNode.Tag);
                }
                else
                {                    
                    if (radioButtonScheduleCalendar.Checked || radioButtonScheduleDaysOfWeek.Checked || radioButtonScheduleTime.Checked)
                    {
                        // we have a next node (a node we're inserting before) only if a node is selected AND it has the same parent.
                        ScheduleNode nextNode = null;
                        if ((treeViewSchedule.SelectedNode != null) && (treeViewSchedule.SelectedNode.Parent == parentNode))
                        { 
                            nextNode = schedule.GetNodeById((Guid)treeViewSchedule.SelectedNode.Tag);
                        }

                        if (radioButtonScheduleCalendar.Checked)
                        {
                            scheduleNode = CreateCalendarNodeFromUI();
                        }
                        else if (radioButtonScheduleDaysOfWeek.Checked)
                        {
                            scheduleNode = CreateWeekNodeFromUI();
                        }
                        else if (radioButtonScheduleTime.Checked)
                        {
                            scheduleNode = CreateTimeNodeFromUI();
                        }
                        schedule.AddNode(parentId, scheduleNode, nextNode);

                        // reload to show the new node
                        LoadScheduleToTreeView(schedule, nextNode == null ? Guid.Empty : nextNode.Id);
                    }
                    else if (radioButtonScheduleResult.Checked)
                    {
                        MessageBox.Show("Result nodes can't exist alongside other nodes at the same level. Add as a standalone sub-node instead).");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Node not created, or created incorrectly, due to an exception: \n{ex.GetType()}\n{ex.Message}");
            }
        }

        private CalendarNode CreateCalendarNodeFromUI()
        {
            int startDay = comboBoxScheduleStartDay.Text == "Last" ? 32 : Convert.ToInt32(comboBoxScheduleStartDay.Text);
            int endDay = comboBoxScheduleEndDay.Text == "Last" ? 32 : Convert.ToInt32(comboBoxScheduleEndDay.Text);
            CalendarNode scheduleNode = new CalendarNode(Guid.Empty,
                Convert.ToInt32(comboBoxScheduleStartMonth.SelectedIndex + 1),
                Convert.ToInt32(comboBoxScheduleEndMonth.SelectedIndex + 1),
                startDay,
                endDay);
            return scheduleNode;
        }

        private WeekNode CreateWeekNodeFromUI()
        {
            return new WeekNode(Guid.Empty,
                checkBoxScheduleSunday.Checked,
                checkBoxScheduleMonday.Checked,
                checkBoxScheduleTuesday.Checked,
                checkBoxScheduleWednesday.Checked,
                checkBoxScheduleThursday.Checked,
                checkBoxScheduleFriday.Checked,
                checkBoxScheduleSaturday.Checked);
        }

        private TimeNode CreateTimeNodeFromUI()
        {
            return new TimeNode(Guid.Empty, dateTimePickerScheduleStartTime.Value, dateTimePickerScheduleEndTime.Value);
        }

        private ActionNode CreateActionNodeFromUI()
        {
            return new ActionNode(Guid.Empty, comboBoxScheduleResult.SelectedIndex == 0 ? ScheduleAction.MinerOn : ScheduleAction.MinerOff);
        }

        /// <summary>
        /// Returns the schedule currently displayed on Schedules tab. Creates new schedule if no schedule is selected by the user.
        /// </summary>        
        private Schedule SelectedSchedule
        {
            get
            {                
                if ((comboBoxScheduleSchedules.SelectedIndex < 0))
                {
                    if (Schedules.Count == 0)
                    {
                        // create a schedule if none exist
                        AddNewSchedule();
                    }
                    else
                    { 
                        comboBoxScheduleSchedules.SelectedIndex = Schedules.Count - 1;
                    }
                }

                return Schedules[comboBoxScheduleSchedules.SelectedIndex];
            }
        }

        private Schedule ScheduleGPU 
        { 
            get 
            {
                try
                {
                    return Schedules.First(x => x.Id.ToString() == Settings.minerGPUSchedule.ToString());
                }
                catch
                {
                    return null;
                }
            } 
        }
        private Schedule ScheduleCPU 
        { 
            get
            {
                try
                {
                    return Schedules.First(x => x.Id.ToString() == Settings.minerCPUSchedule.ToString());
                }
                catch
                {
                    return null;
                }                
            }
        }

        private void AddNewSchedule()
        {
            Schedules.Add(new Schedule() { Name = "Unnamed Schedule" });
            comboBoxScheduleSchedules.SelectedIndex = Schedules.Count - 1;
            SaveSchedulesFromList();
        }

        private void UpdateScheduleDaysComboBoxesFromUI()
        {
            string currentDayText;
            if (comboBoxScheduleStartMonth.SelectedIndex >= 0)
            {
                currentDayText = comboBoxScheduleStartDay.Text;
                StartMonthDays = Enumerable.Range(1, DateTime.DaysInMonth(DateTime.Now.Year, comboBoxScheduleStartMonth.SelectedIndex + 1)).Select(x => x.ToString()).Append("Last").ToArray();
                comboBoxScheduleStartDay.DataSource = StartMonthDays;
                if (currentDayText != string.Empty && StartMonthDays.Contains(currentDayText))
                {
                    comboBoxScheduleStartDay.Text = currentDayText;
                }
                else
                {
                    comboBoxScheduleStartDay.Text = string.Empty;
                }
            }

            currentDayText = comboBoxScheduleEndDay.Text;
            if (comboBoxScheduleEndMonth.SelectedIndex >= 0)
            {
                EndMonthDays = Enumerable.Range(1, DateTime.DaysInMonth(DateTime.Now.Year, comboBoxScheduleEndMonth.SelectedIndex + 1)).Select(x => x.ToString()).Append("Last").ToArray();
                comboBoxScheduleEndDay.DataSource = EndMonthDays;
                if (currentDayText != string.Empty && EndMonthDays.Contains(currentDayText))
                {
                    comboBoxScheduleEndDay.Text = currentDayText;
                }
                else
                {
                    comboBoxScheduleEndDay.Text = string.Empty;
                }
            }
        }

        /// <summary>
        /// Load the given schedule into the tree view, replacing its previous contents
        /// </summary>
        /// <param name="schedule"></param>
        /// <param name="selectedNodeId">ID of the node to select (optional)</param>
        private void LoadScheduleToTreeView(Schedule schedule, Guid? selectedNodeId = null)
        {            
            treeViewSchedule.Nodes.Clear();
            LoadNodesToTreeView(treeViewSchedule.Nodes, schedule.Nodes, schedule);
            if (!selectedNodeId.Equals(Guid.Empty))
            {
                SelectTreeNodeByScheduleNodeId(treeViewSchedule.Nodes, selectedNodeId);
            }
        }

        private void LoadNodesToTreeView(TreeNodeCollection treeNodes, List<ScheduleNode> scheduleNodes, Schedule schedule)
        {
            foreach (ScheduleNode node in scheduleNodes)
            {
                TreeNode treeNode = treeNodes.Add(schedule.GetNodeDescription(node.Id));
                treeNode.Tag = node.Id;

                if (node is BranchingNode)
                {
                    LoadNodesToTreeView(treeNode.Nodes, ((BranchingNode)node).Children, schedule);
                }
            }

            treeViewSchedule.ExpandAll();
        }

        private bool SelectTreeNodeByScheduleNodeId(TreeNodeCollection treeNodes, Guid? selectedNodeId)
        {
            foreach (TreeNode node in treeNodes)
            {                
                if (((Guid)node.Tag).Equals(selectedNodeId))
                {
                    treeViewSchedule.SelectedNode = node;
                    return true;
                }
                if (node.Nodes.Count > 0 && SelectTreeNodeByScheduleNodeId(node.Nodes, selectedNodeId))
                {
                    return true;
                }                
            }
            return false;
        }

        private void ImportConfig(string sourceFilePath)
        {
            try
            {
                string destFilePath = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath;
                if (!File.Exists(destFilePath))
                {
                    destFilePath = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoaming).FilePath;
                }
                File.Copy(sourceFilePath, destFilePath, true);

                // reload from storage
                Settings.Reload();

                // apply
                LoadSettingsToUI(true);
                AddLogEntry($"Config imported from '{sourceFilePath}' to '{destFilePath}'");
                ShowTooltipNotification("Config imported successfully");

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error importing config: {ex.GetType()} - {ex.Message}");
            }
        }

        private void ExportConfig(string destFilePath)
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
                config.SaveAs(destFilePath);
                ShowTooltipNotification("Config exported successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting config: {ex.GetType()} - {ex.Message}");
            }
        }

        private void ChartShowConfigChanged(object sender, EventArgs e)
        {
            foreach (Chart chart in Charts)
            {
                if (radioButtonChartShowAll.Checked)
                {
                    chart.ChartAreas[0].AxisX.Minimum = Double.NaN;
                    chart.ChartAreas[0].AxisX.Maximum = Double.NaN;
                }
                else if (radioButtonChartShowLastX.Checked)
                {
                    switch (comboBoxChartShowLastUnit.SelectedItem.ToString())
                    {
                        case "Minutes":
                            chart.ChartAreas[0].AxisX.Minimum = DateTime.Now.AddMinutes(-(int)numericUpDownChartShowLastX.Value).ToOADate();
                            break;
                        case "Hours":
                            chart.ChartAreas[0].AxisX.Minimum = DateTime.Now.AddHours(-(int)numericUpDownChartShowLastX.Value).ToOADate();
                            break;
                        case "Days":
                            chart.ChartAreas[0].AxisX.Minimum = DateTime.Now.AddDays(-(int)numericUpDownChartShowLastX.Value).ToOADate();
                            break;

                    }
                }
            }

            UpdateChartScales(true, true);
        }

        private void OpenLink(string link)
        {
            using (Process p = new Process())
            {
                p.StartInfo.UseShellExecute = true;
                p.StartInfo.FileName = link;
                p.Start();
            }
        }

        private void PromptToSelectAppPathForRow(int rowIndex)
        {
            // provide user the option of setting the app path
            if (openFileDialogAppPath.ShowDialog() == DialogResult.OK)
            {
                dataGridViewApps.Rows[rowIndex].Cells[ColAppPath.Index].Value = openFileDialogAppPath.FileName;

                // if name is empty, default to the application name without extension
                if (dataGridViewApps.Rows[rowIndex].Cells[ColAppName.Index].Value.ToString().Trim() == string.Empty)
                {
                    dataGridViewApps.Rows[rowIndex].Cells[ColAppName.Index].Value = Path.GetFileNameWithoutExtension(openFileDialogAppPath.FileName);
                }

                SaveSettingsFromUI();
            }
        }

        private Metric Metric(string metricName)
        {
            try
            {
                return Metrics.First(x => (x.Name == metricName));
            }
            catch
            {
                return null;
            }
        }

        private Chart Chart(string chartName)
        {
            try
            {
                return Charts.First(x => (x.Name == chartName));
            }
            catch
            {
                return null;
            }
        }
                
        private Series Series(string metricName)
        {
            try
            {
                return Metrics.First(x => (x.Name == metricName)).Series;
            }
            catch
            {
                return null;
            }
        }

        private void timerMain_Tick(object sender, EventArgs e)
        {
            DoPollingCycle();            
        }   
      
        private void dataGridViewApps_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == ColAppPath.Index)
            {
                PromptToSelectAppPathForRow(e.RowIndex);
            }
        }        

        private void dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            SaveSettingsFromUI();
        }

        private void SettingChanged(object sender, EventArgs e)
        {
            SaveSettingsFromUI();
        }

        private void buttonStartAutomation_Click(object sender, EventArgs e)
        {
            StartAutomation();            
        }

        private void buttonStopAutomation_Click(object sender, EventArgs e)
        {
            StopAutomation();
        }

        private void FormMineControl_Shown(object sender, EventArgs e)
        {            
            InitializeAutomationFromSavedState();
            if (Settings.controlStartupMinimize)
            {
                Hide();
            }
            InitializeInternals();
        }

        private void trackBarGPUPowerStep_Scroll(object sender, EventArgs e)
        {
            AddLogEntry(string.Format("User stepping GPU power to {0}", trackBarGPUPowerStep.Value));
            Series(cGPUPowerStep).Points.AddXY(DateTime.Now, trackBarGPUPowerStep.Value);
            StepGPUPower(trackBarGPUPowerStep.Value);            
        }

        private void FormMineControl_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Settings.controlMinimizeToSysTray && !CanQuit)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void toolStripMenuItemSysTrayExit_Click(object sender, EventArgs e)
        {
            CanQuit = true; // only allow quit through systray
            Application.Exit();
        }

        private void FormMineControl_Resize(object sender, EventArgs e)
        {            
            if (Settings.controlMinimizeToSysTray && (WindowState == FormWindowState.Minimized))
            {
                Hide();                
            }
        }

        private void FormMineControl_FormClosed(object sender, FormClosedEventArgs e)
        {
            // just clean up timed tasks and apps, since stopping automation fully would change the config
            timerMain.Enabled = false;
            SyncMinerState(true, MinerState.DisabledClosing, "due to application shutdown");
            SyncMinerState(false, MinerState.DisabledClosing, "due to application shutdown");

            // archive everything remaining on exit
            DateTime now = DateTime.Now;
            ArchiveAndClearOldLogs(now, true);
            ArchiveChangedConfig();

            // dispose global IDisposables
            ProcessGPUMiner.Dispose();
            ProcessCPUMiner.Dispose();
            ProcessHardwareMonitor.Dispose();
            ProcessGPUController.Dispose();
            bindingSourceSchedules.Dispose();
            bindingSourceGPUSchedule.Dispose();
            bindingSourceCPUSchedule.Dispose();
            job.Dispose();
        }

        private void notifyIconMain_Open(object sender, EventArgs e)
        {            
            // We make sure window is visible by minimizing and re-displaying in its previous state, or Normal if it was minimized. 
            FormWindowState oldState = WindowState;            
            WindowState = FormWindowState.Minimized;            
            Show();
            WindowState = oldState == FormWindowState.Minimized ? FormWindowState.Normal : oldState;            
        }

        private void buttonChartClearData_Click(object sender, EventArgs e)
        {           
            if (MessageBox.Show(this, "Clear ALL data in ALL charts?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // clear chart points
                foreach (Chart chart in Charts)
                {
                    foreach (Series series in chart.Series)
                    {
                        series.Points.Clear();
                    }
                }
            }
        }

        private void chartMain_GetToolTipText(object sender, ToolTipEventArgs e)
        {
            if (e.HitTestResult.Object is DataPoint)
            {
                DataPoint point = (e.HitTestResult.Object as DataPoint);
                e.Text = string.Format("Series: {0}\nX: {1}\nY: {2}", e.HitTestResult.Series.Name, DateTime.FromOADate(point.XValue), point.YValues[0]);
            }
        }

        private void buttonScheduleCreateNode_Click(object sender, EventArgs e)
        {                   
            if ((treeViewSchedule.Nodes.Count == 0) || (treeViewSchedule.SelectedNode == null))
            {
                CreateScheduleNodeFromUI(treeViewSchedule.Nodes, null);                               
            }
            else if (treeViewSchedule.SelectedNode != null)
            {
                if (treeViewSchedule.SelectedNode.Parent == null)
                {
                    CreateScheduleNodeFromUI(treeViewSchedule.Nodes, null);                    
                }
                else
                {
                    CreateScheduleNodeFromUI(treeViewSchedule.SelectedNode.Parent.Nodes, treeViewSchedule.SelectedNode.Parent);                             
                }
            }
            SaveSchedulesFromList();
        }

        private void buttonScheduleCreateSubNode_Click(object sender, EventArgs e)
        {            
            if (treeViewSchedule.SelectedNode != null)
            {
                CreateScheduleNodeFromUI(treeViewSchedule.SelectedNode.Nodes, treeViewSchedule.SelectedNode);
                SaveSchedulesFromList();
            }
        }

        private void radioButtonScheduleType_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxScheduleCalendar.Visible = radioButtonScheduleCalendar.Checked;
            groupBoxScheduleWeek.Visible = radioButtonScheduleDaysOfWeek.Checked;
            groupBoxScheduleTime.Visible = radioButtonScheduleTime.Checked;
            groupBoxScheduleResult.Visible = radioButtonScheduleResult.Checked;
            PositionNodeButtons();
        }
        
        private void buttonScheduleUpdateNode_Click(object sender, EventArgs e)
        {
            //TODO

            SaveSchedulesFromList();
        }

        private void buttonScheduleDeleteNode_Click(object sender, EventArgs e)
        {
            if (treeViewSchedule.SelectedNode == null)
            {
                MessageBox.Show("No node is selected to delete.");
            }
            else
            {                
                if (MessageBox.Show("Any children will be deleted, and nodes at the same level will be deleted if this is an 'else' node or a standalone 'if' node. Continue?", 
                    this.Text, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (SelectedSchedule.DeleteNode((Guid)treeViewSchedule.SelectedNode.Tag))
                    {
                        SaveSchedulesFromList();

                        // reload to show the deletion(s)
                        LoadScheduleToTreeView(SelectedSchedule);
                    }
                    else
                    {
                        MessageBox.Show("Couldn't delete the node for an unknown reason.");
                    }                    
                }
            }
        }

        private void textBoxScheduleName_TextChanged(object sender, EventArgs e)
        {
            SelectedSchedule.Name = textBoxScheduleName.Text.Trim();
            if (SelectedSchedule.Name != comboBoxScheduleSchedules.Text)
            {
                bindingSourceSchedules.ResetBindings(false);
                bindingSourceGPUSchedule.ResetBindings(false);
                bindingSourceCPUSchedule.ResetBindings(false);
            }
        }

        private void buttonScheduleCreateSchedule_Click(object sender, EventArgs e)
        {
            AddNewSchedule();
            SaveSchedulesFromList();
        }

        private void textBoxScheduleName_Leave(object sender, EventArgs e)
        {
            SaveSchedulesFromList();
        }

        private void comboBoxScheduleSchedules_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxScheduleName.Text = SelectedSchedule.Name;
            LoadScheduleToTreeView(SelectedSchedule);
        }

        private void buttonScheduleDuplicateSchedule_Click(object sender, EventArgs e)
        {
            // duplicate the schedule with new name and internal IDs
            Schedules.Add(JsonSerializer.Deserialize<Schedule>(SelectedSchedule.Serialize(jsonOptionsScheduleNodes), jsonOptionsScheduleNodes));
            Schedules.Last().RegenerateIds();
            Schedules.Last().Name += " (Copy)";      
            
            // save and select
            SaveSchedulesFromList();
            comboBoxScheduleSchedules.SelectedIndex = Schedules.Count - 1;                
        }

        private void buttonScheduleDeleteSchedule_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Delete selected schedule?", this.Text, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Schedules.Remove(SelectedSchedule);
                SaveSchedulesFromList();
                if (Schedules.Count > 0)
                {
                    comboBoxScheduleSchedules.SelectedIndex = Schedules.Count - 1;
                }
                else
                {
                    AddNewSchedule();
                }
            }
        }

        private void comboBoxLogFilter_Changed(object sender, EventArgs e)
        {
            if ((comboBoxLogFilter.Text.Trim() == string.Empty) || (comboBoxLogFilter.Text.ToLower().Trim() == "none"))
            {
                ((DataTable)dataGridViewLog.DataSource).DefaultView.RowFilter = "";
            }
            else
            {
                try
                {
                    ((DataTable)dataGridViewLog.DataSource).DefaultView.RowFilter = comboBoxLogFilter.Text;
                }
                catch
                {
                    MessageBox.Show("Error applying selected filter");
                    ((DataTable)dataGridViewLog.DataSource).DefaultView.RowFilter = "";
                }
            }
            ScrollLogToEnd();
        }

        private void dataGridViewLog_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (checkBoxLogColorCode.Checked)
            {
                Color color;
                switch (dataGridViewLog.Rows[e.RowIndex].Cells[1].Value.ToString())
                {
                    case "Info":
                        switch (dataGridViewLog.Rows[e.RowIndex].Cells[0].Value.ToString())
                        {
                            case "MineControl":
                                color = Color.Black;
                                break;
                            case "GPU Miner":
                                color = Color.DarkGray;
                                break;
                            case "CPU Miner":
                                color = Color.Gray;
                                break;
                            default:
                                color = Color.Black;
                                break;
                        }
                        break;
                    case "Warning":
                        color = Color.Orange;
                        break;
                    case "Error":
                        color = Color.Red;
                        break;
                    case "Input":
                        color = Color.Blue;
                        break;
                    default:
                        color = Color.Black;
                        break;
                }

                e.CellStyle.ForeColor = color;                
            }
        }

        private void comboBoxLogFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                comboBoxLogFilter_Changed(null, null);
            }
            e.Handled = true;
        }

        private void buttonBackupsImportConfig_Click(object sender, EventArgs e)
        {
            if (openFileDialogBackups.ShowDialog() == DialogResult.OK)
            {
                ImportConfig(openFileDialogBackups.FileName);
            }
        }

        private void buttonBackupsExportConfig_Click(object sender, EventArgs e)
        {
            if (saveFileDialogBackups.ShowDialog() == DialogResult.OK)
            {
                ExportConfig(saveFileDialogBackups.FileName);
            }
        }

        private void buttonBackupsBackupFolder_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialogBackups.ShowDialog() == DialogResult.OK)
            {
                textBoxArchivesArchiveFolder.Text = folderBrowserDialogBackups.SelectedPath;
            }
        }

        private void dataGridViewMetrics_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dataGridViewMetrics.CurrentCell.OwningColumn == ColDataQuery)
            {
                ComboBox combo = e.Control as ComboBox;
                if (combo != null)
                {
                    combo.DropDownStyle = ComboBoxStyle.DropDown;
                }
            }            
        }

        private void dataGridViewMetrics_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {            
            if (e.ColumnIndex == ColDataQuery.Index)
            { 
                string value = e.FormattedValue.ToString();
                if (!ColDataQuery.Items.Contains(value))
                {
                    ColDataQuery.Items.Add(value);      
                }
                Metrics[e.RowIndex].Query = value;
            }
            if (e.ColumnIndex == ColDataMethod.Index && e.FormattedValue.ToString() == MetricMethod.InternalValue.ToString())
            {                
                ShowTooltipNotification("Note: InternalValue doesn't do anything when set by user", ToolTipIcon.Warning);                
            }
        }

        private void buttonDataViewSysTray_Click(object sender, EventArgs e)
        {
            MessageBox.Show(SysTrayTooltipReader.GetAllSysTrayToolbarText());
        }

        private void dataGridViewMetrics_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {     
            // mark internal metrics read-only
            for (int i = e.RowIndex; i < e.RowIndex + e.RowCount; ++i)
            {               
                foreach (DataGridViewCell cell in dataGridViewMetrics.Rows[i].Cells)
                {
                    if ((cell.ColumnIndex != ColDataEnableTracking.Index) && (Metrics[i].IsInternal))
                    {
                        cell.ReadOnly = true;
                        cell.Style.BackColor = Color.WhiteSmoke;                        
                    }
                    else
                    {
                        cell.ReadOnly = cell.OwningColumn.ReadOnly;
                        cell.Style = cell.OwningColumn.DefaultCellStyle;
                    }
                }                
            }
        }

        private void dataGridViewMetrics_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.ColumnIndex != ColDataEnableTracking.Index) && (Metrics[e.RowIndex].IsInternal))
            {
                ShowTooltipNotification("Only tracking status can be changed for internal metrics", ToolTipIcon.Info);            
            }
        }

        private void numericUpDownTempMin_ValueChanged(object sender, EventArgs e)
        {
            // make sure max temp is higher than min temp
            if (numericUpDownTempMin.Value >= numericUpDownTempMax.Value)
            {
                if ((numericUpDownTempMin.Value + 1) > numericUpDownTempMax.Maximum)
                {
                    // lower the min temp if it would be same as max temp
                    numericUpDownTempMin.Value = numericUpDownTempMax.Maximum - 1;
                }
                else
                {
                    // raise the max temp if it would be <= min temp and it can be raised
                    numericUpDownTempMax.Value = numericUpDownTempMin.Value + 1;
                }
            }
            SaveSettingsFromUI();
        }

        private void numericUpDownTempMax_ValueChanged(object sender, EventArgs e)
        {
            // make sure max temp is higher than min temp
            if (numericUpDownTempMax.Value <= (numericUpDownTempMin.Value))
            {
                if ((numericUpDownTempMax.Value - 1) < numericUpDownTempMin.Minimum)
                {
                    // raise the max temp if it would be the same as min temp
                    numericUpDownTempMax.Value = numericUpDownTempMax.Minimum + 1;
                }
                else
                {
                    // lower the min temp if it would be >= max temp and it can be lowered
                    numericUpDownTempMin.Value = numericUpDownTempMax.Value - 1;
                }
            }
            SaveSettingsFromUI();
        }

        private void buttonGeneralResetConfig_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This will reset ALL settings to defaults! Continue?", Text, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Settings.Reset();
                LoadSettingsToUI();
            }
        }

        private void dataGridViewMetrics_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            string message = $"Data exception in grid: {e.Exception.GetType()} - {e.Exception.Message}";
            AddLogEntry(message, LogType.Warning);
            ShowTooltipNotification(message, ToolTipIcon.Error);
        }

        private void dataGridViewMetrics_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            dataGridViewMetrics.CommitEdit(DataGridViewDataErrorContexts.Commit);            
            dataGridViewMetrics.Refresh();
        }

        private void comboBoxScheduleMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateScheduleDaysComboBoxesFromUI();
        }

        private void tabControlMain_Selected(object sender, TabControlEventArgs e)
        {                        
            if (e.TabPage.Equals(tabPageAnalytics))
            {
                UpdateChartScales(false);
            }
        }

        private void linkLabelGithub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
#if RELEASE
            OpenLink("https://github.com/smurferson1/MineControl");
#else
            MessageBox.Show("Link clicked");
#endif
        }

        private void buttonScheduleMoveNodeUp_Click(object sender, EventArgs e)
        {
            if (treeViewSchedule.SelectedNode == null)
            {                
                MessageBox.Show("No node is selected to move.");
            }
            else
            {
                Guid nodeId = (Guid)treeViewSchedule.SelectedNode.Tag;
                if (SelectedSchedule.MoveNodeUp(nodeId))
                {
                    SaveSchedulesFromList();

                    // reload to show the move
                    LoadScheduleToTreeView(SelectedSchedule, nodeId);
                }
                else
                {
                    MessageBox.Show("Can't move this node up, either because it's already at the top, or because a node that would be switched isn't allowed to move.");
                }
            }
        }

        private void buttonScheduleMoveNodeDown_Click(object sender, EventArgs e)
        {
            if (treeViewSchedule.SelectedNode == null)
            {
                MessageBox.Show("No node is selected to move.");
            }
            else
            {
                Guid nodeId = (Guid)treeViewSchedule.SelectedNode.Tag;
                if (SelectedSchedule.MoveNodeDown(nodeId))
                {
                    SaveSchedulesFromList();

                    // reload to show the move
                    LoadScheduleToTreeView(SelectedSchedule, nodeId);
                }
                else
                {
                    MessageBox.Show("Can't move this node down, either because it's already at the bottom, or because a node that would be switched isn't allowed to move.");
                }
            }
        }

        private void buttonDataRemoveUnusedQueryOptions_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Remove ALL query options in the list that aren't currently assigned to a metric?", Text, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                for (int i = ColDataQuery.Items.Count - 1; i >= 0; --i)
                {
                    if (!Metrics.Any(x => x.Query == ColDataQuery.Items[i].ToString()))
                    {
                        ColDataQuery.Items.RemoveAt(i);
                    }
                }
                SaveSettingsFromUI();
            }
        }

        private void dataGridViewApps_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == ColAppChooseButton.Index)
            {
                PromptToSelectAppPathForRow(e.RowIndex);
            }
        }

        private void linkLabelAboutLicense_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenLink("https://creativecommons.org/licenses/by-sa/4.0/");
        }
    }
}