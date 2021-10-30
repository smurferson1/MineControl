using MineControl.Lib;
using MineControl.Lib.Schedule;
using MineControl.Lib.Utils;
using MineControl.Lib.WinAPI;
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
using System.Runtime.Versioning;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace MineControl
{
    [SupportedOSPlatform("windows")]
    public partial class FormMineControl : Form, IChartManager, ILog, ISettingsFile, IStats, IActiveSchedules
    {
        #region Local Consts
        // apps        
        private const string cAppMC = "MineControl";
        private const string cAppGPUMiner = Const.GPUMiner;
        private const int cAppGPUMinerIndex = 0;
        private const string cAppCPUMiner = Const.CPUMiner;
        private const int cAppCPUMinerIndex = 1;
        private const string cAppHWMonitor = "Hardware Monitor";
        private const int cAppHWMonitorIndex = 2;
        private const string cAppGPUController = "GPU Controller";
        private const int cAppGPUControllerIndex = 3;

        // charts
        private const string cGPU = "GPU";
        private const string cCPU = "CPU";
        private const string cResources = "Resources";

        // metrics and stats
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
        private bool CanSave { get; set; } = false;
        private bool CanQuit { get; set; } = false;
        private DateTime StartupTimestamp { get; } = DateTime.Now;

        // temp management
        private DateTime LastGPUStepChange { get; set; } = DateTime.Now;
        
        // miner management        
                
        // schedules        
        private BindingList<Schedule> Schedules { get; set; } = new BindingList<Schedule>();
        private string[] StartMonthDays { get; set; }
        private string[] EndMonthDays { get; set; }
        private BindingSource bindingSourceSchedules;
        private BindingSource bindingSourceGPUSchedule;
        private BindingSource bindingSourceCPUSchedule;

        // processes        
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
        
        // logs
        private DataTable DataTableLog { get; } = new DataTable();

        // archival
        private Archiver Archiver { get; set; }

        // serialization
        private readonly JsonSerializerOptions jsonOptionsScheduleNodes = new()
        {
            Converters = { new ScheduleNodeConverter() },
            WriteIndented = true,
            PropertyNameCaseInsensitive = true,
            IncludeFields = true
        };
        private readonly JsonSerializerOptions jsonOptionsMetrics = new()
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true,
            ReferenceHandler = ReferenceHandler.Preserve
        };
        private readonly JsonSerializerOptions jsonOptionsMetricQueryOptions = new()
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true
        };
        #endregion

        public FormMineControl()
        {
            InitializeComponent();
        }

        private void UpdateSettingsEvents(bool assign = true)
        {
            if (assign)
            {
                Settings.SettingChanging += Settings_SettingChanging;
                Settings.PropertyChanged += Settings_PropertyChanged;
            }
            else
            {
                Settings.SettingChanging -= Settings_SettingChanging;
                Settings.PropertyChanged -= Settings_PropertyChanged;
            }
        }

        /// <summary>
        /// Initializes internal objects and statics
        /// </summary>
        private void InitializeInternals()
        {
            MinerUtils.Setup(
                this,
                this,
                notifyIconMain,
                dataGridViewApps.Rows[cAppGPUMinerIndex].Cells[ColAppStatus.Index],
                dataGridViewApps.Rows[cAppCPUMinerIndex].Cells[ColAppStatus.Index],
                ProcessOutputReceived);
            Archiver = new Archiver(DataTableLog, Charts, this, this);
        }

        /// <summary>
        /// Sets up very particular UI pieces needed to show info, mainly grids and charts. Things would break without this.
        /// </summary>
        private void InitializeUI()
        {   
            // display version
            this.Text += $" v{Assembly.GetEntryAssembly().GetName().Version}";
            labelAboutVersion.Text = Assembly.GetEntryAssembly().GetName().Version.ToString();

            // display attribution   
            richTextBoxAboutAttribution.Text = Properties.Resources.txtAttribution;
            
            // set up grids and their data sources
            dataGridViewApps.Rows.Add(cAppGPUMiner, "", "Unknown", "");
            dataGridViewApps.Rows.Add(cAppCPUMiner, "", "Unknown", "");
            dataGridViewApps.Rows.Add(cAppHWMonitor, "", "Unknown", "");
            dataGridViewApps.Rows.Add(cAppGPUController, "", "Unknown", "");            
            DataTableLog.Columns.Add("Source");
            DataTableLog.Columns.Add("Type");
            DataTableLog.Columns.Add("Time");
            DataTableLog.Columns["Time"].DataType = typeof(DateTime);
            DataTableLog.Columns.Add("Message");
            dataGridViewLog.AutoGenerateColumns = false;
            dataGridViewLog.DataSource = DataTableLog;
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
            
            // system-defined metrics
            Metrics.Add(new Metric(true, cGPUPowerStep, MetricType.Number, MetricSource.MineControl, MetricMethod.InternalValue, "", this));
            Metric(cGPUPowerStep).IsInternal = true;  
            Metrics.Add(new Metric(false, cGPUHashRate, MetricType.Number, MetricSource.GPUMiner, MetricMethod.RegEx, "", this, this));
            Metrics.Add(new Metric(false, cGPUHashRateUnit, MetricType.Selection, MetricSource.GPUMiner, MetricMethod.RegEx, "", this));
            Metric(cGPUHashRate).Unit = Metric(cGPUHashRateUnit);
            Metrics.Add(new Metric(false, cGPUHashAlgo, MetricType.Selection, MetricSource.GPUMiner, MetricMethod.RegEx, "", this));
            Metric(cGPUHashRate).GroupedBy = Metric(cGPUHashAlgo);            
            Metrics.Add(new Metric(false, cGPUMemJuncTemp, MetricType.Number, MetricSource.SysTray, MetricMethod.RegEx, "", this));
            Metrics.Add(new Metric(false, cGPUMemJuncTempUnit, MetricType.Selection, MetricSource.SysTray, MetricMethod.RegEx, "", this));
            Metric(cGPUMemJuncTemp).Unit = Metric(cGPUMemJuncTempUnit);
            Metrics.Add(new Metric(false, cCPUHashRate, MetricType.Number, MetricSource.CPUMiner, MetricMethod.RegEx, "", this, this));
            Metrics.Add(new Metric(false, cCPUHashRateUnit, MetricType.Selection, MetricSource.CPUMiner, MetricMethod.RegEx, "", this));
            Metric(cCPUHashRate).Unit = Metric(cCPUHashRateUnit);
            Metrics.Add(new Metric(false, cCPUHashAlgo, MetricType.Selection, MetricSource.CPUMiner, MetricMethod.RegEx, "", this));
            Metric(cCPUHashRate).GroupedBy = Metric(cCPUHashAlgo);            
            Metrics.Add(new Metric(false, cCPUTemp, MetricType.Number, MetricSource.SysTray, MetricMethod.RegEx, "", this));
            Metrics.Add(new Metric(false, cCPUTempUnit, MetricType.Selection, MetricSource.SysTray, MetricMethod.RegEx, "", this));
            Metric(cCPUTemp).Unit = Metric(cCPUTempUnit);
            Metrics.Add(new Metric(false, cTotalPower, MetricType.Number, MetricSource.SysTray, MetricMethod.RegEx, "", this));
            Metrics.Add(new Metric(false, cGPUPower, MetricType.Number, MetricSource.SysTray, MetricMethod.RegEx, "", this));
            Metrics.Add(new Metric(false, cCPUPower, MetricType.Number, MetricSource.SysTray, MetricMethod.RegEx, "", this));

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
            bindingSourceSchedules = new();
            bindingSourceSchedules.DataSource = Schedules;
            comboBoxScheduleSchedules.DataSource = bindingSourceSchedules;
            comboBoxScheduleSchedules.DisplayMember = "Name";
            comboBoxScheduleSchedules.ValueMember = "Id";

            bindingSourceGPUSchedule = new();
            bindingSourceGPUSchedule.DataSource = Schedules;
            comboBoxMinerGPUSchedule.DataSource = bindingSourceGPUSchedule;
            comboBoxMinerGPUSchedule.DisplayMember = "Name";
            comboBoxMinerGPUSchedule.ValueMember = "Id";

            bindingSourceCPUSchedule = new();
            bindingSourceCPUSchedule.DataSource = Schedules;
            comboBoxMinerCPUSchedule.DataSource = bindingSourceCPUSchedule;
            comboBoxMinerCPUSchedule.DisplayMember = "Name";
            comboBoxMinerCPUSchedule.ValueMember = "Id";

            // schedule tab
            // align schedule node type UIs to calendar UI
            groupBoxScheduleWeek.Location = groupBoxScheduleCalendar.Location;
            groupBoxScheduleTime.Location = groupBoxScheduleCalendar.Location;
            groupBoxScheduleResult.Location = groupBoxScheduleCalendar.Location;
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
                tab = tabControlCharts.TabPages[^1];

                // chart
                chart = new();
                chart.Name = chartName;
                Charts.Add(chart);
                tab.Controls.Add(chart);
                chart.Cursor = Cursors.Hand;
                chart.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                chart.Dock = DockStyle.Fill;
                chart.Dock = DockStyle.Fill;
                chart.MouseMove += Chart_MouseMove;
                chart.MouseLeave += Chart_MouseLeave;
                chart.Palette = ChartColorPalette.None;
                // there is ALWAYS a callout annotation defined for later display of point info
                chart.Annotations.Add(new CalloutAnnotation());
                (chart.Annotations[0] as CalloutAnnotation).Alignment = ContentAlignment.MiddleLeft;
                (chart.Annotations[0] as CalloutAnnotation).CalloutStyle = CalloutStyle.RoundedRectangle;                

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
            dataGridViewApps.Rows[cAppGPUMinerIndex].Cells[ColAppName.Index].Value = Settings.appGPUMinerName;
            dataGridViewApps.Rows[cAppGPUMinerIndex].Cells[ColAppPath.Index].Value = Settings.appGPUMinerPath;
            dataGridViewApps.Rows[cAppCPUMinerIndex].Cells[ColAppName.Index].Value = Settings.appCPUMinerName;
            dataGridViewApps.Rows[cAppCPUMinerIndex].Cells[ColAppPath.Index].Value = Settings.appCPUMinerPath;
            dataGridViewApps.Rows[cAppHWMonitorIndex].Cells[ColAppName.Index].Value = Settings.appHardwareMonitorName;            
            dataGridViewApps.Rows[cAppHWMonitorIndex].Cells[ColAppPath.Index].Value = Settings.appHardwareMonitorPath;
            dataGridViewApps.Rows[cAppGPUControllerIndex].Cells[ColAppName.Index].Value = Settings.appGPUControllerName;
            dataGridViewApps.Rows[cAppGPUControllerIndex].Cells[ColAppPath.Index].Value = Settings.appGPUControllerPath;

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
            textBoxArchivesArchiveFolder.Text = Archiver.GetArchiveFolder();
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
                LoadMetricsViaMerge(Settings.metricsSerializedMetrics);
            }
        }

        /// <summary>
        /// Deserializes the given metrics and merges them with current metrics. 
        /// This means: current metrics are not removed, but are updated with the new values if needed. New metrics in serializedMetrics are added.
        /// </summary>
        /// <param name="serializedMetrics"></param>
        /// <param name="previewOnly">When true, change list is generated but changes are not committed.</param>
        /// <returns>A text description of the changes made, one per line</returns>
        private string LoadMetricsViaMerge(string serializedMetrics, bool previewOnly = false)
        {
            List<string> changes = new();

            List<Metric> loadedMetrics = JsonSerializer.Deserialize<List<Metric>>(serializedMetrics, jsonOptionsMetrics);
            foreach (Metric metric in loadedMetrics)
            {
                if (!Metrics.Any(x => x.Name == metric.Name))
                {
                    if (!previewOnly)
                    {
                        Metrics.Add(metric);
                    }
                    changes.Add($"Add metric '{metric.Name}'");
                }
                else if (Metric(metric.Name).IsInternal)
                {
                    // just get enabled status for internal metrics
                    if (Metric(metric.Name).IsEnabled != metric.IsEnabled)
                    {
                        if (!previewOnly)
                        {
                            Metric(metric.Name).IsEnabled = metric.IsEnabled;
                        }
                        changes.Add($"Change enabled status for internal metric '{metric.Name}'");
                    }
                }
                else
                {
                    // pull in external metrics fully
                    if (!previewOnly)
                    {
                        Metric(metric.Name).Assign(metric);
                    }
                    changes.Add($"Update values for metric '{metric.Name}'");
                }
            }
            dataGridViewMetrics.Refresh();

            return string.Join("\n", changes);
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
            // don't save anything while we're loading things in or disposing things
            if (!CanSave)
            {
                return;
            }

            // schedules
            if (includeSchedules)
                SaveSchedulesFromList();

            // apps grid
            Settings.appGPUMinerName = dataGridViewApps.Rows[cAppGPUMinerIndex].Cells[ColAppName.Index].Value?.ToString() ?? string.Empty;
            Settings.appGPUMinerPath = dataGridViewApps.Rows[cAppGPUMinerIndex].Cells[ColAppPath.Index].Value?.ToString() ?? string.Empty;
            Settings.appCPUMinerName = dataGridViewApps.Rows[cAppCPUMinerIndex].Cells[ColAppName.Index].Value?.ToString() ?? string.Empty;
            Settings.appCPUMinerPath = dataGridViewApps.Rows[cAppCPUMinerIndex].Cells[ColAppPath.Index].Value?.ToString() ?? string.Empty;
            Settings.appHardwareMonitorName = dataGridViewApps.Rows[cAppHWMonitorIndex].Cells[ColAppName.Index].Value?.ToString() ?? string.Empty;
            Settings.appHardwareMonitorPath = dataGridViewApps.Rows[cAppHWMonitorIndex].Cells[ColAppPath.Index].Value?.ToString() ?? string.Empty;
            Settings.appGPUControllerName = dataGridViewApps.Rows[cAppGPUControllerIndex].Cells[ColAppName.Index].Value?.ToString() ?? string.Empty;
            Settings.appGPUControllerPath = dataGridViewApps.Rows[cAppGPUControllerIndex].Cells[ColAppPath.Index].Value?.ToString() ?? string.Empty;

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

            if (!Directory.GetFiles(Archiver.GetConfigArchiveFolder()).Any())
            {
                Archiver.ArchiveConfigIfNeeded();
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
                        Archiver.IsConfigArchiveNeeded = true;
                        break;

                    // applied automatically but need archiving
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
                        Archiver.IsConfigArchiveNeeded = true;
                        break;

                    // miner changes that can be applied immediately
                    case nameof(Settings.minerCPUMode):
                        MinerUtils.UpdateMinerState(false);
                        Archiver.IsConfigArchiveNeeded = true;
                        break;
                    case nameof(Settings.minerGPUMode):
                        MinerUtils.UpdateMinerState(true, GetStat(cGPUMemJuncTemp));
                        Archiver.IsConfigArchiveNeeded = true;
                        break;
                    case nameof(Settings.minerEnableAutomation):
                    case nameof(Settings.tempEnableAutomation):
                        MinerUtils.UpdateMinerState(true, GetStat(cGPUMemJuncTemp));
                        MinerUtils.UpdateMinerState(false);
                        SysTrayIcon.UpdateTextIconFromSettings(notifyIconMain);
                        // note: basic control setting that does not trigger archiving
                        break;
                    case nameof(Settings.controlRunning):
                    case nameof(Settings.tempSpeedStep):  // Note: applying this setting is handled elsewhere                    
                        SysTrayIcon.UpdateTextIconFromSettings(notifyIconMain);
                        break;

                    // other changes that can be applied immediately
                    case nameof(Settings.tempPollingIntervalMillisecs):
                        timerMain.Interval = Settings.tempPollingIntervalMillisecs;
                        Archiver.IsConfigArchiveNeeded = true;
                        break;

                    default:
                        settingApplied = false;
                        Archiver.IsConfigArchiveNeeded = true;
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

        string IStats.Get(string rowText) => GetStat(rowText);
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

        void IStats.Set(string statName, string value) => SetStat(statName, value);
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
            string status = (string)dataGridViewApps.Rows[cAppHWMonitorIndex].Cells[ColAppStatus.Index].Value;
            ProcessUtils.LaunchProcess(ProcessHardwareMonitor, Settings.appHardwareMonitorPath, Settings.appHardwareMonitorName,
                cAppHWMonitor, ref isHardwareMonitorRunning, false, ref status, ProcessOutputReceived, this);
            dataGridViewApps.Rows[cAppHWMonitorIndex].Cells[ColAppStatus.Index].Value = status;

            // gpu controller
            status = (string)dataGridViewApps.Rows[cAppGPUControllerIndex].Cells[ColAppStatus.Index].Value;
            ProcessUtils.LaunchProcess(ProcessGPUController, Settings.appGPUControllerPath, Settings.appGPUControllerName,
                cAppGPUController, ref isGPUControllerRunning, false, ref status, ProcessOutputReceived, this);
            dataGridViewApps.Rows[cAppGPUControllerIndex].Cells[ColAppStatus.Index].Value = status;
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

                    if (sender == MinerUtils.ProcessGPUMiner)
                    {
                        applicableSource = MetricSource.GPUMiner;
                        logName = cGPU;
                    }
                    else if (sender == MinerUtils.ProcessCPUMiner)
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
                    StringBuilder inputLogValues = new();
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
            MinerUtils.UpdateMinerState(true, GetStat(cGPUMemJuncTemp));
            MinerUtils.UpdateMinerState(false);
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
            MinerUtils.UpdateMinerState(true, GetStat(cGPUMemJuncTemp));
            MinerUtils.UpdateMinerState(false);
            SaveSettingsFromUI();
        }

        /// <summary>
        /// Processes all rules affecting or affected by GPU temp
        /// </summary>
        private void DoGPUTempRules()
        {
            try
            {
                int temp;
                int step;

                // adjust power step based on GPU temp
                if (Int32.TryParse(GetStat(cGPUMemJuncTemp), out temp) && LastGPUStepChange.AddSeconds(Settings.tempSpeedStepBuffer) < DateTime.Now)
                {
                    if (temp < Settings.tempMin)
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
            catch (Exception ex)
            {
                AddLogEntry($"EXCEPTION PROCESSING GPU TEMP CONTROL RULES (make sure apps are configured correctly): {ex.GetType()} - {ex.Message}", LogType.Error);
            }
        }

        private void StepGPUPower(int step)
        {
            try
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

                using (Process p = new())
                {
                    p.StartInfo.FileName = Settings.appGPUControllerPath;
                    p.StartInfo.Arguments = processParams;
                    p.StartInfo.UseShellExecute = true;
                    p.Start();
                }

                LastGPUStepChange = DateTime.Now;
                trackBarGPUPowerStep.Value = step;
            }
            catch (Exception ex)
            {
                AddLogEntry($"EXCEPTION TRYING TO CHANGE GPU POWER STEP (make sure apps are configured correctly): {ex.GetType()} - {ex.Message}", LogType.Error);
            }
        }

        void ILog.Append(string entry, LogType logType, LogSource logSource) => AddLogEntry(entry, logType, logSource);
        private void AddLogEntry(string entry, LogType logType = LogType.Info, LogSource logSource = LogSource.Internal)
        {
            // can't invoke until control has been created, so entries at this stage are lost
            if (!this.Created)
            {
                return;
            }
            
            Invoke(new Action(() =>
            {
                string logSourceText = logSource switch
                {
                    LogSource.Internal => cAppMC,
                    LogSource.GPUMiner => cAppGPUMiner,
                    LogSource.CPUMiner => cAppCPUMiner,
                    _ => cAppMC,
                };

                // add the row                
                DataTableLog.Rows.Add(logSourceText, Enum.GetName(typeof(LogType), logType), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), entry);

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

        /// <summary>
        /// Processes one polling cycle in priority order
        /// </summary>
        private void DoPollingCycle()
        {
            try
            {
                if (UpdateGPUTemp() && Settings.controlRunning && Settings.tempEnableAutomation)
                {
                    DoGPUTempRules();
                }

                // update miner run states based on current conditions
                MinerUtils.UpdateMinerState(true, GetStat(cGPUMemJuncTemp));
                MinerUtils.UpdateMinerState(false);

                // other UI updates
                UpdatePolledMetrics();
                UpdateChartScales(false);
                UpdatePolledStats();
                UpdateStatColors();

                Archiver.RunArchiveAndClearIfNeeded();
            }
            catch (Exception ex)
            {
                AddLogEntry($"Exception executing a polling cycle: {ex.GetType()} - {ex.Message}", LogType.Error);
                throw;
            }
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
                else if (lastUpdate <= DateTime.Now.AddMilliseconds(-(Settings.tempPollingIntervalMillisecs * 2)))
                {                    
                    // 2 polling cycles old
                    row.DefaultCellStyle.ForeColor = Color.Gray;
                }
            }
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
                SetStat(cAvgGPUPowerStep, ChartUtils.CalculateAverage(Series(cGPUPowerStep), CalculationMethod.Lookahead).ToString());                
            }

            // current GPU temp - covered elsewhere            

            // average GPU temp
            if (Series(cGPUMemJuncTemp).Points.Count > 0 && Metric(cGPUMemJuncTemp).IsEnabled)
            {
                SetStat(cAvgGPUMemJuncTemp, ChartUtils.CalculateAverage(Series(cGPUMemJuncTemp), CalculationMethod.Lookahead).ToString());
            }

            // average GPU hash rate
            if (Series(cGPUHashRate).Points.Count > 0 && Metric(cGPUHashRate).IsEnabled)
            {
                SetStat(cAvgGPUHashRate, ChartUtils.CalculateAverage(Series(cGPUHashRate), CalculationMethod.Lookbehind).ToString());
            }

            // CPU temp and average
            if (Series(cCPUTemp).Points.Count > 0 && Metric(cCPUTemp).IsEnabled)
            {
                SetStat(cCPUTemp, Metric(cCPUTemp).NumericResult.ToString());
                SetStat(cAvgCPUTemp, ChartUtils.CalculateAverage(Series(cCPUTemp), CalculationMethod.Lookahead).ToString());
            }

            // average CPU hash rate
            if (Series(cCPUHashRate).Points.Count > 0 && Metric(cCPUHashRate).IsEnabled)
            {
                SetStat(cAvgCPUHashRate, ChartUtils.CalculateAverage(Series(cCPUHashRate), CalculationMethod.Lookbehind).ToString());
            }

            // total power
            if (Metric(cTotalPower).IsEnabled)
            {
                SetStat(cTotalPower, Metric(cTotalPower).NumericResult.ToString());
            }

            // average total power and kWh
            if (Series(cTotalPower).Points.Count > 0 && Metric(cTotalPower).IsEnabled)
            {
                SetStat(cAvgTotalPower, ChartUtils.CalculateAverage(Series(cTotalPower), CalculationMethod.Lookbehind).ToString());
                // kWh = sum of watts per second / 60 (seconds in a minute) * 60 (minutes in an hour) * 1000 (kilo)
                SetStat(cTotalPowerUse, ChartUtils.CalculateRate(Series(cTotalPower), 60 * 60 * 1000, CalculationMethod.Lookbehind).ToString());
            }

            // current GPU power
            if (Metric(cGPUPower).IsEnabled)
            {
                SetStat(cGPUPower, Metric(cGPUPower).NumericResult.ToString());
            }

            // average GPU power and kWh
            if (Series(cGPUPower).Points.Count > 0 && Metric(cGPUPower).IsEnabled)
            {
                SetStat(cAvgGPUPower, ChartUtils.CalculateAverage(Series(cGPUPower), CalculationMethod.Lookbehind).ToString());
                // kWh = sum of watts per second / 60 (seconds in a minute) * 60 (minutes in an hour) * 1000 (kilo)
                SetStat(cGPUPowerUse, ChartUtils.CalculateRate(Series(cGPUPower), 60 * 60 * 1000, CalculationMethod.Lookbehind).ToString());
            }

            // current GPU power
            if (Metric(cCPUPower).IsEnabled)
            {
                SetStat(cCPUPower, Metric(cCPUPower).NumericResult.ToString());
            }

            // average GPU power and kWh
            if (Series(cCPUPower).Points.Count > 0 && Metric(cCPUPower).IsEnabled)
            {
                SetStat(cAvgCPUPower, ChartUtils.CalculateAverage(Series(cCPUPower), CalculationMethod.Lookbehind).ToString());
                // kWh = sum of watts per second / 60 (seconds in a minute) * 60 (minutes in an hour) * 1000 (kilo)
                SetStat(cCPUPowerUse, ChartUtils.CalculateRate(Series(cCPUPower), 60 * 60 * 1000, CalculationMethod.Lookbehind).ToString());
            }
            
            // runtime (elapsed time since application start)         
            TimeSpan runtime = DateTime.Now - StartupTimestamp;                        
            string formattedRuntime = String.Format("{0:0}D {1:00}:{2:00}:{3:00}.{4:000}",
                runtime.Days, runtime.Hours, runtime.Minutes, runtime.Seconds, runtime.Milliseconds);    
            SetStat(cRuntime, formattedRuntime);
        }
        
        private void UpdateChartScales(bool includeGPUPowerStep, bool forceUpdate = false)
        {   
            // don't use resources to update scales if user won't see the chart
            if (!forceUpdate && (WindowState == FormWindowState.Minimized || tabControlMain.SelectedTab != tabPageAnalytics))
            {
                return;
            }

            // only update scales for the currently visible chart
            if (tabControlCharts.SelectedTab.Controls.IndexOf(Chart(cGPU)) >= 0)
            {
                ChartUtils.UpdateChartYAxisScale(Chart(cGPU), AxisType.Secondary,
                    Settings.chartMinTempOnYAxisEnabled ? new (Series, int)[] { (Series(cGPUMemJuncTemp), Settings.chartMinTempOnYAxisValue) } : null);
                if (includeGPUPowerStep)
                {
                    // always set GPU power step axis to the available power steps
                    ChartUtils.SetChartYAxisScale(Chart(cGPU).ChartAreas[0].AxisY, trackBarGPUPowerStep.Minimum, trackBarGPUPowerStep.Maximum, Chart(cGPU).Height);
                }
            }
            else if (tabControlCharts.SelectedTab.Controls.IndexOf(Chart(cCPU)) >= 0)
            {
                ChartUtils.UpdateChartYAxisScale(Chart(cCPU));
            }
            else if (tabControlCharts.SelectedTab.Controls.IndexOf(Chart(cResources)) >= 0)
            {
                ChartUtils.UpdateChartYAxisScale(Chart(cResources));
            }
        }

        private void UpdatePolledMetrics()
        {
            // GPU power step
            Metric(cGPUPowerStep).UpdateFromInput(Settings.tempSpeedStep.ToString());

            // all systray and UserValue metrics
            string sysTrayToolbarText = SysTrayTooltipReader.GetAllSysTrayToolbarText();
            foreach (Metric metric in Metrics)
            {
                // note: GPU mem junc temp is covered elsewhere
                if ((metric.IsEnabled || metric.IsInternal) && (metric.Name != cGPUMemJuncTemp))
                {
                    if (metric.Source == MetricSource.SysTray)
                    {
                        metric.UpdateFromInput(sysTrayToolbarText);
                    }
                    else if (metric.Source == MetricSource.MineControl && metric.Method == MetricMethod.UserValue)
                    {
                        metric.UpdateFromInput(metric.Query);
                    }
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

        private bool UpdateScheduleNodeFromUI(TreeNode selectedNode)
        {
            bool result = false;
            try
            {
                Schedule schedule = SelectedSchedule;
                ScheduleNode newNode = null;

                if (radioButtonScheduleCalendar.Checked)
                {
                    newNode = CreateCalendarNodeFromUI();
                }
                else if (radioButtonScheduleDaysOfWeek.Checked)
                {
                    newNode = CreateWeekNodeFromUI();
                }
                else if (radioButtonScheduleTime.Checked)
                {
                    newNode = CreateTimeNodeFromUI();
                }
                else if (radioButtonScheduleResult.Checked)
                {
                    newNode = CreateActionNodeFromUI();
                }

                if (newNode != null)
                { 
                    result = schedule.ReplaceNode((Guid)selectedNode.Tag, newNode);
                    if (result)
                    {
                        // reload to show the updated node
                        LoadScheduleToTreeView(schedule, newNode.Id);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Node not updated, or updated incorrectly, due to an exception: \n{ex.GetType()}\n{ex.Message}");
            }
            return result;
        }

        private CalendarNode CreateCalendarNodeFromUI()
        {
            int startDay = comboBoxScheduleStartDay.Text == "Last" ? 32 : Convert.ToInt32(comboBoxScheduleStartDay.Text);
            int endDay = comboBoxScheduleEndDay.Text == "Last" ? 32 : Convert.ToInt32(comboBoxScheduleEndDay.Text);
            CalendarNode scheduleNode = new(Guid.Empty,
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

        Schedule IActiveSchedules.GPU => ScheduleGPU;
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

        Schedule IActiveSchedules.CPU => ScheduleCPU;
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

                if (node is BranchingNode branchingNode)
                {
                    LoadNodesToTreeView(treeNode.Nodes, branchingNode.Children, schedule);
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
            if (Settings.controlRunning)
            {
                MessageBox.Show("Automation will be stopped. It can be restarted once the config is imported.");
                StopAutomation();
            }

            CanSave = false;
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
            finally
            {
                CanSave = true;
            }
        }

        void ISettingsFile.Export(string destFilePath, string verb) => ExportConfig(destFilePath, verb);
        private void ExportConfig(string destFilePath, string verb = "")
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
                config.SaveAs(destFilePath);
                ShowTooltipNotification($"Config {(verb == string.Empty ? "export" : verb)} successful");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting config: {ex.GetType()} - {ex.Message}");
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

        private void FormMineControl_Load(object sender, EventArgs e)
        {
            // later calls depend on basic UI things, so this must be first
            InitializeUI();

            SettingsUtils.LoadSettingsFile(Settings, this);
            InitializeInternals();
            LoadSettingsToUI(true);
            UpdateSettingsEvents();
            CanSave = true;
        }

        private void FormMineControl_Shown(object sender, EventArgs e)
        {
            InitializeAutomationFromSavedState();
            if (Settings.controlStartupMinimize)
            {
                Hide();
            }
        }

        private void FormMineControl_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Settings.controlMinimizeToSysTray && !CanQuit)
            {
                e.Cancel = true;
                Hide();
            }
        }
       
        private void FormMineControl_FormClosed(object sender, FormClosedEventArgs e)
        {
            // just clean up timed tasks and apps, since stopping automation fully would change the config
            timerMain.Enabled = false;
            MinerUtils.SyncMinerState(true, MinerState.DisabledClosing, "due to application shutdown");
            MinerUtils.SyncMinerState(false, MinerState.DisabledClosing, "due to application shutdown");

            // archive everything remaining on exit
            DateTime now = DateTime.Now;
            Archiver.ArchiveAndClearOldLogs(now, true);
            Archiver.ArchiveConfigIfNeeded();

            //  avoid unintended saves while disposing binding sources
            CanSave = false;

            // dispose all class IDisposables            
            bindingSourceSchedules.Dispose();
            bindingSourceGPUSchedule.Dispose();
            bindingSourceCPUSchedule.Dispose();
            ProcessHardwareMonitor.Dispose();
            ProcessGPUController.Dispose();
            MinerUtils.DisposeChildren();
            ProcessUtils.DisposeChildren();
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
        
        private void trackBarGPUPowerStep_Scroll(object sender, EventArgs e)
        {
            AddLogEntry(string.Format("User stepping GPU power to {0}", trackBarGPUPowerStep.Value));
            Series(cGPUPowerStep).Points.AddXY(DateTime.Now, trackBarGPUPowerStep.Value);
            StepGPUPower(trackBarGPUPowerStep.Value);            
        }


        private void toolStripMenuItemSysTrayExit_Click(object sender, EventArgs e)
        {
            // only allow quit through systray
            CanQuit = true; 
            Application.Exit();
        }

        private void FormMineControl_Resize(object sender, EventArgs e)
        {            
            if (Settings.controlMinimizeToSysTray && (WindowState == FormWindowState.Minimized))
            {
                Hide();                
            }
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
            if (treeViewSchedule.SelectedNode == null)
            {
                MessageBox.Show("No node is selected, can't create a new sub-node.");
            }
            else
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
            if (treeViewSchedule.SelectedNode == null)
            {
                MessageBox.Show("No node is selected, can't update.");
            }
            else
            {
                if (UpdateScheduleNodeFromUI(treeViewSchedule.SelectedNode))
                {
                    SaveSchedulesFromList();
                }   
                else
                {
                    MessageBox.Show("Couldn't update node. Selected node might be incompatible with the new Node Details.");
                }
            }
        }        

        private void buttonScheduleDeleteNode_Click(object sender, EventArgs e)
        {
            if (treeViewSchedule.SelectedNode == null)
            {
                MessageBox.Show("No node is selected, can't delete.");
            }
            else
            {                
                if (MessageBox.Show("Any children will be deleted, and nodes at the same level will be deleted if this is an 'else' node or the only 'if' node. Continue?", 
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
            LoadSchedulesToUI();
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
                var color = dataGridViewLog.Rows[e.RowIndex].Cells[1].Value.ToString() switch
                {
                    "Info" => dataGridViewLog.Rows[e.RowIndex].Cells[0].Value.ToString() switch
                    {
                        cAppMC => Color.Black,
                        cAppGPUMiner => Color.DarkGray,
                        cAppCPUMiner => Color.Gray,
                        _ => Color.Black,
                    },
                    "Warning" => Color.Orange,
                    "Error" => Color.Red,
                    "Input" => Color.Blue,
                    _ => Color.Black,
                };
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
            if (dataGridViewMetrics.CurrentCell.OwningColumn == ColDataQuery && e.Control is ComboBox combo)
            {                
                combo.DropDownStyle = ComboBoxStyle.DropDown;                
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
                if (Settings.controlRunning)
                {
                    MessageBox.Show("Automation will be stopped. It can be restarted once the config is reset.");
                    StopAutomation();
                }
                CanSave = false;
                try
                {
                    Settings.Reset();
                    LoadSettingsToUI();
                    ShowTooltipNotification("Config reset successful.");
                }
                finally
                {
                    CanSave = true;
                }
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
                foreach (Control control in tabControlCharts.SelectedTab.Controls)
                {
                    if (control is Chart chart)
                    {
                        UpdateChartScales(false);
                        chart.Update();
                    }
                }
            }
        }

        private void charts_ShowConfigOptionsChanged(object sender, EventArgs e)
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
                MessageBox.Show("No node is selected, can't move.");
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
                MessageBox.Show("No node is selected, can't move.");
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
            ProcessUtils.OpenLinkInDefaultBrowser("https://creativecommons.org/licenses/by-sa/4.0/");
        }

        private void buttonDataLoadPreset_Click(object sender, EventArgs e)
        {
            if (openFileDialogPresets.ShowDialog() == DialogResult.OK)
            {
                CanSave = false;
                try
                {
                    var changeList = LoadMetricsViaMerge(File.ReadAllText(openFileDialogPresets.FileName), true);
                    if (MessageBox.Show(this, $"The following changes will be made to metric settings. Continue?\n\nChanges:\n{changeList}",
                        this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        LoadMetricsViaMerge(File.ReadAllText(openFileDialogPresets.FileName), false);
                        ShowTooltipNotification("Preset loaded.");
                    }
                }                
                finally
                {
                    CanSave = true;
                    SaveSettingsFromUI();
                }
            }
        }

        private void buttonDataSavePreset_Click(object sender, EventArgs e)
        {
            if (saveFileDialogPresets.ShowDialog() == DialogResult.OK)
            {
                string savedMetrics;
                if (MessageBox.Show(this, "Press Yes to save all metric settings.\nPress No to EXCLUDE untracked and internal metrics.",
                    this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    File.WriteAllText(saveFileDialogPresets.FileName, JsonSerializer.Serialize(Metrics, jsonOptionsMetrics));
                    savedMetrics = string.Join("\n", Metrics.Select(x => x.Name));
                    
                }
                else
                {
                    File.WriteAllText(saveFileDialogPresets.FileName, JsonSerializer.Serialize(Metrics.Where(x => x.IsEnabled && !x.IsInternal), jsonOptionsMetrics));
                    savedMetrics = string.Join("\n", Metrics.Where(x => x.IsEnabled && !x.IsInternal).Select(x => x.Name));                    
                }
                MessageBox.Show($"Preset saved to file \"{saveFileDialogPresets.FileName}\".\n\nMetric settings included in the preset: \n{savedMetrics}");
            }
        }

        private void richTextBoxAboutAttribution_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            ProcessUtils.OpenLinkInDefaultBrowser(e.LinkText);
        }

        private void Chart_MouseLeave(object sender, EventArgs e)
        {
            if (sender is Chart activeChart)
            {
                foreach (Series series in activeChart.Series)
                {
                    if (series.Tag != null)
                    {
                        --series.BorderWidth;
                        series.Tag = null;
                    }                    
                }
                CalloutAnnotation annotation = (activeChart.Annotations[0] as CalloutAnnotation);
                if (annotation.AnchorDataPoint != null)
                {
                    annotation.AnchorDataPoint.MarkerSize -= 4;
                    annotation.Visible = false;
                    annotation.AnchorDataPoint = null;
                }
            }
        }

        private void Chart_MouseMove(object sender, MouseEventArgs e)
        {
            if (sender is Chart activeChart)
            {
                HitTestResult hitTestResult = ChartUtils.NearHitTest(activeChart, 5, e.X, e.Y);
                Series activeSeries = null;
                if (hitTestResult != null && hitTestResult.Object is DataPoint hitPoint)
                {
                    if (hitTestResult.Series != null)
                    {
                        activeSeries = hitTestResult.Series;
                        if (hitTestResult.Series.Tag == null)
                        {
                            ++hitTestResult.Series.BorderWidth;
                            hitTestResult.Series.Tag = true;
                        }
                    }
                    CalloutAnnotation annotation = (activeChart.Annotations[0] as CalloutAnnotation);
                    if (annotation.AnchorDataPoint != hitPoint)
                    { 
                        // de-select old data point
                        if (annotation.AnchorDataPoint != null)
                        {
                            annotation.AnchorDataPoint.MarkerSize -= 4;
                        }

                        // select new data point
                        hitPoint.MarkerSize += 4;

                        // display annotation
                        if (activeSeries != null)
                        {
                            annotation.Visible = true;
                            annotation.AnchorDataPoint = hitPoint;
                            annotation.Text = $"\n{activeSeries.Name}\nX: {DateTime.FromOADate(hitPoint.XValue)}\nY: {hitPoint.YValues[0]}\n\n";
                        }                        
                    }
                }
                else
                {
                    CalloutAnnotation annotation = (activeChart.Annotations[0] as CalloutAnnotation);
                    if (annotation.AnchorDataPoint != null)
                    {
                        annotation.AnchorDataPoint.MarkerSize -= 4;
                        annotation.Visible = false;
                        annotation.AnchorDataPoint = null;
                    }
                }
                foreach (Series series in activeChart.Series)
                {
                    if (series != activeSeries && series.Tag != null)
                    {
                        --series.BorderWidth;
                        series.Tag = null;
                    }                    
                }
            }
        }

        private void tabControlCharts_Selected(object sender, TabControlEventArgs e)
        {
            foreach (Control control in e.TabPage.Controls)
            {
                if (control is Chart chart)
                {
                    UpdateChartScales(false);
                    chart.Update();
                }
            }
        }
    }
}