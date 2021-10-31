
namespace MineControl
{
    partial class FormMineControl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMineControl));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.timerMain = new System.Windows.Forms.Timer(this.components);
            this.toolTipMain = new System.Windows.Forms.ToolTip(this.components);
            this.checkBoxStartUpAutomation = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownTempSteppingBuffer = new System.Windows.Forms.NumericUpDown();
            this.label24 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.buttonScheduleCreateSubNode = new System.Windows.Forms.Button();
            this.buttonScheduleDeleteNode = new System.Windows.Forms.Button();
            this.buttonScheduleCreateNode = new System.Windows.Forms.Button();
            this.buttonScheduleUpdateNode = new System.Windows.Forms.Button();
            this.label34 = new System.Windows.Forms.Label();
            this.buttonScheduleMoveNodeUp = new System.Windows.Forms.Button();
            this.buttonScheduleMoveNodeDown = new System.Windows.Forms.Button();
            this.checkBoxMinerCPUShowLogs = new System.Windows.Forms.CheckBox();
            this.checkBoxMinerCPUUserActivityShutoff = new System.Windows.Forms.CheckBox();
            this.radioButtonCPUModeDontControl = new System.Windows.Forms.RadioButton();
            this.radioButtonCPUModeAlwaysOff = new System.Windows.Forms.RadioButton();
            this.radioButtonCPUModeAlwaysOn = new System.Windows.Forms.RadioButton();
            this.radioButtonCPUModeSchedule = new System.Windows.Forms.RadioButton();
            this.checkBoxMinerGPUShowLogs = new System.Windows.Forms.CheckBox();
            this.checkBoxMinerGPUUserActivityShutoff = new System.Windows.Forms.CheckBox();
            this.radioButtonGPUModeDontControl = new System.Windows.Forms.RadioButton();
            this.radioButtonGPUModeAlwaysOff = new System.Windows.Forms.RadioButton();
            this.radioButtonGPUModeAlwaysOn = new System.Windows.Forms.RadioButton();
            this.radioButtonGPUModeSchedule = new System.Windows.Forms.RadioButton();
            this.numericUpDownTempMin = new System.Windows.Forms.NumericUpDown();
            this.textBoxTempPowerStepParam1 = new System.Windows.Forms.TextBox();
            this.textBoxTempPowerStepParam2 = new System.Windows.Forms.TextBox();
            this.textBoxTempPowerStepParam3 = new System.Windows.Forms.TextBox();
            this.textBoxTempPowerStepParam4 = new System.Windows.Forms.TextBox();
            this.textBoxTempPowerStepParam5 = new System.Windows.Forms.TextBox();
            this.trackBarGPUPowerStep = new System.Windows.Forms.TrackBar();
            this.numericUpDownTempMax = new System.Windows.Forms.NumericUpDown();
            this.label30 = new System.Windows.Forms.Label();
            this.checkBoxMinimizeToSysTray = new System.Windows.Forms.CheckBox();
            this.checkBoxStartupMinimize = new System.Windows.Forms.CheckBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.numericUpDownTempPollingInterval = new System.Windows.Forms.NumericUpDown();
            this.comboBoxArchivesClearOldChartsUnit = new System.Windows.Forms.ComboBox();
            this.numericUpDownArchivesClearOldChartsValue = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownArchivesDeleteOldFilesDays = new System.Windows.Forms.NumericUpDown();
            this.textBoxArchivesArchiveFolder = new System.Windows.Forms.TextBox();
            this.label35 = new System.Windows.Forms.Label();
            this.comboBoxArchivesArchiveIntervalUnit = new System.Windows.Forms.ComboBox();
            this.numericUpDownArchivesArchiveInterval = new System.Windows.Forms.NumericUpDown();
            this.comboBoxArchivesLogManagementUnit = new System.Windows.Forms.ComboBox();
            this.numericUpDownArchivesLogManagementValue = new System.Windows.Forms.NumericUpDown();
            this.label33 = new System.Windows.Forms.Label();
            this.comboBoxArchivesLogManagementType = new System.Windows.Forms.ComboBox();
            this.checkBoxArchivesLogManagement = new System.Windows.Forms.CheckBox();
            this.checkBoxArchivesArchiveConfig = new System.Windows.Forms.CheckBox();
            this.label36 = new System.Windows.Forms.Label();
            this.checkBoxArchivesDeleteOldFiles = new System.Windows.Forms.CheckBox();
            this.checkBoxArchivesClearOldCharts = new System.Windows.Forms.CheckBox();
            this.buttonDataViewSysTrayTooltips = new System.Windows.Forms.Button();
            this.buttonDataRemoveUnusedQueryOptions = new System.Windows.Forms.Button();
            this.numericUpDownTempGPUShutOffThresholdSecs = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.numericUpDownTempGPUShutOffSecs = new System.Windows.Forms.NumericUpDown();
            this.label15 = new System.Windows.Forms.Label();
            this.checkBoxStopWhenOverheat = new System.Windows.Forms.CheckBox();
            this.checkBoxTempStopWhenTempUnknown = new System.Windows.Forms.CheckBox();
            this.numericUpDownTempTryStepUpSecs = new System.Windows.Forms.NumericUpDown();
            this.checkBoxTempTryStepUp = new System.Windows.Forms.CheckBox();
            this.buttonScheduleDeleteSchedule = new System.Windows.Forms.Button();
            this.buttonScheduleDuplicateSchedule = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.textBoxScheduleName = new System.Windows.Forms.TextBox();
            this.treeViewSchedule = new System.Windows.Forms.TreeView();
            this.buttonScheduleCreateSchedule = new System.Windows.Forms.Button();
            this.buttonChartClearData = new System.Windows.Forms.Button();
            this.comboBoxChartShowLastUnit = new System.Windows.Forms.ComboBox();
            this.numericUpDownChartShowLastX = new System.Windows.Forms.NumericUpDown();
            this.radioButtonChartShowLastX = new System.Windows.Forms.RadioButton();
            this.radioButtonChartShowAll = new System.Windows.Forms.RadioButton();
            this.numericUpDownChartMinGPUTempOnYAxisValue = new System.Windows.Forms.NumericUpDown();
            this.checkBoxChartMinGPUTempOnYAxisEnabled = new System.Windows.Forms.CheckBox();
            this.checkBoxLogAutoScroll = new System.Windows.Forms.CheckBox();
            this.checkBoxLogColorCode = new System.Windows.Forms.CheckBox();
            this.comboBoxLogFilter = new System.Windows.Forms.ComboBox();
            this.checkBoxEnableTempControl = new System.Windows.Forms.CheckBox();
            this.checkBoxEnableMinerAutomation = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonDataLoadPreset = new System.Windows.Forms.Button();
            this.buttonDataSavePreset = new System.Windows.Forms.Button();
            this.openFileDialogAppPath = new System.Windows.Forms.OpenFileDialog();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageConfig = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.comboBoxMinerCPUSchedule = new System.Windows.Forms.ComboBox();
            this.groupBoxGeneral = new System.Windows.Forms.GroupBox();
            this.buttonGeneralResetConfig = new System.Windows.Forms.Button();
            this.numericUpDownMinerUserActivityTimeout = new System.Windows.Forms.NumericUpDown();
            this.buttonGeneralExportConfig = new System.Windows.Forms.Button();
            this.buttonGeneralImportConfig = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.comboBoxMinerGPUSchedule = new System.Windows.Forms.ComboBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.buttonBackupsBackupFolder = new System.Windows.Forms.Button();
            this.groupBoxDataTracking = new System.Windows.Forms.GroupBox();
            this.dataGridViewMetrics = new System.Windows.Forms.DataGridView();
            this.ColDataEnableTracking = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ColDataMetric = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColDataType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ColDataInputSource = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ColDataMethod = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ColDataQuery = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.labelMaxSpeed = new System.Windows.Forms.Label();
            this.labelMinSpeed = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBoxMinersAndApps = new System.Windows.Forms.GroupBox();
            this.dataGridViewApps = new System.Windows.Forms.DataGridView();
            this.ColAppType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColAppStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColAppName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColAppPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColAppChooseButton = new System.Windows.Forms.DataGridViewButtonColumn();
            this.tabPageSchedule = new System.Windows.Forms.TabPage();
            this.groupBoxScheduleNodeDetails = new System.Windows.Forms.GroupBox();
            this.panelScheduleNodeButtons = new System.Windows.Forms.Panel();
            this.radioButtonScheduleCalendar = new System.Windows.Forms.RadioButton();
            this.groupBoxScheduleCalendar = new System.Windows.Forms.GroupBox();
            this.label17 = new System.Windows.Forms.Label();
            this.comboBoxScheduleEndDay = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.comboBoxScheduleEndMonth = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.comboBoxScheduleStartDay = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.comboBoxScheduleStartMonth = new System.Windows.Forms.ComboBox();
            this.radioButtonScheduleResult = new System.Windows.Forms.RadioButton();
            this.groupBoxScheduleWeek = new System.Windows.Forms.GroupBox();
            this.checkBoxScheduleSunday = new System.Windows.Forms.CheckBox();
            this.checkBoxScheduleThursday = new System.Windows.Forms.CheckBox();
            this.checkBoxScheduleSaturday = new System.Windows.Forms.CheckBox();
            this.checkBoxScheduleFriday = new System.Windows.Forms.CheckBox();
            this.checkBoxScheduleWednesday = new System.Windows.Forms.CheckBox();
            this.checkBoxScheduleTuesday = new System.Windows.Forms.CheckBox();
            this.checkBoxScheduleMonday = new System.Windows.Forms.CheckBox();
            this.groupBoxScheduleResult = new System.Windows.Forms.GroupBox();
            this.comboBoxScheduleResult = new System.Windows.Forms.ComboBox();
            this.groupBoxScheduleTime = new System.Windows.Forms.GroupBox();
            this.label20 = new System.Windows.Forms.Label();
            this.dateTimePickerScheduleEndTime = new System.Windows.Forms.DateTimePicker();
            this.label19 = new System.Windows.Forms.Label();
            this.dateTimePickerScheduleStartTime = new System.Windows.Forms.DateTimePicker();
            this.radioButtonScheduleTime = new System.Windows.Forms.RadioButton();
            this.radioButtonScheduleDaysOfWeek = new System.Windows.Forms.RadioButton();
            this.comboBoxScheduleSchedules = new System.Windows.Forms.ComboBox();
            this.tabPageAnalytics = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBoxCharts = new System.Windows.Forms.GroupBox();
            this.buttonChartUpdate = new System.Windows.Forms.Button();
            this.tabControlCharts = new System.Windows.Forms.TabControl();
            this.tabControlAnalytics = new System.Windows.Forms.TabControl();
            this.tabPageStats = new System.Windows.Forms.TabPage();
            this.dataGridViewStats = new System.Windows.Forms.DataGridView();
            this.ColStatsStat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColStatsValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColStatsLastUpdate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPageAnalyticsOptions = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label32 = new System.Windows.Forms.Label();
            this.dataGridViewLog = new System.Windows.Forms.DataGridView();
            this.ColLogSource = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColLogType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColLogTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColLogMessage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPageAbout = new System.Windows.Forms.TabPage();
            this.buttonGeneralDisplayIntro = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.richTextBoxAboutAttribution = new System.Windows.Forms.RichTextBox();
            this.linkLabelAboutLicense = new System.Windows.Forms.LinkLabel();
            this.linkLabelAboutLink = new System.Windows.Forms.LinkLabel();
            this.label28 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.labelAboutVersion = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBoxControl = new System.Windows.Forms.GroupBox();
            this.labelStatusDisplay = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonStopAutomation = new System.Windows.Forms.Button();
            this.buttonStartAutomation = new System.Windows.Forms.Button();
            this.notifyIconMain = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStripSysTray = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemSysTrayOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemSysTrayExit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTipNotification = new System.Windows.Forms.ToolTip(this.components);
            this.openFileDialogBackups = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialogBackups = new System.Windows.Forms.SaveFileDialog();
            this.folderBrowserDialogBackups = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialogPresets = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialogPresets = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTempSteppingBuffer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTempMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarGPUPowerStep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTempMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTempPollingInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownArchivesClearOldChartsValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownArchivesDeleteOldFilesDays)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownArchivesArchiveInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownArchivesLogManagementValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTempGPUShutOffThresholdSecs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTempGPUShutOffSecs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTempTryStepUpSecs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownChartShowLastX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownChartMinGPUTempOnYAxisValue)).BeginInit();
            this.tabControlMain.SuspendLayout();
            this.tabPageConfig.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBoxGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinerUserActivityTimeout)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBoxDataTracking.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMetrics)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBoxMinersAndApps.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewApps)).BeginInit();
            this.tabPageSchedule.SuspendLayout();
            this.groupBoxScheduleNodeDetails.SuspendLayout();
            this.panelScheduleNodeButtons.SuspendLayout();
            this.groupBoxScheduleCalendar.SuspendLayout();
            this.groupBoxScheduleWeek.SuspendLayout();
            this.groupBoxScheduleResult.SuspendLayout();
            this.groupBoxScheduleTime.SuspendLayout();
            this.tabPageAnalytics.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBoxCharts.SuspendLayout();
            this.tabControlAnalytics.SuspendLayout();
            this.tabPageStats.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStats)).BeginInit();
            this.tabPageAnalyticsOptions.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLog)).BeginInit();
            this.tabPageAbout.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBoxControl.SuspendLayout();
            this.contextMenuStripSysTray.SuspendLayout();
            this.SuspendLayout();
            // 
            // timerMain
            // 
            this.timerMain.Interval = 1000;
            this.timerMain.Tick += new System.EventHandler(this.timerMain_Tick);
            // 
            // toolTipMain
            // 
            this.toolTipMain.AutoPopDelay = 10000;
            this.toolTipMain.InitialDelay = 500;
            this.toolTipMain.ReshowDelay = 100;
            // 
            // checkBoxStartUpAutomation
            // 
            this.checkBoxStartUpAutomation.AutoSize = true;
            this.checkBoxStartUpAutomation.Checked = true;
            this.checkBoxStartUpAutomation.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxStartUpAutomation.Location = new System.Drawing.Point(13, 86);
            this.checkBoxStartUpAutomation.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBoxStartUpAutomation.Name = "checkBoxStartUpAutomation";
            this.checkBoxStartUpAutomation.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.checkBoxStartUpAutomation.Size = new System.Drawing.Size(250, 19);
            this.checkBoxStartUpAutomation.TabIndex = 2;
            this.checkBoxStartUpAutomation.Text = "Continue Previous Automation On Startup";
            this.toolTipMain.SetToolTip(this.checkBoxStartUpAutomation, "Re-enables any automation that was running on last close");
            this.checkBoxStartUpAutomation.UseVisualStyleBackColor = true;
            this.checkBoxStartUpAutomation.CheckedChanged += new System.EventHandler(this.SettingChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(401, 113);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 15);
            this.label2.TabIndex = 42;
            this.label2.Text = "Secs";
            this.toolTipMain.SetToolTip(this.label2, "How long MineControl will wait after GPU power step change before considering ano" +
        "ther power step change. \r\nNecessary to give GPU time to adjust to the new power " +
        "step.");
            // 
            // numericUpDownTempSteppingBuffer
            // 
            this.numericUpDownTempSteppingBuffer.Location = new System.Drawing.Point(352, 111);
            this.numericUpDownTempSteppingBuffer.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.numericUpDownTempSteppingBuffer.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.numericUpDownTempSteppingBuffer.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericUpDownTempSteppingBuffer.Name = "numericUpDownTempSteppingBuffer";
            this.numericUpDownTempSteppingBuffer.Size = new System.Drawing.Size(48, 23);
            this.numericUpDownTempSteppingBuffer.TabIndex = 41;
            this.toolTipMain.SetToolTip(this.numericUpDownTempSteppingBuffer, "How long MineControl will wait after GPU power step change before considering ano" +
        "ther power step change. \r\nNecessary to give GPU time to adjust to the new power " +
        "step.");
            this.numericUpDownTempSteppingBuffer.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownTempSteppingBuffer.ValueChanged += new System.EventHandler(this.SettingChanged);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(264, 113);
            this.label24.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(88, 15);
            this.label24.TabIndex = 40;
            this.label24.Text = "Post-Step Wait:";
            this.toolTipMain.SetToolTip(this.label24, "How long MineControl will wait after GPU power step change before considering ano" +
        "ther power step change. \r\nNecessary to give GPU time to adjust to the new power " +
        "step.");
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(268, 218);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(30, 15);
            this.label10.TabIndex = 50;
            this.label10.Text = "Secs";
            this.toolTipMain.SetToolTip(this.label10, "When enabled, MineControl will attempt increasing GPU Power Step if it hasn\'t cha" +
        "nged during the specified number of seconds.");
            // 
            // buttonScheduleCreateSubNode
            // 
            this.buttonScheduleCreateSubNode.Location = new System.Drawing.Point(142, 15);
            this.buttonScheduleCreateSubNode.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonScheduleCreateSubNode.Name = "buttonScheduleCreateSubNode";
            this.buttonScheduleCreateSubNode.Size = new System.Drawing.Size(117, 27);
            this.buttonScheduleCreateSubNode.TabIndex = 22;
            this.buttonScheduleCreateSubNode.Text = "Add SubNode";
            this.toolTipMain.SetToolTip(this.buttonScheduleCreateSubNode, "Adds a new node with the specified details as a child of the selected node at the" +
        " end of the list");
            this.buttonScheduleCreateSubNode.UseVisualStyleBackColor = true;
            this.buttonScheduleCreateSubNode.Click += new System.EventHandler(this.buttonScheduleCreateSubNode_Click);
            // 
            // buttonScheduleDeleteNode
            // 
            this.buttonScheduleDeleteNode.Location = new System.Drawing.Point(12, 499);
            this.buttonScheduleDeleteNode.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonScheduleDeleteNode.Name = "buttonScheduleDeleteNode";
            this.buttonScheduleDeleteNode.Size = new System.Drawing.Size(120, 27);
            this.buttonScheduleDeleteNode.TabIndex = 16;
            this.buttonScheduleDeleteNode.Text = "Delete Node";
            this.toolTipMain.SetToolTip(this.buttonScheduleDeleteNode, "Deletes the selected node and any nodes that depend on it");
            this.buttonScheduleDeleteNode.UseVisualStyleBackColor = true;
            this.buttonScheduleDeleteNode.Click += new System.EventHandler(this.buttonScheduleDeleteNode_Click);
            // 
            // buttonScheduleCreateNode
            // 
            this.buttonScheduleCreateNode.Location = new System.Drawing.Point(8, 15);
            this.buttonScheduleCreateNode.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonScheduleCreateNode.Name = "buttonScheduleCreateNode";
            this.buttonScheduleCreateNode.Size = new System.Drawing.Size(111, 27);
            this.buttonScheduleCreateNode.TabIndex = 8;
            this.buttonScheduleCreateNode.Text = "Insert Node";
            this.toolTipMain.SetToolTip(this.buttonScheduleCreateNode, "Inserts a new node with the specified details above the selected tree level, or a" +
        "t the end of the top level if no node is selected.");
            this.buttonScheduleCreateNode.UseVisualStyleBackColor = true;
            this.buttonScheduleCreateNode.Click += new System.EventHandler(this.buttonScheduleCreateNode_Click);
            // 
            // buttonScheduleUpdateNode
            // 
            this.buttonScheduleUpdateNode.Location = new System.Drawing.Point(285, 15);
            this.buttonScheduleUpdateNode.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonScheduleUpdateNode.Name = "buttonScheduleUpdateNode";
            this.buttonScheduleUpdateNode.Size = new System.Drawing.Size(117, 27);
            this.buttonScheduleUpdateNode.TabIndex = 24;
            this.buttonScheduleUpdateNode.Text = "Update Node";
            this.toolTipMain.SetToolTip(this.buttonScheduleUpdateNode, "Updates the currently selected node with the specified details if possible.");
            this.buttonScheduleUpdateNode.UseVisualStyleBackColor = true;
            this.buttonScheduleUpdateNode.Click += new System.EventHandler(this.buttonScheduleUpdateNode_Click);
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(9, 54);
            this.label34.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(89, 15);
            this.label34.TabIndex = 57;
            this.label34.Text = "Archive Interval";
            this.toolTipMain.SetToolTip(this.label34, "How often archiving and data retention rules are evaluated. \r\nIf no data is found" +
        " to archive or clear when the interval is reached, nothing will happen.");
            // 
            // buttonScheduleMoveNodeUp
            // 
            this.buttonScheduleMoveNodeUp.Location = new System.Drawing.Point(185, 499);
            this.buttonScheduleMoveNodeUp.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonScheduleMoveNodeUp.Name = "buttonScheduleMoveNodeUp";
            this.buttonScheduleMoveNodeUp.Size = new System.Drawing.Size(120, 27);
            this.buttonScheduleMoveNodeUp.TabIndex = 24;
            this.buttonScheduleMoveNodeUp.Text = "Move Node Up";
            this.toolTipMain.SetToolTip(this.buttonScheduleMoveNodeUp, "Moves the selected node up if possible. Only moves within the same tree level.");
            this.buttonScheduleMoveNodeUp.UseVisualStyleBackColor = true;
            this.buttonScheduleMoveNodeUp.Click += new System.EventHandler(this.buttonScheduleMoveNodeUp_Click);
            // 
            // buttonScheduleMoveNodeDown
            // 
            this.buttonScheduleMoveNodeDown.Location = new System.Drawing.Point(358, 499);
            this.buttonScheduleMoveNodeDown.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonScheduleMoveNodeDown.Name = "buttonScheduleMoveNodeDown";
            this.buttonScheduleMoveNodeDown.Size = new System.Drawing.Size(120, 27);
            this.buttonScheduleMoveNodeDown.TabIndex = 25;
            this.buttonScheduleMoveNodeDown.Text = "Move Node Down";
            this.toolTipMain.SetToolTip(this.buttonScheduleMoveNodeDown, "Moves the selected node down if possible. Only moves within the same tree level.");
            this.buttonScheduleMoveNodeDown.UseVisualStyleBackColor = true;
            this.buttonScheduleMoveNodeDown.Click += new System.EventHandler(this.buttonScheduleMoveNodeDown_Click);
            // 
            // checkBoxMinerCPUShowLogs
            // 
            this.checkBoxMinerCPUShowLogs.AutoSize = true;
            this.checkBoxMinerCPUShowLogs.Location = new System.Drawing.Point(348, 77);
            this.checkBoxMinerCPUShowLogs.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBoxMinerCPUShowLogs.Name = "checkBoxMinerCPUShowLogs";
            this.checkBoxMinerCPUShowLogs.Size = new System.Drawing.Size(80, 19);
            this.checkBoxMinerCPUShowLogs.TabIndex = 10;
            this.checkBoxMinerCPUShowLogs.Text = "Keep Logs";
            this.toolTipMain.SetToolTip(this.checkBoxMinerCPUShowLogs, "When checked, miner\'s logs show up in the MineControl Log (otherwise, logs are di" +
        "scarded once scanned for input)");
            this.checkBoxMinerCPUShowLogs.UseVisualStyleBackColor = true;
            this.checkBoxMinerCPUShowLogs.CheckedChanged += new System.EventHandler(this.SettingChanged);
            // 
            // checkBoxMinerCPUUserActivityShutoff
            // 
            this.checkBoxMinerCPUUserActivityShutoff.AutoSize = true;
            this.checkBoxMinerCPUUserActivityShutoff.Location = new System.Drawing.Point(10, 77);
            this.checkBoxMinerCPUUserActivityShutoff.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBoxMinerCPUUserActivityShutoff.Name = "checkBoxMinerCPUUserActivityShutoff";
            this.checkBoxMinerCPUUserActivityShutoff.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.checkBoxMinerCPUUserActivityShutoff.Size = new System.Drawing.Size(158, 19);
            this.checkBoxMinerCPUUserActivityShutoff.TabIndex = 9;
            this.checkBoxMinerCPUUserActivityShutoff.Text = "Stop During User Activity";
            this.toolTipMain.SetToolTip(this.checkBoxMinerCPUUserActivityShutoff, "Miner is stopped when recent user activity has occurred anywhere in Windows (\"rec" +
        "ent\" is determined by the User Activity Timeout setting)");
            this.checkBoxMinerCPUUserActivityShutoff.UseVisualStyleBackColor = true;
            this.checkBoxMinerCPUUserActivityShutoff.CheckedChanged += new System.EventHandler(this.SettingChanged);
            // 
            // radioButtonCPUModeDontControl
            // 
            this.radioButtonCPUModeDontControl.AutoSize = true;
            this.radioButtonCPUModeDontControl.Location = new System.Drawing.Point(332, 20);
            this.radioButtonCPUModeDontControl.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.radioButtonCPUModeDontControl.Name = "radioButtonCPUModeDontControl";
            this.radioButtonCPUModeDontControl.Size = new System.Drawing.Size(97, 19);
            this.radioButtonCPUModeDontControl.TabIndex = 7;
            this.radioButtonCPUModeDontControl.TabStop = true;
            this.radioButtonCPUModeDontControl.Text = "Don\'t Control";
            this.toolTipMain.SetToolTip(this.radioButtonCPUModeDontControl, "Miner is not touched at all by MineControl");
            this.radioButtonCPUModeDontControl.UseVisualStyleBackColor = true;
            this.radioButtonCPUModeDontControl.Click += new System.EventHandler(this.SettingChanged);
            // 
            // radioButtonCPUModeAlwaysOff
            // 
            this.radioButtonCPUModeAlwaysOff.AutoSize = true;
            this.radioButtonCPUModeAlwaysOff.Location = new System.Drawing.Point(179, 20);
            this.radioButtonCPUModeAlwaysOff.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.radioButtonCPUModeAlwaysOff.Name = "radioButtonCPUModeAlwaysOff";
            this.radioButtonCPUModeAlwaysOff.Size = new System.Drawing.Size(82, 19);
            this.radioButtonCPUModeAlwaysOff.TabIndex = 4;
            this.radioButtonCPUModeAlwaysOff.Text = "Always Off";
            this.toolTipMain.SetToolTip(this.radioButtonCPUModeAlwaysOff, "Miner is not executed and miner process is killed if found running");
            this.radioButtonCPUModeAlwaysOff.UseVisualStyleBackColor = true;
            this.radioButtonCPUModeAlwaysOff.Click += new System.EventHandler(this.SettingChanged);
            // 
            // radioButtonCPUModeAlwaysOn
            // 
            this.radioButtonCPUModeAlwaysOn.AutoSize = true;
            this.radioButtonCPUModeAlwaysOn.Checked = true;
            this.radioButtonCPUModeAlwaysOn.Location = new System.Drawing.Point(10, 20);
            this.radioButtonCPUModeAlwaysOn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.radioButtonCPUModeAlwaysOn.Name = "radioButtonCPUModeAlwaysOn";
            this.radioButtonCPUModeAlwaysOn.Size = new System.Drawing.Size(81, 19);
            this.radioButtonCPUModeAlwaysOn.TabIndex = 3;
            this.radioButtonCPUModeAlwaysOn.TabStop = true;
            this.radioButtonCPUModeAlwaysOn.Text = "Always On";
            this.toolTipMain.SetToolTip(this.radioButtonCPUModeAlwaysOn, "Miner always runs when automation is running and \"Run Enabled Miners\" is checked");
            this.radioButtonCPUModeAlwaysOn.UseVisualStyleBackColor = true;
            this.radioButtonCPUModeAlwaysOn.Click += new System.EventHandler(this.SettingChanged);
            // 
            // radioButtonCPUModeSchedule
            // 
            this.radioButtonCPUModeSchedule.AutoSize = true;
            this.radioButtonCPUModeSchedule.Location = new System.Drawing.Point(10, 47);
            this.radioButtonCPUModeSchedule.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.radioButtonCPUModeSchedule.Name = "radioButtonCPUModeSchedule";
            this.radioButtonCPUModeSchedule.Size = new System.Drawing.Size(106, 19);
            this.radioButtonCPUModeSchedule.TabIndex = 2;
            this.radioButtonCPUModeSchedule.Text = "On A Schedule:";
            this.toolTipMain.SetToolTip(this.radioButtonCPUModeSchedule, "Miner runs using the specified schedule");
            this.radioButtonCPUModeSchedule.UseVisualStyleBackColor = true;
            this.radioButtonCPUModeSchedule.Click += new System.EventHandler(this.SettingChanged);
            // 
            // checkBoxMinerGPUShowLogs
            // 
            this.checkBoxMinerGPUShowLogs.AutoSize = true;
            this.checkBoxMinerGPUShowLogs.Location = new System.Drawing.Point(348, 75);
            this.checkBoxMinerGPUShowLogs.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBoxMinerGPUShowLogs.Name = "checkBoxMinerGPUShowLogs";
            this.checkBoxMinerGPUShowLogs.Size = new System.Drawing.Size(80, 19);
            this.checkBoxMinerGPUShowLogs.TabIndex = 8;
            this.checkBoxMinerGPUShowLogs.Text = "Keep Logs";
            this.toolTipMain.SetToolTip(this.checkBoxMinerGPUShowLogs, "When checked, miner\'s logs show up in the MineControl Log (otherwise, logs are di" +
        "scarded once scanned for input)");
            this.checkBoxMinerGPUShowLogs.UseVisualStyleBackColor = true;
            this.checkBoxMinerGPUShowLogs.CheckedChanged += new System.EventHandler(this.SettingChanged);
            // 
            // checkBoxMinerGPUUserActivityShutoff
            // 
            this.checkBoxMinerGPUUserActivityShutoff.AutoSize = true;
            this.checkBoxMinerGPUUserActivityShutoff.Location = new System.Drawing.Point(10, 77);
            this.checkBoxMinerGPUUserActivityShutoff.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBoxMinerGPUUserActivityShutoff.Name = "checkBoxMinerGPUUserActivityShutoff";
            this.checkBoxMinerGPUUserActivityShutoff.Size = new System.Drawing.Size(158, 19);
            this.checkBoxMinerGPUUserActivityShutoff.TabIndex = 7;
            this.checkBoxMinerGPUUserActivityShutoff.Text = "Stop During User Activity";
            this.toolTipMain.SetToolTip(this.checkBoxMinerGPUUserActivityShutoff, "Miner is stopped when recent user activity has occurred anywhere in Windows (\"rec" +
        "ent\" is determined by the User Activity Timeout setting)");
            this.checkBoxMinerGPUUserActivityShutoff.UseVisualStyleBackColor = true;
            this.checkBoxMinerGPUUserActivityShutoff.CheckedChanged += new System.EventHandler(this.SettingChanged);
            // 
            // radioButtonGPUModeDontControl
            // 
            this.radioButtonGPUModeDontControl.AutoSize = true;
            this.radioButtonGPUModeDontControl.Location = new System.Drawing.Point(331, 20);
            this.radioButtonGPUModeDontControl.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.radioButtonGPUModeDontControl.Name = "radioButtonGPUModeDontControl";
            this.radioButtonGPUModeDontControl.Size = new System.Drawing.Size(97, 19);
            this.radioButtonGPUModeDontControl.TabIndex = 5;
            this.radioButtonGPUModeDontControl.TabStop = true;
            this.radioButtonGPUModeDontControl.Text = "Don\'t Control";
            this.toolTipMain.SetToolTip(this.radioButtonGPUModeDontControl, "Miner is not touched at all by MineControl");
            this.radioButtonGPUModeDontControl.UseVisualStyleBackColor = true;
            this.radioButtonGPUModeDontControl.Click += new System.EventHandler(this.SettingChanged);
            // 
            // radioButtonGPUModeAlwaysOff
            // 
            this.radioButtonGPUModeAlwaysOff.AutoSize = true;
            this.radioButtonGPUModeAlwaysOff.Location = new System.Drawing.Point(173, 20);
            this.radioButtonGPUModeAlwaysOff.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.radioButtonGPUModeAlwaysOff.Name = "radioButtonGPUModeAlwaysOff";
            this.radioButtonGPUModeAlwaysOff.Size = new System.Drawing.Size(82, 19);
            this.radioButtonGPUModeAlwaysOff.TabIndex = 2;
            this.radioButtonGPUModeAlwaysOff.Text = "Always Off";
            this.toolTipMain.SetToolTip(this.radioButtonGPUModeAlwaysOff, "Miner is not executed and miner process is killed if found running");
            this.radioButtonGPUModeAlwaysOff.UseVisualStyleBackColor = true;
            this.radioButtonGPUModeAlwaysOff.Click += new System.EventHandler(this.SettingChanged);
            // 
            // radioButtonGPUModeAlwaysOn
            // 
            this.radioButtonGPUModeAlwaysOn.AutoSize = true;
            this.radioButtonGPUModeAlwaysOn.Checked = true;
            this.radioButtonGPUModeAlwaysOn.Location = new System.Drawing.Point(10, 20);
            this.radioButtonGPUModeAlwaysOn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.radioButtonGPUModeAlwaysOn.Name = "radioButtonGPUModeAlwaysOn";
            this.radioButtonGPUModeAlwaysOn.Size = new System.Drawing.Size(81, 19);
            this.radioButtonGPUModeAlwaysOn.TabIndex = 1;
            this.radioButtonGPUModeAlwaysOn.TabStop = true;
            this.radioButtonGPUModeAlwaysOn.Text = "Always On";
            this.toolTipMain.SetToolTip(this.radioButtonGPUModeAlwaysOn, "Miner always runs when automation is running and \"Run Enabled Miners\" is checked");
            this.radioButtonGPUModeAlwaysOn.UseVisualStyleBackColor = true;
            this.radioButtonGPUModeAlwaysOn.Click += new System.EventHandler(this.SettingChanged);
            // 
            // radioButtonGPUModeSchedule
            // 
            this.radioButtonGPUModeSchedule.AutoSize = true;
            this.radioButtonGPUModeSchedule.Location = new System.Drawing.Point(10, 47);
            this.radioButtonGPUModeSchedule.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.radioButtonGPUModeSchedule.Name = "radioButtonGPUModeSchedule";
            this.radioButtonGPUModeSchedule.Size = new System.Drawing.Size(106, 19);
            this.radioButtonGPUModeSchedule.TabIndex = 0;
            this.radioButtonGPUModeSchedule.Text = "On A Schedule:";
            this.toolTipMain.SetToolTip(this.radioButtonGPUModeSchedule, "Miner runs using the specified schedule");
            this.radioButtonGPUModeSchedule.UseVisualStyleBackColor = true;
            this.radioButtonGPUModeSchedule.Click += new System.EventHandler(this.SettingChanged);
            // 
            // numericUpDownTempMin
            // 
            this.numericUpDownTempMin.Location = new System.Drawing.Point(34, 25);
            this.numericUpDownTempMin.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDownTempMin.Name = "numericUpDownTempMin";
            this.numericUpDownTempMin.Size = new System.Drawing.Size(42, 23);
            this.numericUpDownTempMin.TabIndex = 53;
            this.toolTipMain.SetToolTip(this.numericUpDownTempMin, resources.GetString("numericUpDownTempMin.ToolTip"));
            this.numericUpDownTempMin.ValueChanged += new System.EventHandler(this.numericUpDownTempMin_ValueChanged);
            // 
            // textBoxTempPowerStepParam1
            // 
            this.textBoxTempPowerStepParam1.Location = new System.Drawing.Point(103, 173);
            this.textBoxTempPowerStepParam1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBoxTempPowerStepParam1.Name = "textBoxTempPowerStepParam1";
            this.textBoxTempPowerStepParam1.Size = new System.Drawing.Size(142, 23);
            this.textBoxTempPowerStepParam1.TabIndex = 47;
            this.textBoxTempPowerStepParam1.Text = "-Profile1";
            this.toolTipMain.SetToolTip(this.textBoxTempPowerStepParam1, "Command line parameter provided to the GPU Controller app to set the correspondin" +
        "g power step.");
            this.textBoxTempPowerStepParam1.Leave += new System.EventHandler(this.SettingChanged);
            // 
            // textBoxTempPowerStepParam2
            // 
            this.textBoxTempPowerStepParam2.Location = new System.Drawing.Point(103, 142);
            this.textBoxTempPowerStepParam2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBoxTempPowerStepParam2.Name = "textBoxTempPowerStepParam2";
            this.textBoxTempPowerStepParam2.Size = new System.Drawing.Size(142, 23);
            this.textBoxTempPowerStepParam2.TabIndex = 46;
            this.textBoxTempPowerStepParam2.Text = "-Profile2";
            this.toolTipMain.SetToolTip(this.textBoxTempPowerStepParam2, "Command line parameter provided to the GPU Controller app to set the correspondin" +
        "g power step.");
            this.textBoxTempPowerStepParam2.Leave += new System.EventHandler(this.SettingChanged);
            // 
            // textBoxTempPowerStepParam3
            // 
            this.textBoxTempPowerStepParam3.Location = new System.Drawing.Point(103, 110);
            this.textBoxTempPowerStepParam3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBoxTempPowerStepParam3.Name = "textBoxTempPowerStepParam3";
            this.textBoxTempPowerStepParam3.Size = new System.Drawing.Size(142, 23);
            this.textBoxTempPowerStepParam3.TabIndex = 45;
            this.textBoxTempPowerStepParam3.Text = "-Profile3";
            this.toolTipMain.SetToolTip(this.textBoxTempPowerStepParam3, "Command line parameter provided to the GPU Controller app to set the correspondin" +
        "g power step.");
            this.textBoxTempPowerStepParam3.Leave += new System.EventHandler(this.SettingChanged);
            // 
            // textBoxTempPowerStepParam4
            // 
            this.textBoxTempPowerStepParam4.Location = new System.Drawing.Point(103, 79);
            this.textBoxTempPowerStepParam4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBoxTempPowerStepParam4.Name = "textBoxTempPowerStepParam4";
            this.textBoxTempPowerStepParam4.Size = new System.Drawing.Size(142, 23);
            this.textBoxTempPowerStepParam4.TabIndex = 44;
            this.textBoxTempPowerStepParam4.Text = "-Profile4";
            this.toolTipMain.SetToolTip(this.textBoxTempPowerStepParam4, "Command line parameter provided to the GPU Controller app to set the correspondin" +
        "g power step.");
            this.textBoxTempPowerStepParam4.Leave += new System.EventHandler(this.SettingChanged);
            // 
            // textBoxTempPowerStepParam5
            // 
            this.textBoxTempPowerStepParam5.Location = new System.Drawing.Point(103, 48);
            this.textBoxTempPowerStepParam5.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBoxTempPowerStepParam5.Name = "textBoxTempPowerStepParam5";
            this.textBoxTempPowerStepParam5.Size = new System.Drawing.Size(142, 23);
            this.textBoxTempPowerStepParam5.TabIndex = 43;
            this.textBoxTempPowerStepParam5.Text = "-Profile5";
            this.toolTipMain.SetToolTip(this.textBoxTempPowerStepParam5, "Command line parameter provided to the GPU Controller app to set the correspondin" +
        "g power step.");
            this.textBoxTempPowerStepParam5.Leave += new System.EventHandler(this.SettingChanged);
            // 
            // trackBarGPUPowerStep
            // 
            this.trackBarGPUPowerStep.LargeChange = 1;
            this.trackBarGPUPowerStep.Location = new System.Drawing.Point(49, 46);
            this.trackBarGPUPowerStep.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.trackBarGPUPowerStep.Maximum = 5;
            this.trackBarGPUPowerStep.Minimum = 1;
            this.trackBarGPUPowerStep.Name = "trackBarGPUPowerStep";
            this.trackBarGPUPowerStep.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBarGPUPowerStep.Size = new System.Drawing.Size(45, 152);
            this.trackBarGPUPowerStep.TabIndex = 13;
            this.trackBarGPUPowerStep.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.toolTipMain.SetToolTip(this.trackBarGPUPowerStep, "Shows the last selected GPU power step (selection can come from the user or autom" +
        "ated action)");
            this.trackBarGPUPowerStep.Value = 1;
            this.trackBarGPUPowerStep.Scroll += new System.EventHandler(this.trackBarGPUPowerStep_Scroll);
            this.trackBarGPUPowerStep.ValueChanged += new System.EventHandler(this.SettingChanged);
            // 
            // numericUpDownTempMax
            // 
            this.numericUpDownTempMax.Location = new System.Drawing.Point(110, 25);
            this.numericUpDownTempMax.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDownTempMax.Name = "numericUpDownTempMax";
            this.numericUpDownTempMax.Size = new System.Drawing.Size(42, 23);
            this.numericUpDownTempMax.TabIndex = 54;
            this.toolTipMain.SetToolTip(this.numericUpDownTempMax, resources.GetString("numericUpDownTempMax.ToolTip"));
            this.numericUpDownTempMax.ValueChanged += new System.EventHandler(this.numericUpDownTempMax_ValueChanged);
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(180, 149);
            this.label30.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(33, 15);
            this.label30.TabIndex = 8;
            this.label30.Text = "Mins";
            this.toolTipMain.SetToolTip(this.label30, "How long MineControl will wait after the last user activity before considering th" +
        "e user inactive.");
            // 
            // checkBoxMinimizeToSysTray
            // 
            this.checkBoxMinimizeToSysTray.AutoSize = true;
            this.checkBoxMinimizeToSysTray.Checked = true;
            this.checkBoxMinimizeToSysTray.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxMinimizeToSysTray.Location = new System.Drawing.Point(13, 54);
            this.checkBoxMinimizeToSysTray.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBoxMinimizeToSysTray.Name = "checkBoxMinimizeToSysTray";
            this.checkBoxMinimizeToSysTray.Size = new System.Drawing.Size(131, 19);
            this.checkBoxMinimizeToSysTray.TabIndex = 6;
            this.checkBoxMinimizeToSysTray.Text = "Minimize To SysTray";
            this.toolTipMain.SetToolTip(this.checkBoxMinimizeToSysTray, "When checked, MineControl minimizes to the system tray");
            this.checkBoxMinimizeToSysTray.UseVisualStyleBackColor = true;
            this.checkBoxMinimizeToSysTray.CheckedChanged += new System.EventHandler(this.SettingChanged);
            // 
            // checkBoxStartupMinimize
            // 
            this.checkBoxStartupMinimize.AutoSize = true;
            this.checkBoxStartupMinimize.Location = new System.Drawing.Point(13, 22);
            this.checkBoxStartupMinimize.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBoxStartupMinimize.Name = "checkBoxStartupMinimize";
            this.checkBoxStartupMinimize.Size = new System.Drawing.Size(135, 19);
            this.checkBoxStartupMinimize.TabIndex = 5;
            this.checkBoxStartupMinimize.Text = "Minimize On Startup";
            this.toolTipMain.SetToolTip(this.checkBoxStartupMinimize, "When enabled, MineControl minimizes itself when it starts");
            this.checkBoxStartupMinimize.UseVisualStyleBackColor = true;
            this.checkBoxStartupMinimize.CheckedChanged += new System.EventHandler(this.SettingChanged);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(10, 149);
            this.label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(120, 15);
            this.label21.TabIndex = 6;
            this.label21.Text = "User Activity Timeout";
            this.toolTipMain.SetToolTip(this.label21, "How long MineControl will wait after the last user activity before considering th" +
        "e user inactive.");
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(158, 118);
            this.label23.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(52, 15);
            this.label23.TabIndex = 39;
            this.label23.Text = "Millisecs";
            this.toolTipMain.SetToolTip(this.label23, resources.GetString("label23.ToolTip"));
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(10, 117);
            this.label22.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(86, 15);
            this.label22.TabIndex = 37;
            this.label22.Text = "Polling Interval";
            this.toolTipMain.SetToolTip(this.label22, resources.GetString("label22.ToolTip"));
            // 
            // numericUpDownTempPollingInterval
            // 
            this.numericUpDownTempPollingInterval.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownTempPollingInterval.Location = new System.Drawing.Point(96, 115);
            this.numericUpDownTempPollingInterval.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.numericUpDownTempPollingInterval.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownTempPollingInterval.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownTempPollingInterval.Name = "numericUpDownTempPollingInterval";
            this.numericUpDownTempPollingInterval.Size = new System.Drawing.Size(61, 23);
            this.numericUpDownTempPollingInterval.TabIndex = 38;
            this.toolTipMain.SetToolTip(this.numericUpDownTempPollingInterval, resources.GetString("numericUpDownTempPollingInterval.ToolTip"));
            this.numericUpDownTempPollingInterval.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownTempPollingInterval.ValueChanged += new System.EventHandler(this.SettingChanged);
            // 
            // comboBoxArchivesClearOldChartsUnit
            // 
            this.comboBoxArchivesClearOldChartsUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxArchivesClearOldChartsUnit.FormattingEnabled = true;
            this.comboBoxArchivesClearOldChartsUnit.Items.AddRange(new object[] {
            "Minutes",
            "Hours",
            "Days"});
            this.comboBoxArchivesClearOldChartsUnit.Location = new System.Drawing.Point(239, 117);
            this.comboBoxArchivesClearOldChartsUnit.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.comboBoxArchivesClearOldChartsUnit.Name = "comboBoxArchivesClearOldChartsUnit";
            this.comboBoxArchivesClearOldChartsUnit.Size = new System.Drawing.Size(68, 23);
            this.comboBoxArchivesClearOldChartsUnit.TabIndex = 71;
            this.toolTipMain.SetToolTip(this.comboBoxArchivesClearOldChartsUnit, "When enabled, all chart data older than the specified time will be cleared. Evalu" +
        "ated on the archive interval.");
            // 
            // numericUpDownArchivesClearOldChartsValue
            // 
            this.numericUpDownArchivesClearOldChartsValue.Location = new System.Drawing.Point(182, 117);
            this.numericUpDownArchivesClearOldChartsValue.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.numericUpDownArchivesClearOldChartsValue.Name = "numericUpDownArchivesClearOldChartsValue";
            this.numericUpDownArchivesClearOldChartsValue.Size = new System.Drawing.Size(52, 23);
            this.numericUpDownArchivesClearOldChartsValue.TabIndex = 70;
            this.toolTipMain.SetToolTip(this.numericUpDownArchivesClearOldChartsValue, "When enabled, all chart data older than the specified time will be cleared. Evalu" +
        "ated on the archive interval.");
            // 
            // numericUpDownArchivesDeleteOldFilesDays
            // 
            this.numericUpDownArchivesDeleteOldFilesDays.Location = new System.Drawing.Point(198, 181);
            this.numericUpDownArchivesDeleteOldFilesDays.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.numericUpDownArchivesDeleteOldFilesDays.Name = "numericUpDownArchivesDeleteOldFilesDays";
            this.numericUpDownArchivesDeleteOldFilesDays.Size = new System.Drawing.Size(52, 23);
            this.numericUpDownArchivesDeleteOldFilesDays.TabIndex = 66;
            this.toolTipMain.SetToolTip(this.numericUpDownArchivesDeleteOldFilesDays, "When enabled, archive files older than the specified number of days are deleted f" +
        "rom the Archive Folder.\r\nEvaluated on the archive interval and when closing Mine" +
        "Control.");
            // 
            // textBoxArchivesArchiveFolder
            // 
            this.textBoxArchivesArchiveFolder.Location = new System.Drawing.Point(94, 21);
            this.textBoxArchivesArchiveFolder.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBoxArchivesArchiveFolder.Name = "textBoxArchivesArchiveFolder";
            this.textBoxArchivesArchiveFolder.Size = new System.Drawing.Size(307, 23);
            this.textBoxArchivesArchiveFolder.TabIndex = 61;
            this.toolTipMain.SetToolTip(this.textBoxArchivesArchiveFolder, "Where archive files will go. Config archives are placed in a \"ConfigArchives\" sub" +
        "-folder.");
            this.textBoxArchivesArchiveFolder.Validated += new System.EventHandler(this.SettingChanged);
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(9, 24);
            this.label35.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(83, 15);
            this.label35.TabIndex = 60;
            this.label35.Text = "Archive Folder";
            this.toolTipMain.SetToolTip(this.label35, "Where archive files will go. Config archives are placed in a \"ConfigArchives\" sub" +
        "-folder.");
            // 
            // comboBoxArchivesArchiveIntervalUnit
            // 
            this.comboBoxArchivesArchiveIntervalUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxArchivesArchiveIntervalUnit.FormattingEnabled = true;
            this.comboBoxArchivesArchiveIntervalUnit.Items.AddRange(new object[] {
            "Minutes",
            "Hours",
            "Days"});
            this.comboBoxArchivesArchiveIntervalUnit.Location = new System.Drawing.Point(156, 52);
            this.comboBoxArchivesArchiveIntervalUnit.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.comboBoxArchivesArchiveIntervalUnit.Name = "comboBoxArchivesArchiveIntervalUnit";
            this.comboBoxArchivesArchiveIntervalUnit.Size = new System.Drawing.Size(70, 23);
            this.comboBoxArchivesArchiveIntervalUnit.TabIndex = 59;
            this.toolTipMain.SetToolTip(this.comboBoxArchivesArchiveIntervalUnit, "How often archiving and data retention rules are evaluated. ");
            this.comboBoxArchivesArchiveIntervalUnit.SelectedIndexChanged += new System.EventHandler(this.SettingChanged);
            // 
            // numericUpDownArchivesArchiveInterval
            // 
            this.numericUpDownArchivesArchiveInterval.Location = new System.Drawing.Point(99, 52);
            this.numericUpDownArchivesArchiveInterval.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.numericUpDownArchivesArchiveInterval.Name = "numericUpDownArchivesArchiveInterval";
            this.numericUpDownArchivesArchiveInterval.Size = new System.Drawing.Size(52, 23);
            this.numericUpDownArchivesArchiveInterval.TabIndex = 58;
            this.toolTipMain.SetToolTip(this.numericUpDownArchivesArchiveInterval, "How often archiving and data retention rules are evaluated. ");
            this.numericUpDownArchivesArchiveInterval.ValueChanged += new System.EventHandler(this.SettingChanged);
            // 
            // comboBoxArchivesLogManagementUnit
            // 
            this.comboBoxArchivesLogManagementUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxArchivesLogManagementUnit.FormattingEnabled = true;
            this.comboBoxArchivesLogManagementUnit.Items.AddRange(new object[] {
            "Minutes",
            "Hours",
            "Days"});
            this.comboBoxArchivesLogManagementUnit.Location = new System.Drawing.Point(325, 87);
            this.comboBoxArchivesLogManagementUnit.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.comboBoxArchivesLogManagementUnit.Name = "comboBoxArchivesLogManagementUnit";
            this.comboBoxArchivesLogManagementUnit.Size = new System.Drawing.Size(103, 23);
            this.comboBoxArchivesLogManagementUnit.TabIndex = 56;
            this.toolTipMain.SetToolTip(this.comboBoxArchivesLogManagementUnit, resources.GetString("comboBoxArchivesLogManagementUnit.ToolTip"));
            this.comboBoxArchivesLogManagementUnit.SelectedIndexChanged += new System.EventHandler(this.SettingChanged);
            // 
            // numericUpDownArchivesLogManagementValue
            // 
            this.numericUpDownArchivesLogManagementValue.Location = new System.Drawing.Point(268, 87);
            this.numericUpDownArchivesLogManagementValue.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.numericUpDownArchivesLogManagementValue.Name = "numericUpDownArchivesLogManagementValue";
            this.numericUpDownArchivesLogManagementValue.Size = new System.Drawing.Size(52, 23);
            this.numericUpDownArchivesLogManagementValue.TabIndex = 55;
            this.toolTipMain.SetToolTip(this.numericUpDownArchivesLogManagementValue, resources.GetString("numericUpDownArchivesLogManagementValue.ToolTip"));
            this.numericUpDownArchivesLogManagementValue.ValueChanged += new System.EventHandler(this.SettingChanged);
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(174, 89);
            this.label33.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(93, 15);
            this.label33.TabIndex = 54;
            this.label33.Text = "Logs Older Than";
            this.toolTipMain.SetToolTip(this.label33, resources.GetString("label33.ToolTip"));
            // 
            // comboBoxArchivesLogManagementType
            // 
            this.comboBoxArchivesLogManagementType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxArchivesLogManagementType.FormattingEnabled = true;
            this.comboBoxArchivesLogManagementType.Items.AddRange(new object[] {
            "Clear",
            "Archive & Clear"});
            this.comboBoxArchivesLogManagementType.Location = new System.Drawing.Point(31, 85);
            this.comboBoxArchivesLogManagementType.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.comboBoxArchivesLogManagementType.Name = "comboBoxArchivesLogManagementType";
            this.comboBoxArchivesLogManagementType.Size = new System.Drawing.Size(140, 23);
            this.comboBoxArchivesLogManagementType.TabIndex = 53;
            this.toolTipMain.SetToolTip(this.comboBoxArchivesLogManagementType, resources.GetString("comboBoxArchivesLogManagementType.ToolTip"));
            this.comboBoxArchivesLogManagementType.SelectedIndexChanged += new System.EventHandler(this.SettingChanged);
            // 
            // checkBoxArchivesLogManagement
            // 
            this.checkBoxArchivesLogManagement.AutoSize = true;
            this.checkBoxArchivesLogManagement.Location = new System.Drawing.Point(12, 89);
            this.checkBoxArchivesLogManagement.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBoxArchivesLogManagement.Name = "checkBoxArchivesLogManagement";
            this.checkBoxArchivesLogManagement.Size = new System.Drawing.Size(15, 14);
            this.checkBoxArchivesLogManagement.TabIndex = 52;
            this.toolTipMain.SetToolTip(this.checkBoxArchivesLogManagement, resources.GetString("checkBoxArchivesLogManagement.ToolTip"));
            this.checkBoxArchivesLogManagement.UseVisualStyleBackColor = true;
            // 
            // checkBoxArchivesArchiveConfig
            // 
            this.checkBoxArchivesArchiveConfig.AutoSize = true;
            this.checkBoxArchivesArchiveConfig.Location = new System.Drawing.Point(12, 150);
            this.checkBoxArchivesArchiveConfig.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBoxArchivesArchiveConfig.Name = "checkBoxArchivesArchiveConfig";
            this.checkBoxArchivesArchiveConfig.Size = new System.Drawing.Size(154, 19);
            this.checkBoxArchivesArchiveConfig.TabIndex = 51;
            this.checkBoxArchivesArchiveConfig.Text = "Archive Config Changes";
            this.toolTipMain.SetToolTip(this.checkBoxArchivesArchiveConfig, resources.GetString("checkBoxArchivesArchiveConfig.ToolTip"));
            this.checkBoxArchivesArchiveConfig.UseVisualStyleBackColor = true;
            this.checkBoxArchivesArchiveConfig.CheckedChanged += new System.EventHandler(this.SettingChanged);
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(251, 183);
            this.label36.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(32, 15);
            this.label36.TabIndex = 68;
            this.label36.Text = "Days";
            this.toolTipMain.SetToolTip(this.label36, "When enabled, archive files older than the specified number of days are deleted f" +
        "rom the Archive Folder.\r\nEvaluated on the archive interval and when closing Mine" +
        "Control.");
            // 
            // checkBoxArchivesDeleteOldFiles
            // 
            this.checkBoxArchivesDeleteOldFiles.AutoSize = true;
            this.checkBoxArchivesDeleteOldFiles.Location = new System.Drawing.Point(12, 182);
            this.checkBoxArchivesDeleteOldFiles.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBoxArchivesDeleteOldFiles.Name = "checkBoxArchivesDeleteOldFiles";
            this.checkBoxArchivesDeleteOldFiles.Size = new System.Drawing.Size(189, 19);
            this.checkBoxArchivesDeleteOldFiles.TabIndex = 67;
            this.checkBoxArchivesDeleteOldFiles.Text = "Delete Archive Files Older Than";
            this.toolTipMain.SetToolTip(this.checkBoxArchivesDeleteOldFiles, "When enabled, archive files older than the specified number of days are deleted f" +
        "rom the Archive Folder.\r\nEvaluated on the archive interval and when closing Mine" +
        "Control.");
            this.checkBoxArchivesDeleteOldFiles.UseVisualStyleBackColor = true;
            // 
            // checkBoxArchivesClearOldCharts
            // 
            this.checkBoxArchivesClearOldCharts.AutoSize = true;
            this.checkBoxArchivesClearOldCharts.Location = new System.Drawing.Point(12, 118);
            this.checkBoxArchivesClearOldCharts.Name = "checkBoxArchivesClearOldCharts";
            this.checkBoxArchivesClearOldCharts.Size = new System.Drawing.Size(173, 19);
            this.checkBoxArchivesClearOldCharts.TabIndex = 69;
            this.checkBoxArchivesClearOldCharts.Text = "Clear Chart Data Older Than";
            this.toolTipMain.SetToolTip(this.checkBoxArchivesClearOldCharts, "When enabled, all chart data older than the specified time will be cleared. Evalu" +
        "ated on the archive interval.");
            this.checkBoxArchivesClearOldCharts.UseVisualStyleBackColor = true;
            // 
            // buttonDataViewSysTrayTooltips
            // 
            this.buttonDataViewSysTrayTooltips.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDataViewSysTrayTooltips.Location = new System.Drawing.Point(747, 0);
            this.buttonDataViewSysTrayTooltips.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonDataViewSysTrayTooltips.Name = "buttonDataViewSysTrayTooltips";
            this.buttonDataViewSysTrayTooltips.Size = new System.Drawing.Size(139, 24);
            this.buttonDataViewSysTrayTooltips.TabIndex = 1;
            this.buttonDataViewSysTrayTooltips.Text = "View SysTray Tooltips";
            this.toolTipMain.SetToolTip(this.buttonDataViewSysTrayTooltips, "Shows what MineControl can currently see on the system tray.\r\nNote: For something" +
        " to be visible here, it must also be CURRENTLY visible on the system tray (not i" +
        "n overflow).");
            this.buttonDataViewSysTrayTooltips.UseVisualStyleBackColor = true;
            this.buttonDataViewSysTrayTooltips.Click += new System.EventHandler(this.buttonDataViewSysTray_Click);
            // 
            // buttonDataRemoveUnusedQueryOptions
            // 
            this.buttonDataRemoveUnusedQueryOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDataRemoveUnusedQueryOptions.Location = new System.Drawing.Point(554, 0);
            this.buttonDataRemoveUnusedQueryOptions.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonDataRemoveUnusedQueryOptions.Name = "buttonDataRemoveUnusedQueryOptions";
            this.buttonDataRemoveUnusedQueryOptions.Size = new System.Drawing.Size(185, 24);
            this.buttonDataRemoveUnusedQueryOptions.TabIndex = 2;
            this.buttonDataRemoveUnusedQueryOptions.Text = "Remove Unused Query Options";
            this.toolTipMain.SetToolTip(this.buttonDataRemoveUnusedQueryOptions, "Removes options in the Query/User Value drop-down that aren\'t currently assigned " +
        "to any data tracking metric.");
            this.buttonDataRemoveUnusedQueryOptions.UseVisualStyleBackColor = true;
            this.buttonDataRemoveUnusedQueryOptions.Click += new System.EventHandler(this.buttonDataRemoveUnusedQueryOptions_Click);
            // 
            // numericUpDownTempGPUShutOffThresholdSecs
            // 
            this.numericUpDownTempGPUShutOffThresholdSecs.Location = new System.Drawing.Point(32, 77);
            this.numericUpDownTempGPUShutOffThresholdSecs.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.numericUpDownTempGPUShutOffThresholdSecs.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.numericUpDownTempGPUShutOffThresholdSecs.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownTempGPUShutOffThresholdSecs.Name = "numericUpDownTempGPUShutOffThresholdSecs";
            this.numericUpDownTempGPUShutOffThresholdSecs.Size = new System.Drawing.Size(41, 23);
            this.numericUpDownTempGPUShutOffThresholdSecs.TabIndex = 30;
            this.toolTipMain.SetToolTip(this.numericUpDownTempGPUShutOffThresholdSecs, resources.GetString("numericUpDownTempGPUShutOffThresholdSecs.ToolTip"));
            this.numericUpDownTempGPUShutOffThresholdSecs.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownTempGPUShutOffThresholdSecs.ValueChanged += new System.EventHandler(this.SettingChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(73, 79);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(121, 15);
            this.label14.TabIndex = 31;
            this.label14.Text = "Secs At Lowest Power";
            this.toolTipMain.SetToolTip(this.label14, resources.GetString("label14.ToolTip"));
            // 
            // numericUpDownTempGPUShutOffSecs
            // 
            this.numericUpDownTempGPUShutOffSecs.Location = new System.Drawing.Point(114, 51);
            this.numericUpDownTempGPUShutOffSecs.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.numericUpDownTempGPUShutOffSecs.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.numericUpDownTempGPUShutOffSecs.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownTempGPUShutOffSecs.Name = "numericUpDownTempGPUShutOffSecs";
            this.numericUpDownTempGPUShutOffSecs.Size = new System.Drawing.Size(50, 23);
            this.numericUpDownTempGPUShutOffSecs.TabIndex = 32;
            this.toolTipMain.SetToolTip(this.numericUpDownTempGPUShutOffSecs, resources.GetString("numericUpDownTempGPUShutOffSecs.ToolTip"));
            this.numericUpDownTempGPUShutOffSecs.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownTempGPUShutOffSecs.ValueChanged += new System.EventHandler(this.SettingChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(165, 54);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(163, 15);
            this.label15.TabIndex = 33;
            this.label15.Text = "Secs If Temp Exceeds Max For";
            this.toolTipMain.SetToolTip(this.label15, resources.GetString("label15.ToolTip"));
            // 
            // checkBoxStopWhenOverheat
            // 
            this.checkBoxStopWhenOverheat.AutoSize = true;
            this.checkBoxStopWhenOverheat.Checked = true;
            this.checkBoxStopWhenOverheat.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxStopWhenOverheat.Location = new System.Drawing.Point(12, 53);
            this.checkBoxStopWhenOverheat.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBoxStopWhenOverheat.Name = "checkBoxStopWhenOverheat";
            this.checkBoxStopWhenOverheat.Size = new System.Drawing.Size(104, 19);
            this.checkBoxStopWhenOverheat.TabIndex = 29;
            this.checkBoxStopWhenOverheat.Text = "Stop Miner For";
            this.toolTipMain.SetToolTip(this.checkBoxStopWhenOverheat, resources.GetString("checkBoxStopWhenOverheat.ToolTip"));
            this.checkBoxStopWhenOverheat.UseVisualStyleBackColor = true;
            this.checkBoxStopWhenOverheat.CheckedChanged += new System.EventHandler(this.SettingChanged);
            // 
            // checkBoxTempStopWhenTempUnknown
            // 
            this.checkBoxTempStopWhenTempUnknown.AutoSize = true;
            this.checkBoxTempStopWhenTempUnknown.Checked = true;
            this.checkBoxTempStopWhenTempUnknown.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxTempStopWhenTempUnknown.Location = new System.Drawing.Point(12, 23);
            this.checkBoxTempStopWhenTempUnknown.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBoxTempStopWhenTempUnknown.Name = "checkBoxTempStopWhenTempUnknown";
            this.checkBoxTempStopWhenTempUnknown.Size = new System.Drawing.Size(214, 19);
            this.checkBoxTempStopWhenTempUnknown.TabIndex = 51;
            this.checkBoxTempStopWhenTempUnknown.Text = "Stop Miner While Temp Is Unknown";
            this.toolTipMain.SetToolTip(this.checkBoxTempStopWhenTempUnknown, "When enabled, MineControl will stop the GPU miner when it can\'t detect GPU/memory" +
        " junction temp.");
            this.checkBoxTempStopWhenTempUnknown.UseVisualStyleBackColor = true;
            this.checkBoxTempStopWhenTempUnknown.CheckedChanged += new System.EventHandler(this.SettingChanged);
            // 
            // numericUpDownTempTryStepUpSecs
            // 
            this.numericUpDownTempTryStepUpSecs.Location = new System.Drawing.Point(213, 216);
            this.numericUpDownTempTryStepUpSecs.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.numericUpDownTempTryStepUpSecs.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numericUpDownTempTryStepUpSecs.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownTempTryStepUpSecs.Name = "numericUpDownTempTryStepUpSecs";
            this.numericUpDownTempTryStepUpSecs.Size = new System.Drawing.Size(52, 23);
            this.numericUpDownTempTryStepUpSecs.TabIndex = 49;
            this.toolTipMain.SetToolTip(this.numericUpDownTempTryStepUpSecs, "When enabled, MineControl will attempt increasing GPU Power Step if it hasn\'t cha" +
        "nged during the specified number of seconds.");
            this.numericUpDownTempTryStepUpSecs.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numericUpDownTempTryStepUpSecs.ValueChanged += new System.EventHandler(this.SettingChanged);
            // 
            // checkBoxTempTryStepUp
            // 
            this.checkBoxTempTryStepUp.AutoSize = true;
            this.checkBoxTempTryStepUp.Location = new System.Drawing.Point(13, 217);
            this.checkBoxTempTryStepUp.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBoxTempTryStepUp.Name = "checkBoxTempTryStepUp";
            this.checkBoxTempTryStepUp.Size = new System.Drawing.Size(206, 19);
            this.checkBoxTempTryStepUp.TabIndex = 48;
            this.checkBoxTempTryStepUp.Text = "Step Up If No Step Change In Last ";
            this.toolTipMain.SetToolTip(this.checkBoxTempTryStepUp, "When enabled, MineControl will attempt increasing GPU Power Step if it hasn\'t cha" +
        "nged during the specified number of seconds.");
            this.checkBoxTempTryStepUp.UseVisualStyleBackColor = true;
            this.checkBoxTempTryStepUp.CheckedChanged += new System.EventHandler(this.SettingChanged);
            // 
            // buttonScheduleDeleteSchedule
            // 
            this.buttonScheduleDeleteSchedule.Location = new System.Drawing.Point(686, 6);
            this.buttonScheduleDeleteSchedule.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonScheduleDeleteSchedule.Name = "buttonScheduleDeleteSchedule";
            this.buttonScheduleDeleteSchedule.Size = new System.Drawing.Size(88, 27);
            this.buttonScheduleDeleteSchedule.TabIndex = 7;
            this.buttonScheduleDeleteSchedule.Text = "Delete";
            this.toolTipMain.SetToolTip(this.buttonScheduleDeleteSchedule, "Delete the selected schedule");
            this.buttonScheduleDeleteSchedule.UseVisualStyleBackColor = true;
            this.buttonScheduleDeleteSchedule.Click += new System.EventHandler(this.buttonScheduleDeleteSchedule_Click);
            // 
            // buttonScheduleDuplicateSchedule
            // 
            this.buttonScheduleDuplicateSchedule.Location = new System.Drawing.Point(592, 6);
            this.buttonScheduleDuplicateSchedule.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonScheduleDuplicateSchedule.Name = "buttonScheduleDuplicateSchedule";
            this.buttonScheduleDuplicateSchedule.Size = new System.Drawing.Size(88, 27);
            this.buttonScheduleDuplicateSchedule.TabIndex = 6;
            this.buttonScheduleDuplicateSchedule.Text = "Duplicate";
            this.toolTipMain.SetToolTip(this.buttonScheduleDuplicateSchedule, "Duplicate the selected schedule");
            this.buttonScheduleDuplicateSchedule.UseVisualStyleBackColor = true;
            this.buttonScheduleDuplicateSchedule.Click += new System.EventHandler(this.buttonScheduleDuplicateSchedule_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(8, 123);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(50, 15);
            this.label12.TabIndex = 5;
            this.label12.Text = "Content";
            this.toolTipMain.SetToolTip(this.label12, "Content of the selected schedule");
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 53);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(39, 15);
            this.label11.TabIndex = 4;
            this.label11.Text = "Name";
            this.toolTipMain.SetToolTip(this.label11, "Name of the selected schedule");
            // 
            // textBoxScheduleName
            // 
            this.textBoxScheduleName.Location = new System.Drawing.Point(12, 72);
            this.textBoxScheduleName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBoxScheduleName.Name = "textBoxScheduleName";
            this.textBoxScheduleName.Size = new System.Drawing.Size(466, 23);
            this.textBoxScheduleName.TabIndex = 3;
            this.toolTipMain.SetToolTip(this.textBoxScheduleName, "Name of the selected schedule");
            this.textBoxScheduleName.TextChanged += new System.EventHandler(this.textBoxScheduleName_TextChanged);
            this.textBoxScheduleName.Leave += new System.EventHandler(this.textBoxScheduleName_Leave);
            // 
            // treeViewSchedule
            // 
            this.treeViewSchedule.HideSelection = false;
            this.treeViewSchedule.Location = new System.Drawing.Point(12, 142);
            this.treeViewSchedule.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.treeViewSchedule.Name = "treeViewSchedule";
            this.treeViewSchedule.Size = new System.Drawing.Size(466, 346);
            this.treeViewSchedule.TabIndex = 2;
            this.toolTipMain.SetToolTip(this.treeViewSchedule, "Content of the selected schedule");
            // 
            // buttonScheduleCreateSchedule
            // 
            this.buttonScheduleCreateSchedule.Location = new System.Drawing.Point(497, 6);
            this.buttonScheduleCreateSchedule.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonScheduleCreateSchedule.Name = "buttonScheduleCreateSchedule";
            this.buttonScheduleCreateSchedule.Size = new System.Drawing.Size(88, 27);
            this.buttonScheduleCreateSchedule.TabIndex = 1;
            this.buttonScheduleCreateSchedule.Text = "Create New";
            this.toolTipMain.SetToolTip(this.buttonScheduleCreateSchedule, "Create a new empty schedule");
            this.buttonScheduleCreateSchedule.UseVisualStyleBackColor = true;
            this.buttonScheduleCreateSchedule.Click += new System.EventHandler(this.buttonScheduleCreateSchedule_Click);
            // 
            // buttonChartClearData
            // 
            this.buttonChartClearData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonChartClearData.Location = new System.Drawing.Point(584, 595);
            this.buttonChartClearData.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonChartClearData.Name = "buttonChartClearData";
            this.buttonChartClearData.Size = new System.Drawing.Size(70, 23);
            this.buttonChartClearData.TabIndex = 19;
            this.buttonChartClearData.Text = "Clear Data";
            this.toolTipMain.SetToolTip(this.buttonChartClearData, "Clears ALL data on ALL charts.");
            this.buttonChartClearData.UseVisualStyleBackColor = true;
            this.buttonChartClearData.Click += new System.EventHandler(this.buttonChartClearData_Click);
            // 
            // comboBoxChartShowLastUnit
            // 
            this.comboBoxChartShowLastUnit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxChartShowLastUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxChartShowLastUnit.FormattingEnabled = true;
            this.comboBoxChartShowLastUnit.Items.AddRange(new object[] {
            "Minutes",
            "Hours",
            "Days"});
            this.comboBoxChartShowLastUnit.Location = new System.Drawing.Point(230, 597);
            this.comboBoxChartShowLastUnit.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.comboBoxChartShowLastUnit.Name = "comboBoxChartShowLastUnit";
            this.comboBoxChartShowLastUnit.Size = new System.Drawing.Size(89, 23);
            this.comboBoxChartShowLastUnit.TabIndex = 18;
            this.toolTipMain.SetToolTip(this.comboBoxChartShowLastUnit, "Limits visible area of all charts. Doesn\'t maintain itself over time.");
            this.comboBoxChartShowLastUnit.SelectedValueChanged += new System.EventHandler(this.charts_ShowConfigOptionsChanged);
            // 
            // numericUpDownChartShowLastX
            // 
            this.numericUpDownChartShowLastX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numericUpDownChartShowLastX.Location = new System.Drawing.Point(169, 597);
            this.numericUpDownChartShowLastX.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.numericUpDownChartShowLastX.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownChartShowLastX.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownChartShowLastX.Name = "numericUpDownChartShowLastX";
            this.numericUpDownChartShowLastX.Size = new System.Drawing.Size(56, 23);
            this.numericUpDownChartShowLastX.TabIndex = 17;
            this.toolTipMain.SetToolTip(this.numericUpDownChartShowLastX, "Limits visible area of all charts. Doesn\'t maintain itself over time.");
            this.numericUpDownChartShowLastX.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownChartShowLastX.ValueChanged += new System.EventHandler(this.charts_ShowConfigOptionsChanged);
            // 
            // radioButtonChartShowLastX
            // 
            this.radioButtonChartShowLastX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.radioButtonChartShowLastX.AutoSize = true;
            this.radioButtonChartShowLastX.Location = new System.Drawing.Point(93, 597);
            this.radioButtonChartShowLastX.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.radioButtonChartShowLastX.Name = "radioButtonChartShowLastX";
            this.radioButtonChartShowLastX.Size = new System.Drawing.Size(78, 19);
            this.radioButtonChartShowLastX.TabIndex = 16;
            this.radioButtonChartShowLastX.Text = "Show Last";
            this.toolTipMain.SetToolTip(this.radioButtonChartShowLastX, "Limits visible data of all charts by X axis (time). Doesn\'t maintain itself over " +
        "time.");
            this.radioButtonChartShowLastX.UseVisualStyleBackColor = true;
            this.radioButtonChartShowLastX.CheckedChanged += new System.EventHandler(this.charts_ShowConfigOptionsChanged);
            // 
            // radioButtonChartShowAll
            // 
            this.radioButtonChartShowAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.radioButtonChartShowAll.AutoSize = true;
            this.radioButtonChartShowAll.Checked = true;
            this.radioButtonChartShowAll.Location = new System.Drawing.Point(9, 597);
            this.radioButtonChartShowAll.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.radioButtonChartShowAll.Name = "radioButtonChartShowAll";
            this.radioButtonChartShowAll.Size = new System.Drawing.Size(71, 19);
            this.radioButtonChartShowAll.TabIndex = 14;
            this.radioButtonChartShowAll.TabStop = true;
            this.radioButtonChartShowAll.Text = "Show All";
            this.toolTipMain.SetToolTip(this.radioButtonChartShowAll, "Shows all available chart data on the X (time) axis.");
            this.radioButtonChartShowAll.UseVisualStyleBackColor = true;
            this.radioButtonChartShowAll.CheckedChanged += new System.EventHandler(this.charts_ShowConfigOptionsChanged);
            // 
            // numericUpDownChartMinGPUTempOnYAxisValue
            // 
            this.numericUpDownChartMinGPUTempOnYAxisValue.Location = new System.Drawing.Point(162, 18);
            this.numericUpDownChartMinGPUTempOnYAxisValue.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.numericUpDownChartMinGPUTempOnYAxisValue.Name = "numericUpDownChartMinGPUTempOnYAxisValue";
            this.numericUpDownChartMinGPUTempOnYAxisValue.Size = new System.Drawing.Size(40, 23);
            this.numericUpDownChartMinGPUTempOnYAxisValue.TabIndex = 21;
            this.toolTipMain.SetToolTip(this.numericUpDownChartMinGPUTempOnYAxisValue, resources.GetString("numericUpDownChartMinGPUTempOnYAxisValue.ToolTip"));
            this.numericUpDownChartMinGPUTempOnYAxisValue.Value = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.numericUpDownChartMinGPUTempOnYAxisValue.ValueChanged += new System.EventHandler(this.SettingChanged);
            // 
            // checkBoxChartMinGPUTempOnYAxisEnabled
            // 
            this.checkBoxChartMinGPUTempOnYAxisEnabled.AutoSize = true;
            this.checkBoxChartMinGPUTempOnYAxisEnabled.Checked = true;
            this.checkBoxChartMinGPUTempOnYAxisEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxChartMinGPUTempOnYAxisEnabled.Location = new System.Drawing.Point(12, 18);
            this.checkBoxChartMinGPUTempOnYAxisEnabled.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBoxChartMinGPUTempOnYAxisEnabled.Name = "checkBoxChartMinGPUTempOnYAxisEnabled";
            this.checkBoxChartMinGPUTempOnYAxisEnabled.Size = new System.Drawing.Size(140, 19);
            this.checkBoxChartMinGPUTempOnYAxisEnabled.TabIndex = 20;
            this.checkBoxChartMinGPUTempOnYAxisEnabled.Text = "Min GPU Temp On Y2";
            this.toolTipMain.SetToolTip(this.checkBoxChartMinGPUTempOnYAxisEnabled, resources.GetString("checkBoxChartMinGPUTempOnYAxisEnabled.ToolTip"));
            this.checkBoxChartMinGPUTempOnYAxisEnabled.UseVisualStyleBackColor = true;
            this.checkBoxChartMinGPUTempOnYAxisEnabled.CheckedChanged += new System.EventHandler(this.SettingChanged);
            // 
            // checkBoxLogAutoScroll
            // 
            this.checkBoxLogAutoScroll.AutoSize = true;
            this.checkBoxLogAutoScroll.Checked = true;
            this.checkBoxLogAutoScroll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxLogAutoScroll.Location = new System.Drawing.Point(7, 18);
            this.checkBoxLogAutoScroll.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBoxLogAutoScroll.Name = "checkBoxLogAutoScroll";
            this.checkBoxLogAutoScroll.Size = new System.Drawing.Size(81, 19);
            this.checkBoxLogAutoScroll.TabIndex = 1;
            this.checkBoxLogAutoScroll.Text = "AutoScroll";
            this.toolTipMain.SetToolTip(this.checkBoxLogAutoScroll, "When checked, grid auto-scrolls to bottom as new entries are added.");
            this.checkBoxLogAutoScroll.UseVisualStyleBackColor = true;
            // 
            // checkBoxLogColorCode
            // 
            this.checkBoxLogColorCode.AutoSize = true;
            this.checkBoxLogColorCode.Location = new System.Drawing.Point(126, 18);
            this.checkBoxLogColorCode.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBoxLogColorCode.Name = "checkBoxLogColorCode";
            this.checkBoxLogColorCode.Size = new System.Drawing.Size(86, 19);
            this.checkBoxLogColorCode.TabIndex = 6;
            this.checkBoxLogColorCode.Text = "Color Code";
            this.toolTipMain.SetToolTip(this.checkBoxLogColorCode, "When checked, MineControl color codes log entries.\r\nNote: any original color codi" +
        "ng from miners is always lost.");
            this.checkBoxLogColorCode.UseVisualStyleBackColor = true;
            // 
            // comboBoxLogFilter
            // 
            this.comboBoxLogFilter.FormattingEnabled = true;
            this.comboBoxLogFilter.Items.AddRange(new object[] {
            "None",
            "Source = \'MineControl\'",
            "Source = \'GPU Miner\'",
            "Source = \'CPU Miner\'",
            "Type = \'Info\'",
            "Type = \'Warning\'",
            "Type = \'Error\'",
            "Type = \'Input\'"});
            this.comboBoxLogFilter.Location = new System.Drawing.Point(279, 16);
            this.comboBoxLogFilter.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.comboBoxLogFilter.Name = "comboBoxLogFilter";
            this.comboBoxLogFilter.Size = new System.Drawing.Size(231, 23);
            this.comboBoxLogFilter.TabIndex = 5;
            this.toolTipMain.SetToolTip(this.comboBoxLogFilter, "Customizable filter for log entries");
            this.comboBoxLogFilter.SelectedIndexChanged += new System.EventHandler(this.comboBoxLogFilter_Changed);
            this.comboBoxLogFilter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBoxLogFilter_KeyDown);
            this.comboBoxLogFilter.Validated += new System.EventHandler(this.comboBoxLogFilter_Changed);
            // 
            // checkBoxEnableTempControl
            // 
            this.checkBoxEnableTempControl.AutoSize = true;
            this.checkBoxEnableTempControl.Location = new System.Drawing.Point(382, 21);
            this.checkBoxEnableTempControl.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBoxEnableTempControl.Name = "checkBoxEnableTempControl";
            this.checkBoxEnableTempControl.Size = new System.Drawing.Size(132, 19);
            this.checkBoxEnableTempControl.TabIndex = 2;
            this.checkBoxEnableTempControl.Text = "Manage GPU Temps";
            this.toolTipMain.SetToolTip(this.checkBoxEnableTempControl, "When checked, GPU temp management rules (like power stepping) are evaluated. \r\nWh" +
        "en unchecked, no GPU temp rules or failsafes are evaluated.");
            this.checkBoxEnableTempControl.UseVisualStyleBackColor = true;
            this.checkBoxEnableTempControl.CheckedChanged += new System.EventHandler(this.SettingChanged);
            this.checkBoxEnableTempControl.Click += new System.EventHandler(this.SettingChanged);
            // 
            // checkBoxEnableMinerAutomation
            // 
            this.checkBoxEnableMinerAutomation.AutoSize = true;
            this.checkBoxEnableMinerAutomation.Location = new System.Drawing.Point(201, 21);
            this.checkBoxEnableMinerAutomation.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBoxEnableMinerAutomation.Name = "checkBoxEnableMinerAutomation";
            this.checkBoxEnableMinerAutomation.Size = new System.Drawing.Size(131, 19);
            this.checkBoxEnableMinerAutomation.TabIndex = 3;
            this.checkBoxEnableMinerAutomation.Text = "Run Enabled Miners";
            this.toolTipMain.SetToolTip(this.checkBoxEnableMinerAutomation, resources.GetString("checkBoxEnableMinerAutomation.ToolTip"));
            this.checkBoxEnableMinerAutomation.UseVisualStyleBackColor = true;
            this.checkBoxEnableMinerAutomation.CheckedChanged += new System.EventHandler(this.SettingChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 27);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 15);
            this.label5.TabIndex = 6;
            this.label5.Text = "Min";
            this.toolTipMain.SetToolTip(this.label5, resources.GetString("label5.ToolTip"));
            // 
            // buttonDataLoadPreset
            // 
            this.buttonDataLoadPreset.Location = new System.Drawing.Point(88, 0);
            this.buttonDataLoadPreset.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonDataLoadPreset.Name = "buttonDataLoadPreset";
            this.buttonDataLoadPreset.Size = new System.Drawing.Size(80, 24);
            this.buttonDataLoadPreset.TabIndex = 3;
            this.buttonDataLoadPreset.Text = "Load Preset";
            this.toolTipMain.SetToolTip(this.buttonDataLoadPreset, "Loads metric settings from a preset file. This will only update metric options fo" +
        "r metrics in the preset file.");
            this.buttonDataLoadPreset.UseVisualStyleBackColor = true;
            this.buttonDataLoadPreset.Click += new System.EventHandler(this.buttonDataLoadPreset_Click);
            // 
            // buttonDataSavePreset
            // 
            this.buttonDataSavePreset.Location = new System.Drawing.Point(173, 0);
            this.buttonDataSavePreset.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonDataSavePreset.Name = "buttonDataSavePreset";
            this.buttonDataSavePreset.Size = new System.Drawing.Size(80, 24);
            this.buttonDataSavePreset.TabIndex = 4;
            this.buttonDataSavePreset.Text = "Save Preset";
            this.toolTipMain.SetToolTip(this.buttonDataSavePreset, "Saves current metric options to a preset file, optionally excluding internal and " +
        "untracked metrics from the output.");
            this.buttonDataSavePreset.UseVisualStyleBackColor = true;
            this.buttonDataSavePreset.Click += new System.EventHandler(this.buttonDataSavePreset_Click);
            // 
            // openFileDialogAppPath
            // 
            this.openFileDialogAppPath.FileName = "openFileDialogAppPath";
            // 
            // tabControlMain
            // 
            this.tabControlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlMain.Controls.Add(this.tabPageConfig);
            this.tabControlMain.Controls.Add(this.tabPageSchedule);
            this.tabControlMain.Controls.Add(this.tabPageAnalytics);
            this.tabControlMain.Controls.Add(this.tabPageAbout);
            this.tabControlMain.Location = new System.Drawing.Point(1, 2);
            this.tabControlMain.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(922, 943);
            this.tabControlMain.TabIndex = 7;
            this.tabControlMain.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControlMain_Selected);
            // 
            // tabPageConfig
            // 
            this.tabPageConfig.Controls.Add(this.groupBox5);
            this.tabPageConfig.Controls.Add(this.groupBoxGeneral);
            this.tabPageConfig.Controls.Add(this.groupBox4);
            this.tabPageConfig.Controls.Add(this.groupBox6);
            this.tabPageConfig.Controls.Add(this.groupBoxDataTracking);
            this.tabPageConfig.Controls.Add(this.groupBox2);
            this.tabPageConfig.Controls.Add(this.groupBoxMinersAndApps);
            this.tabPageConfig.Location = new System.Drawing.Point(4, 24);
            this.tabPageConfig.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabPageConfig.Name = "tabPageConfig";
            this.tabPageConfig.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabPageConfig.Size = new System.Drawing.Size(914, 915);
            this.tabPageConfig.TabIndex = 0;
            this.tabPageConfig.Text = "Config";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.checkBoxMinerCPUShowLogs);
            this.groupBox5.Controls.Add(this.checkBoxMinerCPUUserActivityShutoff);
            this.groupBox5.Controls.Add(this.comboBoxMinerCPUSchedule);
            this.groupBox5.Controls.Add(this.radioButtonCPUModeDontControl);
            this.groupBox5.Controls.Add(this.radioButtonCPUModeAlwaysOff);
            this.groupBox5.Controls.Add(this.radioButtonCPUModeAlwaysOn);
            this.groupBox5.Controls.Add(this.radioButtonCPUModeSchedule);
            this.groupBox5.Location = new System.Drawing.Point(463, 151);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox5.Size = new System.Drawing.Size(443, 109);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "CPU Miner";
            // 
            // comboBoxMinerCPUSchedule
            // 
            this.comboBoxMinerCPUSchedule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMinerCPUSchedule.DropDownWidth = 600;
            this.comboBoxMinerCPUSchedule.FormattingEnabled = true;
            this.comboBoxMinerCPUSchedule.Location = new System.Drawing.Point(114, 46);
            this.comboBoxMinerCPUSchedule.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.comboBoxMinerCPUSchedule.Name = "comboBoxMinerCPUSchedule";
            this.comboBoxMinerCPUSchedule.Size = new System.Drawing.Size(315, 23);
            this.comboBoxMinerCPUSchedule.TabIndex = 8;
            this.comboBoxMinerCPUSchedule.SelectedIndexChanged += new System.EventHandler(this.SettingChanged);
            // 
            // groupBoxGeneral
            // 
            this.groupBoxGeneral.Controls.Add(this.buttonGeneralResetConfig);
            this.groupBoxGeneral.Controls.Add(this.label30);
            this.groupBoxGeneral.Controls.Add(this.checkBoxMinimizeToSysTray);
            this.groupBoxGeneral.Controls.Add(this.numericUpDownMinerUserActivityTimeout);
            this.groupBoxGeneral.Controls.Add(this.buttonGeneralExportConfig);
            this.groupBoxGeneral.Controls.Add(this.checkBoxStartupMinimize);
            this.groupBoxGeneral.Controls.Add(this.buttonGeneralImportConfig);
            this.groupBoxGeneral.Controls.Add(this.label21);
            this.groupBoxGeneral.Controls.Add(this.checkBoxStartUpAutomation);
            this.groupBoxGeneral.Controls.Add(this.label23);
            this.groupBoxGeneral.Controls.Add(this.label22);
            this.groupBoxGeneral.Controls.Add(this.numericUpDownTempPollingInterval);
            this.groupBoxGeneral.Location = new System.Drawing.Point(463, 498);
            this.groupBoxGeneral.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBoxGeneral.Name = "groupBoxGeneral";
            this.groupBoxGeneral.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBoxGeneral.Size = new System.Drawing.Size(443, 190);
            this.groupBoxGeneral.TabIndex = 6;
            this.groupBoxGeneral.TabStop = false;
            this.groupBoxGeneral.Text = "General";
            // 
            // buttonGeneralResetConfig
            // 
            this.buttonGeneralResetConfig.Location = new System.Drawing.Point(327, 81);
            this.buttonGeneralResetConfig.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonGeneralResetConfig.Name = "buttonGeneralResetConfig";
            this.buttonGeneralResetConfig.Size = new System.Drawing.Size(105, 27);
            this.buttonGeneralResetConfig.TabIndex = 65;
            this.buttonGeneralResetConfig.Text = "Reset Config...";
            this.buttonGeneralResetConfig.UseVisualStyleBackColor = true;
            this.buttonGeneralResetConfig.Click += new System.EventHandler(this.buttonGeneralResetConfig_Click);
            // 
            // numericUpDownMinerUserActivityTimeout
            // 
            this.numericUpDownMinerUserActivityTimeout.Location = new System.Drawing.Point(131, 147);
            this.numericUpDownMinerUserActivityTimeout.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.numericUpDownMinerUserActivityTimeout.Name = "numericUpDownMinerUserActivityTimeout";
            this.numericUpDownMinerUserActivityTimeout.Size = new System.Drawing.Size(47, 23);
            this.numericUpDownMinerUserActivityTimeout.TabIndex = 7;
            this.toolTipNotification.SetToolTip(this.numericUpDownMinerUserActivityTimeout, "How long MineControl will wait after the last user activity before considering th" +
        "e user inactive.");
            this.numericUpDownMinerUserActivityTimeout.ValueChanged += new System.EventHandler(this.SettingChanged);
            // 
            // buttonGeneralExportConfig
            // 
            this.buttonGeneralExportConfig.Location = new System.Drawing.Point(327, 49);
            this.buttonGeneralExportConfig.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonGeneralExportConfig.Name = "buttonGeneralExportConfig";
            this.buttonGeneralExportConfig.Size = new System.Drawing.Size(105, 27);
            this.buttonGeneralExportConfig.TabIndex = 64;
            this.buttonGeneralExportConfig.Text = "Export Config...";
            this.buttonGeneralExportConfig.UseVisualStyleBackColor = true;
            this.buttonGeneralExportConfig.Click += new System.EventHandler(this.buttonBackupsExportConfig_Click);
            // 
            // buttonGeneralImportConfig
            // 
            this.buttonGeneralImportConfig.Location = new System.Drawing.Point(327, 17);
            this.buttonGeneralImportConfig.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonGeneralImportConfig.Name = "buttonGeneralImportConfig";
            this.buttonGeneralImportConfig.Size = new System.Drawing.Size(105, 27);
            this.buttonGeneralImportConfig.TabIndex = 63;
            this.buttonGeneralImportConfig.Text = "Import Config...";
            this.buttonGeneralImportConfig.UseVisualStyleBackColor = true;
            this.buttonGeneralImportConfig.Click += new System.EventHandler(this.buttonBackupsImportConfig_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.checkBoxMinerGPUShowLogs);
            this.groupBox4.Controls.Add(this.checkBoxMinerGPUUserActivityShutoff);
            this.groupBox4.Controls.Add(this.comboBoxMinerGPUSchedule);
            this.groupBox4.Controls.Add(this.radioButtonGPUModeDontControl);
            this.groupBox4.Controls.Add(this.radioButtonGPUModeAlwaysOff);
            this.groupBox4.Controls.Add(this.radioButtonGPUModeAlwaysOn);
            this.groupBox4.Controls.Add(this.radioButtonGPUModeSchedule);
            this.groupBox4.Location = new System.Drawing.Point(8, 151);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox4.Size = new System.Drawing.Size(442, 109);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "GPU Miner";
            // 
            // comboBoxMinerGPUSchedule
            // 
            this.comboBoxMinerGPUSchedule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMinerGPUSchedule.DropDownWidth = 600;
            this.comboBoxMinerGPUSchedule.FormattingEnabled = true;
            this.comboBoxMinerGPUSchedule.Location = new System.Drawing.Point(114, 46);
            this.comboBoxMinerGPUSchedule.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.comboBoxMinerGPUSchedule.Name = "comboBoxMinerGPUSchedule";
            this.comboBoxMinerGPUSchedule.Size = new System.Drawing.Size(314, 23);
            this.comboBoxMinerGPUSchedule.TabIndex = 6;
            this.comboBoxMinerGPUSchedule.SelectedIndexChanged += new System.EventHandler(this.SettingChanged);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.comboBoxArchivesClearOldChartsUnit);
            this.groupBox6.Controls.Add(this.numericUpDownArchivesClearOldChartsValue);
            this.groupBox6.Controls.Add(this.numericUpDownArchivesDeleteOldFilesDays);
            this.groupBox6.Controls.Add(this.buttonBackupsBackupFolder);
            this.groupBox6.Controls.Add(this.textBoxArchivesArchiveFolder);
            this.groupBox6.Controls.Add(this.label35);
            this.groupBox6.Controls.Add(this.comboBoxArchivesArchiveIntervalUnit);
            this.groupBox6.Controls.Add(this.numericUpDownArchivesArchiveInterval);
            this.groupBox6.Controls.Add(this.label34);
            this.groupBox6.Controls.Add(this.comboBoxArchivesLogManagementUnit);
            this.groupBox6.Controls.Add(this.numericUpDownArchivesLogManagementValue);
            this.groupBox6.Controls.Add(this.label33);
            this.groupBox6.Controls.Add(this.comboBoxArchivesLogManagementType);
            this.groupBox6.Controls.Add(this.checkBoxArchivesLogManagement);
            this.groupBox6.Controls.Add(this.checkBoxArchivesArchiveConfig);
            this.groupBox6.Controls.Add(this.label36);
            this.groupBox6.Controls.Add(this.checkBoxArchivesDeleteOldFiles);
            this.groupBox6.Controls.Add(this.checkBoxArchivesClearOldCharts);
            this.groupBox6.Location = new System.Drawing.Point(463, 269);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox6.Size = new System.Drawing.Size(443, 221);
            this.groupBox6.TabIndex = 7;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Archives And Data Retention";
            // 
            // buttonBackupsBackupFolder
            // 
            this.buttonBackupsBackupFolder.Location = new System.Drawing.Point(401, 20);
            this.buttonBackupsBackupFolder.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonBackupsBackupFolder.Name = "buttonBackupsBackupFolder";
            this.buttonBackupsBackupFolder.Size = new System.Drawing.Size(28, 25);
            this.buttonBackupsBackupFolder.TabIndex = 62;
            this.buttonBackupsBackupFolder.Text = "...";
            this.buttonBackupsBackupFolder.UseVisualStyleBackColor = true;
            this.buttonBackupsBackupFolder.Click += new System.EventHandler(this.buttonBackupsBackupFolder_Click);
            // 
            // groupBoxDataTracking
            // 
            this.groupBoxDataTracking.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxDataTracking.Controls.Add(this.dataGridViewMetrics);
            this.groupBoxDataTracking.Controls.Add(this.buttonDataViewSysTrayTooltips);
            this.groupBoxDataTracking.Controls.Add(this.buttonDataRemoveUnusedQueryOptions);
            this.groupBoxDataTracking.Controls.Add(this.buttonDataLoadPreset);
            this.groupBoxDataTracking.Controls.Add(this.buttonDataSavePreset);
            this.groupBoxDataTracking.Location = new System.Drawing.Point(8, 694);
            this.groupBoxDataTracking.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBoxDataTracking.Name = "groupBoxDataTracking";
            this.groupBoxDataTracking.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBoxDataTracking.Size = new System.Drawing.Size(897, 215);
            this.groupBoxDataTracking.TabIndex = 9;
            this.groupBoxDataTracking.TabStop = false;
            this.groupBoxDataTracking.Text = "Data Tracking";
            // 
            // dataGridViewMetrics
            // 
            this.dataGridViewMetrics.AllowUserToAddRows = false;
            this.dataGridViewMetrics.AllowUserToDeleteRows = false;
            this.dataGridViewMetrics.AllowUserToResizeRows = false;
            this.dataGridViewMetrics.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridViewMetrics.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMetrics.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColDataEnableTracking,
            this.ColDataMetric,
            this.ColDataType,
            this.ColDataInputSource,
            this.ColDataMethod,
            this.ColDataQuery});
            this.dataGridViewMetrics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewMetrics.Location = new System.Drawing.Point(4, 19);
            this.dataGridViewMetrics.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dataGridViewMetrics.Name = "dataGridViewMetrics";
            this.dataGridViewMetrics.RowHeadersVisible = false;
            this.dataGridViewMetrics.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridViewMetrics.Size = new System.Drawing.Size(889, 193);
            this.dataGridViewMetrics.TabIndex = 0;
            this.dataGridViewMetrics.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewMetrics_CellEnter);
            this.dataGridViewMetrics.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridViewMetrics_CellValidating);
            this.dataGridViewMetrics.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellValueChanged);
            this.dataGridViewMetrics.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridViewMetrics_CurrentCellDirtyStateChanged);
            this.dataGridViewMetrics.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridViewMetrics_DataError);
            this.dataGridViewMetrics.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridViewMetrics_EditingControlShowing);
            this.dataGridViewMetrics.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridViewMetrics_RowsAdded);
            // 
            // ColDataEnableTracking
            // 
            this.ColDataEnableTracking.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.ColDataEnableTracking.HeaderText = "Track";
            this.ColDataEnableTracking.Name = "ColDataEnableTracking";
            this.ColDataEnableTracking.ToolTipText = "Whether the metric will be tracked in charts and stats.";
            this.ColDataEnableTracking.Width = 40;
            // 
            // ColDataMetric
            // 
            this.ColDataMetric.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ColDataMetric.HeaderText = "Metric";
            this.ColDataMetric.Name = "ColDataMetric";
            this.ColDataMetric.ReadOnly = true;
            this.ColDataMetric.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColDataMetric.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColDataMetric.ToolTipText = "Name of the metric";
            this.ColDataMetric.Width = 47;
            // 
            // ColDataType
            // 
            this.ColDataType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ColDataType.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.ColDataType.HeaderText = "Type";
            this.ColDataType.Name = "ColDataType";
            this.ColDataType.ReadOnly = true;
            this.ColDataType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColDataType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ColDataType.ToolTipText = "Type of the metric. Number = numeric, Selection = text/string.";
            this.ColDataType.Width = 56;
            // 
            // ColDataInputSource
            // 
            this.ColDataInputSource.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ColDataInputSource.HeaderText = "Source";
            this.ColDataInputSource.Name = "ColDataInputSource";
            this.ColDataInputSource.ToolTipText = "Source of the input, i.e. which thing the metric\'s data comes from.";
            this.ColDataInputSource.Width = 49;
            // 
            // ColDataMethod
            // 
            this.ColDataMethod.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ColDataMethod.HeaderText = "Method";
            this.ColDataMethod.Name = "ColDataMethod";
            this.ColDataMethod.ToolTipText = "How data is queried once it\'s been read from the source.";
            this.ColDataMethod.Width = 55;
            // 
            // ColDataQuery
            // 
            this.ColDataQuery.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColDataQuery.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.ColDataQuery.DropDownWidth = 500;
            this.ColDataQuery.HeaderText = "Query/User Value";
            this.ColDataQuery.Name = "ColDataQuery";
            this.ColDataQuery.ToolTipText = "RegEx query string for \"RegEx\" method, or the canned user value for the \"UserValu" +
    "e\" method";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox7);
            this.groupBox2.Controls.Add(this.groupBox9);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.numericUpDownTempTryStepUpSecs);
            this.groupBox2.Controls.Add(this.checkBoxTempTryStepUp);
            this.groupBox2.Controls.Add(this.textBoxTempPowerStepParam1);
            this.groupBox2.Controls.Add(this.textBoxTempPowerStepParam2);
            this.groupBox2.Controls.Add(this.textBoxTempPowerStepParam3);
            this.groupBox2.Controls.Add(this.textBoxTempPowerStepParam4);
            this.groupBox2.Controls.Add(this.textBoxTempPowerStepParam5);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.numericUpDownTempSteppingBuffer);
            this.groupBox2.Controls.Add(this.label24);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.labelMaxSpeed);
            this.groupBox2.Controls.Add(this.labelMinSpeed);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.trackBarGPUPowerStep);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Location = new System.Drawing.Point(9, 269);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox2.Size = new System.Drawing.Size(441, 419);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "GPU Temperature Management";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.numericUpDownTempMax);
            this.groupBox7.Controls.Add(this.numericUpDownTempMin);
            this.groupBox7.Controls.Add(this.label5);
            this.groupBox7.Controls.Add(this.label6);
            this.groupBox7.Location = new System.Drawing.Point(264, 24);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(163, 60);
            this.groupBox7.TabIndex = 55;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Target Temps";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(83, 27);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 15);
            this.label6.TabIndex = 7;
            this.label6.Text = "Max";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.numericUpDownTempGPUShutOffThresholdSecs);
            this.groupBox9.Controls.Add(this.label14);
            this.groupBox9.Controls.Add(this.numericUpDownTempGPUShutOffSecs);
            this.groupBox9.Controls.Add(this.label15);
            this.groupBox9.Controls.Add(this.checkBoxStopWhenOverheat);
            this.groupBox9.Controls.Add(this.checkBoxTempStopWhenTempUnknown);
            this.groupBox9.Location = new System.Drawing.Point(14, 256);
            this.groupBox9.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox9.Size = new System.Drawing.Size(413, 147);
            this.groupBox9.TabIndex = 52;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "GPU Failsafe Protections";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(100, 24);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(131, 15);
            this.label9.TabIndex = 19;
            this.label9.Text = "Command Line Params";
            // 
            // labelMaxSpeed
            // 
            this.labelMaxSpeed.Location = new System.Drawing.Point(12, 51);
            this.labelMaxSpeed.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelMaxSpeed.Name = "labelMaxSpeed";
            this.labelMaxSpeed.Size = new System.Drawing.Size(35, 15);
            this.labelMaxSpeed.TabIndex = 17;
            this.labelMaxSpeed.Text = "High";
            this.labelMaxSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelMinSpeed
            // 
            this.labelMinSpeed.Location = new System.Drawing.Point(12, 176);
            this.labelMinSpeed.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelMinSpeed.Name = "labelMinSpeed";
            this.labelMinSpeed.Size = new System.Drawing.Size(35, 15);
            this.labelMinSpeed.TabIndex = 16;
            this.labelMinSpeed.Text = "Low";
            this.labelMinSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 24);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 15);
            this.label8.TabIndex = 14;
            this.label8.Text = "Power Steps";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(686, 65);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(0, 15);
            this.label7.TabIndex = 8;
            // 
            // groupBoxMinersAndApps
            // 
            this.groupBoxMinersAndApps.Controls.Add(this.dataGridViewApps);
            this.groupBoxMinersAndApps.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBoxMinersAndApps.Location = new System.Drawing.Point(8, 3);
            this.groupBoxMinersAndApps.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBoxMinersAndApps.Name = "groupBoxMinersAndApps";
            this.groupBoxMinersAndApps.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBoxMinersAndApps.Size = new System.Drawing.Size(897, 142);
            this.groupBoxMinersAndApps.TabIndex = 2;
            this.groupBoxMinersAndApps.TabStop = false;
            this.groupBoxMinersAndApps.Text = "Miners And Applications";
            // 
            // dataGridViewApps
            // 
            this.dataGridViewApps.AllowUserToAddRows = false;
            this.dataGridViewApps.AllowUserToDeleteRows = false;
            this.dataGridViewApps.AllowUserToResizeRows = false;
            this.dataGridViewApps.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridViewApps.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewApps.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColAppType,
            this.ColAppStatus,
            this.ColAppName,
            this.ColAppPath,
            this.ColAppChooseButton});
            this.dataGridViewApps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewApps.Location = new System.Drawing.Point(4, 16);
            this.dataGridViewApps.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dataGridViewApps.Name = "dataGridViewApps";
            this.dataGridViewApps.RowHeadersVisible = false;
            this.dataGridViewApps.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridViewApps.Size = new System.Drawing.Size(889, 123);
            this.dataGridViewApps.TabIndex = 1;
            this.dataGridViewApps.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewApps_CellContentClick);
            this.dataGridViewApps.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewApps_CellDoubleClick);
            this.dataGridViewApps.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellValueChanged);
            // 
            // ColAppType
            // 
            this.ColAppType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ColAppType.Frozen = true;
            this.ColAppType.HeaderText = "App Type";
            this.ColAppType.Name = "ColAppType";
            this.ColAppType.ReadOnly = true;
            this.ColAppType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColAppType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColAppType.Width = 59;
            // 
            // ColAppStatus
            // 
            this.ColAppStatus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ColAppStatus.Frozen = true;
            this.ColAppStatus.HeaderText = "Status";
            this.ColAppStatus.Name = "ColAppStatus";
            this.ColAppStatus.ReadOnly = true;
            this.ColAppStatus.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColAppStatus.Width = 43;
            // 
            // ColAppName
            // 
            this.ColAppName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ColAppName.Frozen = true;
            this.ColAppName.HeaderText = "Process Name";
            this.ColAppName.Name = "ColAppName";
            this.ColAppName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColAppName.ToolTipText = "Exact name of launched process as shown in Task Manager, WITHOUT file extension";
            this.ColAppName.Width = 82;
            // 
            // ColAppPath
            // 
            this.ColAppPath.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColAppPath.HeaderText = "Full Start Path";
            this.ColAppPath.Name = "ColAppPath";
            this.ColAppPath.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColAppPath.ToolTipText = "Complete path for launching the process, including file name and extension (can b" +
    "e a batch file)";
            // 
            // ColAppChooseButton
            // 
            this.ColAppChooseButton.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ColAppChooseButton.HeaderText = "";
            this.ColAppChooseButton.Name = "ColAppChooseButton";
            this.ColAppChooseButton.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColAppChooseButton.Text = "...";
            this.ColAppChooseButton.UseColumnTextForButtonValue = true;
            this.ColAppChooseButton.Width = 5;
            // 
            // tabPageSchedule
            // 
            this.tabPageSchedule.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageSchedule.Controls.Add(this.buttonScheduleMoveNodeDown);
            this.tabPageSchedule.Controls.Add(this.buttonScheduleMoveNodeUp);
            this.tabPageSchedule.Controls.Add(this.groupBoxScheduleNodeDetails);
            this.tabPageSchedule.Controls.Add(this.buttonScheduleDeleteNode);
            this.tabPageSchedule.Controls.Add(this.buttonScheduleDeleteSchedule);
            this.tabPageSchedule.Controls.Add(this.buttonScheduleDuplicateSchedule);
            this.tabPageSchedule.Controls.Add(this.label12);
            this.tabPageSchedule.Controls.Add(this.label11);
            this.tabPageSchedule.Controls.Add(this.textBoxScheduleName);
            this.tabPageSchedule.Controls.Add(this.treeViewSchedule);
            this.tabPageSchedule.Controls.Add(this.buttonScheduleCreateSchedule);
            this.tabPageSchedule.Controls.Add(this.comboBoxScheduleSchedules);
            this.tabPageSchedule.Location = new System.Drawing.Point(4, 24);
            this.tabPageSchedule.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabPageSchedule.Name = "tabPageSchedule";
            this.tabPageSchedule.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabPageSchedule.Size = new System.Drawing.Size(914, 915);
            this.tabPageSchedule.TabIndex = 2;
            this.tabPageSchedule.Text = "Schedules";
            // 
            // groupBoxScheduleNodeDetails
            // 
            this.groupBoxScheduleNodeDetails.AutoSize = true;
            this.groupBoxScheduleNodeDetails.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBoxScheduleNodeDetails.Controls.Add(this.panelScheduleNodeButtons);
            this.groupBoxScheduleNodeDetails.Controls.Add(this.radioButtonScheduleCalendar);
            this.groupBoxScheduleNodeDetails.Controls.Add(this.groupBoxScheduleCalendar);
            this.groupBoxScheduleNodeDetails.Controls.Add(this.radioButtonScheduleResult);
            this.groupBoxScheduleNodeDetails.Controls.Add(this.groupBoxScheduleWeek);
            this.groupBoxScheduleNodeDetails.Controls.Add(this.groupBoxScheduleResult);
            this.groupBoxScheduleNodeDetails.Controls.Add(this.groupBoxScheduleTime);
            this.groupBoxScheduleNodeDetails.Controls.Add(this.radioButtonScheduleTime);
            this.groupBoxScheduleNodeDetails.Controls.Add(this.radioButtonScheduleDaysOfWeek);
            this.groupBoxScheduleNodeDetails.Location = new System.Drawing.Point(481, 123);
            this.groupBoxScheduleNodeDetails.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBoxScheduleNodeDetails.Name = "groupBoxScheduleNodeDetails";
            this.groupBoxScheduleNodeDetails.Padding = new System.Windows.Forms.Padding(4, 3, 4, 0);
            this.groupBoxScheduleNodeDetails.Size = new System.Drawing.Size(419, 429);
            this.groupBoxScheduleNodeDetails.TabIndex = 23;
            this.groupBoxScheduleNodeDetails.TabStop = false;
            this.groupBoxScheduleNodeDetails.Text = "Node Details";
            // 
            // panelScheduleNodeButtons
            // 
            this.panelScheduleNodeButtons.Controls.Add(this.buttonScheduleUpdateNode);
            this.panelScheduleNodeButtons.Controls.Add(this.buttonScheduleCreateNode);
            this.panelScheduleNodeButtons.Controls.Add(this.buttonScheduleCreateSubNode);
            this.panelScheduleNodeButtons.Location = new System.Drawing.Point(4, 368);
            this.panelScheduleNodeButtons.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panelScheduleNodeButtons.Name = "panelScheduleNodeButtons";
            this.panelScheduleNodeButtons.Size = new System.Drawing.Size(407, 42);
            this.panelScheduleNodeButtons.TabIndex = 25;
            // 
            // radioButtonScheduleCalendar
            // 
            this.radioButtonScheduleCalendar.AutoSize = true;
            this.radioButtonScheduleCalendar.Location = new System.Drawing.Point(7, 22);
            this.radioButtonScheduleCalendar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.radioButtonScheduleCalendar.Name = "radioButtonScheduleCalendar";
            this.radioButtonScheduleCalendar.Size = new System.Drawing.Size(72, 19);
            this.radioButtonScheduleCalendar.TabIndex = 17;
            this.radioButtonScheduleCalendar.Text = "Calendar";
            this.radioButtonScheduleCalendar.UseVisualStyleBackColor = true;
            this.radioButtonScheduleCalendar.CheckedChanged += new System.EventHandler(this.radioButtonScheduleType_CheckedChanged);
            // 
            // groupBoxScheduleCalendar
            // 
            this.groupBoxScheduleCalendar.Controls.Add(this.label17);
            this.groupBoxScheduleCalendar.Controls.Add(this.comboBoxScheduleEndDay);
            this.groupBoxScheduleCalendar.Controls.Add(this.label18);
            this.groupBoxScheduleCalendar.Controls.Add(this.comboBoxScheduleEndMonth);
            this.groupBoxScheduleCalendar.Controls.Add(this.label16);
            this.groupBoxScheduleCalendar.Controls.Add(this.comboBoxScheduleStartDay);
            this.groupBoxScheduleCalendar.Controls.Add(this.label13);
            this.groupBoxScheduleCalendar.Controls.Add(this.comboBoxScheduleStartMonth);
            this.groupBoxScheduleCalendar.Location = new System.Drawing.Point(7, 48);
            this.groupBoxScheduleCalendar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBoxScheduleCalendar.Name = "groupBoxScheduleCalendar";
            this.groupBoxScheduleCalendar.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBoxScheduleCalendar.Size = new System.Drawing.Size(400, 95);
            this.groupBoxScheduleCalendar.TabIndex = 13;
            this.groupBoxScheduleCalendar.TabStop = false;
            this.groupBoxScheduleCalendar.Text = "Calendar";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(216, 57);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(50, 15);
            this.label17.TabIndex = 7;
            this.label17.Text = "End Day";
            // 
            // comboBoxScheduleEndDay
            // 
            this.comboBoxScheduleEndDay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxScheduleEndDay.FormattingEnabled = true;
            this.comboBoxScheduleEndDay.Location = new System.Drawing.Point(292, 53);
            this.comboBoxScheduleEndDay.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.comboBoxScheduleEndDay.Name = "comboBoxScheduleEndDay";
            this.comboBoxScheduleEndDay.Size = new System.Drawing.Size(101, 23);
            this.comboBoxScheduleEndDay.TabIndex = 6;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(216, 25);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(66, 15);
            this.label18.TabIndex = 5;
            this.label18.Text = "End Month";
            // 
            // comboBoxScheduleEndMonth
            // 
            this.comboBoxScheduleEndMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxScheduleEndMonth.FormattingEnabled = true;
            this.comboBoxScheduleEndMonth.Items.AddRange(new object[] {
            "January",
            "February",
            "March",
            "April",
            "May",
            "June",
            "July",
            "August",
            "September",
            "October",
            "November",
            "December"});
            this.comboBoxScheduleEndMonth.Location = new System.Drawing.Point(292, 22);
            this.comboBoxScheduleEndMonth.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.comboBoxScheduleEndMonth.Name = "comboBoxScheduleEndMonth";
            this.comboBoxScheduleEndMonth.Size = new System.Drawing.Size(101, 23);
            this.comboBoxScheduleEndMonth.TabIndex = 4;
            this.comboBoxScheduleEndMonth.SelectedIndexChanged += new System.EventHandler(this.comboBoxScheduleMonth_SelectedIndexChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(7, 57);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(54, 15);
            this.label16.TabIndex = 3;
            this.label16.Text = "Start Day";
            // 
            // comboBoxScheduleStartDay
            // 
            this.comboBoxScheduleStartDay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxScheduleStartDay.FormattingEnabled = true;
            this.comboBoxScheduleStartDay.Location = new System.Drawing.Point(86, 53);
            this.comboBoxScheduleStartDay.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.comboBoxScheduleStartDay.Name = "comboBoxScheduleStartDay";
            this.comboBoxScheduleStartDay.Size = new System.Drawing.Size(101, 23);
            this.comboBoxScheduleStartDay.TabIndex = 2;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(7, 25);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(70, 15);
            this.label13.TabIndex = 1;
            this.label13.Text = "Start Month";
            // 
            // comboBoxScheduleStartMonth
            // 
            this.comboBoxScheduleStartMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxScheduleStartMonth.FormattingEnabled = true;
            this.comboBoxScheduleStartMonth.Items.AddRange(new object[] {
            "January",
            "February",
            "March",
            "April",
            "May",
            "June",
            "July",
            "August",
            "September",
            "October",
            "November",
            "December"});
            this.comboBoxScheduleStartMonth.Location = new System.Drawing.Point(86, 22);
            this.comboBoxScheduleStartMonth.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.comboBoxScheduleStartMonth.Name = "comboBoxScheduleStartMonth";
            this.comboBoxScheduleStartMonth.Size = new System.Drawing.Size(101, 23);
            this.comboBoxScheduleStartMonth.TabIndex = 0;
            this.comboBoxScheduleStartMonth.SelectedIndexChanged += new System.EventHandler(this.comboBoxScheduleMonth_SelectedIndexChanged);
            // 
            // radioButtonScheduleResult
            // 
            this.radioButtonScheduleResult.AutoSize = true;
            this.radioButtonScheduleResult.Location = new System.Drawing.Point(323, 22);
            this.radioButtonScheduleResult.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.radioButtonScheduleResult.Name = "radioButtonScheduleResult";
            this.radioButtonScheduleResult.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.radioButtonScheduleResult.Size = new System.Drawing.Size(57, 19);
            this.radioButtonScheduleResult.TabIndex = 21;
            this.radioButtonScheduleResult.Text = "Result";
            this.radioButtonScheduleResult.UseVisualStyleBackColor = true;
            this.radioButtonScheduleResult.CheckedChanged += new System.EventHandler(this.radioButtonScheduleType_CheckedChanged);
            // 
            // groupBoxScheduleWeek
            // 
            this.groupBoxScheduleWeek.Controls.Add(this.checkBoxScheduleSunday);
            this.groupBoxScheduleWeek.Controls.Add(this.checkBoxScheduleThursday);
            this.groupBoxScheduleWeek.Controls.Add(this.checkBoxScheduleSaturday);
            this.groupBoxScheduleWeek.Controls.Add(this.checkBoxScheduleFriday);
            this.groupBoxScheduleWeek.Controls.Add(this.checkBoxScheduleWednesday);
            this.groupBoxScheduleWeek.Controls.Add(this.checkBoxScheduleTuesday);
            this.groupBoxScheduleWeek.Controls.Add(this.checkBoxScheduleMonday);
            this.groupBoxScheduleWeek.Location = new System.Drawing.Point(7, 150);
            this.groupBoxScheduleWeek.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBoxScheduleWeek.Name = "groupBoxScheduleWeek";
            this.groupBoxScheduleWeek.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBoxScheduleWeek.Size = new System.Drawing.Size(400, 80);
            this.groupBoxScheduleWeek.TabIndex = 14;
            this.groupBoxScheduleWeek.TabStop = false;
            this.groupBoxScheduleWeek.Text = "Days Of Week";
            // 
            // checkBoxScheduleSunday
            // 
            this.checkBoxScheduleSunday.AutoSize = true;
            this.checkBoxScheduleSunday.Location = new System.Drawing.Point(216, 48);
            this.checkBoxScheduleSunday.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBoxScheduleSunday.Name = "checkBoxScheduleSunday";
            this.checkBoxScheduleSunday.Size = new System.Drawing.Size(65, 19);
            this.checkBoxScheduleSunday.TabIndex = 6;
            this.checkBoxScheduleSunday.Text = "Sunday";
            this.checkBoxScheduleSunday.UseVisualStyleBackColor = true;
            // 
            // checkBoxScheduleThursday
            // 
            this.checkBoxScheduleThursday.AutoSize = true;
            this.checkBoxScheduleThursday.Location = new System.Drawing.Point(309, 22);
            this.checkBoxScheduleThursday.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBoxScheduleThursday.Name = "checkBoxScheduleThursday";
            this.checkBoxScheduleThursday.Size = new System.Drawing.Size(74, 19);
            this.checkBoxScheduleThursday.TabIndex = 5;
            this.checkBoxScheduleThursday.Text = "Thursday";
            this.checkBoxScheduleThursday.UseVisualStyleBackColor = true;
            // 
            // checkBoxScheduleSaturday
            // 
            this.checkBoxScheduleSaturday.AutoSize = true;
            this.checkBoxScheduleSaturday.Location = new System.Drawing.Point(115, 48);
            this.checkBoxScheduleSaturday.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBoxScheduleSaturday.Name = "checkBoxScheduleSaturday";
            this.checkBoxScheduleSaturday.Size = new System.Drawing.Size(72, 19);
            this.checkBoxScheduleSaturday.TabIndex = 4;
            this.checkBoxScheduleSaturday.Text = "Saturday";
            this.checkBoxScheduleSaturday.UseVisualStyleBackColor = true;
            // 
            // checkBoxScheduleFriday
            // 
            this.checkBoxScheduleFriday.AutoSize = true;
            this.checkBoxScheduleFriday.Location = new System.Drawing.Point(10, 48);
            this.checkBoxScheduleFriday.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBoxScheduleFriday.Name = "checkBoxScheduleFriday";
            this.checkBoxScheduleFriday.Size = new System.Drawing.Size(58, 19);
            this.checkBoxScheduleFriday.TabIndex = 3;
            this.checkBoxScheduleFriday.Text = "Friday";
            this.checkBoxScheduleFriday.UseVisualStyleBackColor = true;
            // 
            // checkBoxScheduleWednesday
            // 
            this.checkBoxScheduleWednesday.AutoSize = true;
            this.checkBoxScheduleWednesday.Location = new System.Drawing.Point(216, 22);
            this.checkBoxScheduleWednesday.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBoxScheduleWednesday.Name = "checkBoxScheduleWednesday";
            this.checkBoxScheduleWednesday.Size = new System.Drawing.Size(87, 19);
            this.checkBoxScheduleWednesday.TabIndex = 2;
            this.checkBoxScheduleWednesday.Text = "Wednesday";
            this.checkBoxScheduleWednesday.UseVisualStyleBackColor = true;
            // 
            // checkBoxScheduleTuesday
            // 
            this.checkBoxScheduleTuesday.AutoSize = true;
            this.checkBoxScheduleTuesday.Location = new System.Drawing.Point(115, 22);
            this.checkBoxScheduleTuesday.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBoxScheduleTuesday.Name = "checkBoxScheduleTuesday";
            this.checkBoxScheduleTuesday.Size = new System.Drawing.Size(69, 19);
            this.checkBoxScheduleTuesday.TabIndex = 1;
            this.checkBoxScheduleTuesday.Text = "Tuesday";
            this.checkBoxScheduleTuesday.UseVisualStyleBackColor = true;
            // 
            // checkBoxScheduleMonday
            // 
            this.checkBoxScheduleMonday.AutoSize = true;
            this.checkBoxScheduleMonday.Location = new System.Drawing.Point(10, 22);
            this.checkBoxScheduleMonday.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBoxScheduleMonday.Name = "checkBoxScheduleMonday";
            this.checkBoxScheduleMonday.Size = new System.Drawing.Size(70, 19);
            this.checkBoxScheduleMonday.TabIndex = 0;
            this.checkBoxScheduleMonday.Text = "Monday";
            this.checkBoxScheduleMonday.UseVisualStyleBackColor = true;
            // 
            // groupBoxScheduleResult
            // 
            this.groupBoxScheduleResult.Controls.Add(this.comboBoxScheduleResult);
            this.groupBoxScheduleResult.Location = new System.Drawing.Point(7, 302);
            this.groupBoxScheduleResult.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBoxScheduleResult.Name = "groupBoxScheduleResult";
            this.groupBoxScheduleResult.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBoxScheduleResult.Size = new System.Drawing.Size(400, 60);
            this.groupBoxScheduleResult.TabIndex = 20;
            this.groupBoxScheduleResult.TabStop = false;
            this.groupBoxScheduleResult.Text = "Result";
            // 
            // comboBoxScheduleResult
            // 
            this.comboBoxScheduleResult.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxScheduleResult.FormattingEnabled = true;
            this.comboBoxScheduleResult.Items.AddRange(new object[] {
            "Miner On",
            "Miner Off"});
            this.comboBoxScheduleResult.Location = new System.Drawing.Point(13, 22);
            this.comboBoxScheduleResult.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.comboBoxScheduleResult.Name = "comboBoxScheduleResult";
            this.comboBoxScheduleResult.Size = new System.Drawing.Size(377, 23);
            this.comboBoxScheduleResult.TabIndex = 0;
            // 
            // groupBoxScheduleTime
            // 
            this.groupBoxScheduleTime.Controls.Add(this.label20);
            this.groupBoxScheduleTime.Controls.Add(this.dateTimePickerScheduleEndTime);
            this.groupBoxScheduleTime.Controls.Add(this.label19);
            this.groupBoxScheduleTime.Controls.Add(this.dateTimePickerScheduleStartTime);
            this.groupBoxScheduleTime.Location = new System.Drawing.Point(7, 237);
            this.groupBoxScheduleTime.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBoxScheduleTime.Name = "groupBoxScheduleTime";
            this.groupBoxScheduleTime.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBoxScheduleTime.Size = new System.Drawing.Size(400, 58);
            this.groupBoxScheduleTime.TabIndex = 15;
            this.groupBoxScheduleTime.TabStop = false;
            this.groupBoxScheduleTime.Text = "Time";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(216, 25);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(56, 15);
            this.label20.TabIndex = 10;
            this.label20.Text = "End Time";
            // 
            // dateTimePickerScheduleEndTime
            // 
            this.dateTimePickerScheduleEndTime.CustomFormat = "hh:mm tt";
            this.dateTimePickerScheduleEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePickerScheduleEndTime.Location = new System.Drawing.Point(292, 22);
            this.dateTimePickerScheduleEndTime.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dateTimePickerScheduleEndTime.Name = "dateTimePickerScheduleEndTime";
            this.dateTimePickerScheduleEndTime.ShowUpDown = true;
            this.dateTimePickerScheduleEndTime.Size = new System.Drawing.Size(101, 23);
            this.dateTimePickerScheduleEndTime.TabIndex = 9;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(9, 25);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(60, 15);
            this.label19.TabIndex = 8;
            this.label19.Text = "Start Time";
            // 
            // dateTimePickerScheduleStartTime
            // 
            this.dateTimePickerScheduleStartTime.CustomFormat = "HH:MM tt";
            this.dateTimePickerScheduleStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePickerScheduleStartTime.Location = new System.Drawing.Point(86, 22);
            this.dateTimePickerScheduleStartTime.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dateTimePickerScheduleStartTime.Name = "dateTimePickerScheduleStartTime";
            this.dateTimePickerScheduleStartTime.ShowUpDown = true;
            this.dateTimePickerScheduleStartTime.Size = new System.Drawing.Size(101, 23);
            this.dateTimePickerScheduleStartTime.TabIndex = 0;
            this.dateTimePickerScheduleStartTime.Value = new System.DateTime(2021, 9, 4, 12, 27, 57, 0);
            // 
            // radioButtonScheduleTime
            // 
            this.radioButtonScheduleTime.AutoSize = true;
            this.radioButtonScheduleTime.Location = new System.Drawing.Point(236, 22);
            this.radioButtonScheduleTime.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.radioButtonScheduleTime.Name = "radioButtonScheduleTime";
            this.radioButtonScheduleTime.Size = new System.Drawing.Size(51, 19);
            this.radioButtonScheduleTime.TabIndex = 19;
            this.radioButtonScheduleTime.Text = "Time";
            this.radioButtonScheduleTime.UseVisualStyleBackColor = true;
            this.radioButtonScheduleTime.CheckedChanged += new System.EventHandler(this.radioButtonScheduleType_CheckedChanged);
            // 
            // radioButtonScheduleDaysOfWeek
            // 
            this.radioButtonScheduleDaysOfWeek.AutoSize = true;
            this.radioButtonScheduleDaysOfWeek.Location = new System.Drawing.Point(102, 22);
            this.radioButtonScheduleDaysOfWeek.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.radioButtonScheduleDaysOfWeek.Name = "radioButtonScheduleDaysOfWeek";
            this.radioButtonScheduleDaysOfWeek.Size = new System.Drawing.Size(98, 19);
            this.radioButtonScheduleDaysOfWeek.TabIndex = 18;
            this.radioButtonScheduleDaysOfWeek.Text = "Days Of Week";
            this.radioButtonScheduleDaysOfWeek.UseVisualStyleBackColor = true;
            this.radioButtonScheduleDaysOfWeek.CheckedChanged += new System.EventHandler(this.radioButtonScheduleType_CheckedChanged);
            // 
            // comboBoxScheduleSchedules
            // 
            this.comboBoxScheduleSchedules.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxScheduleSchedules.DropDownWidth = 600;
            this.comboBoxScheduleSchedules.FormattingEnabled = true;
            this.comboBoxScheduleSchedules.Location = new System.Drawing.Point(12, 7);
            this.comboBoxScheduleSchedules.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.comboBoxScheduleSchedules.Name = "comboBoxScheduleSchedules";
            this.comboBoxScheduleSchedules.Size = new System.Drawing.Size(466, 23);
            this.comboBoxScheduleSchedules.TabIndex = 0;
            this.comboBoxScheduleSchedules.SelectedIndexChanged += new System.EventHandler(this.comboBoxScheduleSchedules_SelectedIndexChanged);
            // 
            // tabPageAnalytics
            // 
            this.tabPageAnalytics.Controls.Add(this.splitContainer2);
            this.tabPageAnalytics.Location = new System.Drawing.Point(4, 24);
            this.tabPageAnalytics.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabPageAnalytics.Name = "tabPageAnalytics";
            this.tabPageAnalytics.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabPageAnalytics.Size = new System.Drawing.Size(914, 915);
            this.tabPageAnalytics.TabIndex = 1;
            this.tabPageAnalytics.Text = "Analytics";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer2.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.Location = new System.Drawing.Point(7, 6);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer1);
            this.splitContainer2.Panel1.Cursor = System.Windows.Forms.Cursors.Default;
            this.splitContainer2.Panel1MinSize = 100;
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer2.Panel2.Cursor = System.Windows.Forms.Cursors.Default;
            this.splitContainer2.Panel2MinSize = 100;
            this.splitContainer2.Size = new System.Drawing.Size(898, 903);
            this.splitContainer2.SplitterDistance = 628;
            this.splitContainer2.TabIndex = 17;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Cursor = System.Windows.Forms.Cursors.VSplit;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBoxCharts);
            this.splitContainer1.Panel1.Cursor = System.Windows.Forms.Cursors.Default;
            this.splitContainer1.Panel1MinSize = 300;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControlAnalytics);
            this.splitContainer1.Panel2.Cursor = System.Windows.Forms.Cursors.Default;
            this.splitContainer1.Panel2MinSize = 100;
            this.splitContainer1.Size = new System.Drawing.Size(898, 628);
            this.splitContainer1.SplitterDistance = 662;
            this.splitContainer1.TabIndex = 16;
            // 
            // groupBoxCharts
            // 
            this.groupBoxCharts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxCharts.Controls.Add(this.buttonChartUpdate);
            this.groupBoxCharts.Controls.Add(this.tabControlCharts);
            this.groupBoxCharts.Controls.Add(this.buttonChartClearData);
            this.groupBoxCharts.Controls.Add(this.comboBoxChartShowLastUnit);
            this.groupBoxCharts.Controls.Add(this.numericUpDownChartShowLastX);
            this.groupBoxCharts.Controls.Add(this.radioButtonChartShowLastX);
            this.groupBoxCharts.Controls.Add(this.radioButtonChartShowAll);
            this.groupBoxCharts.Location = new System.Drawing.Point(0, 0);
            this.groupBoxCharts.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBoxCharts.Name = "groupBoxCharts";
            this.groupBoxCharts.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBoxCharts.Size = new System.Drawing.Size(662, 628);
            this.groupBoxCharts.TabIndex = 13;
            this.groupBoxCharts.TabStop = false;
            this.groupBoxCharts.Text = "Charts";
            // 
            // buttonChartUpdate
            // 
            this.buttonChartUpdate.Location = new System.Drawing.Point(495, 595);
            this.buttonChartUpdate.Name = "buttonChartUpdate";
            this.buttonChartUpdate.Size = new System.Drawing.Size(82, 23);
            this.buttonChartUpdate.TabIndex = 20;
            this.buttonChartUpdate.Text = "Update View";
            this.buttonChartUpdate.UseVisualStyleBackColor = true;
            this.buttonChartUpdate.Click += new System.EventHandler(this.charts_ShowConfigOptionsChanged);
            // 
            // tabControlCharts
            // 
            this.tabControlCharts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlCharts.Location = new System.Drawing.Point(7, 18);
            this.tabControlCharts.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabControlCharts.Name = "tabControlCharts";
            this.tabControlCharts.SelectedIndex = 0;
            this.tabControlCharts.Size = new System.Drawing.Size(648, 568);
            this.tabControlCharts.TabIndex = 16;
            this.tabControlCharts.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControlCharts_Selected);
            // 
            // tabControlAnalytics
            // 
            this.tabControlAnalytics.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlAnalytics.Controls.Add(this.tabPageStats);
            this.tabControlAnalytics.Controls.Add(this.tabPageAnalyticsOptions);
            this.tabControlAnalytics.Location = new System.Drawing.Point(-1, 3);
            this.tabControlAnalytics.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabControlAnalytics.Name = "tabControlAnalytics";
            this.tabControlAnalytics.SelectedIndex = 0;
            this.tabControlAnalytics.Size = new System.Drawing.Size(235, 628);
            this.tabControlAnalytics.TabIndex = 15;
            // 
            // tabPageStats
            // 
            this.tabPageStats.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageStats.Controls.Add(this.dataGridViewStats);
            this.tabPageStats.Location = new System.Drawing.Point(4, 24);
            this.tabPageStats.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabPageStats.Name = "tabPageStats";
            this.tabPageStats.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabPageStats.Size = new System.Drawing.Size(227, 600);
            this.tabPageStats.TabIndex = 0;
            this.tabPageStats.Text = "Stats";
            // 
            // dataGridViewStats
            // 
            this.dataGridViewStats.AllowUserToAddRows = false;
            this.dataGridViewStats.AllowUserToDeleteRows = false;
            this.dataGridViewStats.AllowUserToResizeRows = false;
            this.dataGridViewStats.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewStats.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewStats.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridViewStats.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewStats.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColStatsStat,
            this.ColStatsValue,
            this.ColStatsLastUpdate});
            this.dataGridViewStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewStats.Location = new System.Drawing.Point(4, 3);
            this.dataGridViewStats.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dataGridViewStats.Name = "dataGridViewStats";
            this.dataGridViewStats.ReadOnly = true;
            this.dataGridViewStats.RowHeadersVisible = false;
            this.dataGridViewStats.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridViewStats.Size = new System.Drawing.Size(219, 594);
            this.dataGridViewStats.TabIndex = 0;
            // 
            // ColStatsStat
            // 
            this.ColStatsStat.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ColStatsStat.DefaultCellStyle = dataGridViewCellStyle4;
            this.ColStatsStat.HeaderText = "Stat";
            this.ColStatsStat.Name = "ColStatsStat";
            this.ColStatsStat.ReadOnly = true;
            this.ColStatsStat.Width = 52;
            // 
            // ColStatsValue
            // 
            this.ColStatsValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.NullValue = null;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ColStatsValue.DefaultCellStyle = dataGridViewCellStyle5;
            this.ColStatsValue.HeaderText = "Value";
            this.ColStatsValue.Name = "ColStatsValue";
            this.ColStatsValue.ReadOnly = true;
            // 
            // ColStatsLastUpdate
            // 
            this.ColStatsLastUpdate.HeaderText = "Last Update";
            this.ColStatsLastUpdate.Name = "ColStatsLastUpdate";
            this.ColStatsLastUpdate.ReadOnly = true;
            this.ColStatsLastUpdate.Visible = false;
            this.ColStatsLastUpdate.Width = 94;
            // 
            // tabPageAnalyticsOptions
            // 
            this.tabPageAnalyticsOptions.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageAnalyticsOptions.Controls.Add(this.numericUpDownChartMinGPUTempOnYAxisValue);
            this.tabPageAnalyticsOptions.Controls.Add(this.checkBoxChartMinGPUTempOnYAxisEnabled);
            this.tabPageAnalyticsOptions.Location = new System.Drawing.Point(4, 24);
            this.tabPageAnalyticsOptions.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabPageAnalyticsOptions.Name = "tabPageAnalyticsOptions";
            this.tabPageAnalyticsOptions.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabPageAnalyticsOptions.Size = new System.Drawing.Size(227, 600);
            this.tabPageAnalyticsOptions.TabIndex = 1;
            this.tabPageAnalyticsOptions.Text = "Options";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.label32);
            this.groupBox3.Controls.Add(this.dataGridViewLog);
            this.groupBox3.Controls.Add(this.checkBoxLogAutoScroll);
            this.groupBox3.Controls.Add(this.checkBoxLogColorCode);
            this.groupBox3.Controls.Add(this.comboBoxLogFilter);
            this.groupBox3.Location = new System.Drawing.Point(0, -4);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox3.Size = new System.Drawing.Size(898, 273);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Log";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(245, 20);
            this.label32.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(33, 15);
            this.label32.TabIndex = 4;
            this.label32.Text = "Filter";
            // 
            // dataGridViewLog
            // 
            this.dataGridViewLog.AllowUserToAddRows = false;
            this.dataGridViewLog.AllowUserToDeleteRows = false;
            this.dataGridViewLog.AllowUserToResizeRows = false;
            this.dataGridViewLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewLog.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            this.dataGridViewLog.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridViewLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewLog.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColLogSource,
            this.ColLogType,
            this.ColLogTime,
            this.ColLogMessage});
            this.dataGridViewLog.GridColor = System.Drawing.SystemColors.ButtonFace;
            this.dataGridViewLog.Location = new System.Drawing.Point(4, 45);
            this.dataGridViewLog.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dataGridViewLog.Name = "dataGridViewLog";
            this.dataGridViewLog.RowHeadersVisible = false;
            this.dataGridViewLog.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewLog.ShowEditingIcon = false;
            this.dataGridViewLog.ShowRowErrors = false;
            this.dataGridViewLog.Size = new System.Drawing.Size(891, 225);
            this.dataGridViewLog.TabIndex = 3;
            this.dataGridViewLog.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewLog_CellFormatting);
            // 
            // ColLogSource
            // 
            this.ColLogSource.HeaderText = "Source";
            this.ColLogSource.MinimumWidth = 25;
            this.ColLogSource.Name = "ColLogSource";
            this.ColLogSource.ReadOnly = true;
            this.ColLogSource.Width = 75;
            // 
            // ColLogType
            // 
            this.ColLogType.HeaderText = "Type";
            this.ColLogType.MinimumWidth = 25;
            this.ColLogType.Name = "ColLogType";
            this.ColLogType.ReadOnly = true;
            this.ColLogType.Width = 50;
            // 
            // ColLogTime
            // 
            this.ColLogTime.HeaderText = "Time";
            this.ColLogTime.Name = "ColLogTime";
            this.ColLogTime.ReadOnly = true;
            this.ColLogTime.Width = 115;
            // 
            // ColLogMessage
            // 
            this.ColLogMessage.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ColLogMessage.DefaultCellStyle = dataGridViewCellStyle6;
            this.ColLogMessage.HeaderText = "Message";
            this.ColLogMessage.MinimumWidth = 30;
            this.ColLogMessage.Name = "ColLogMessage";
            this.ColLogMessage.ReadOnly = true;
            // 
            // tabPageAbout
            // 
            this.tabPageAbout.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageAbout.Controls.Add(this.buttonGeneralDisplayIntro);
            this.tabPageAbout.Controls.Add(this.panel1);
            this.tabPageAbout.Controls.Add(this.linkLabelAboutLicense);
            this.tabPageAbout.Controls.Add(this.linkLabelAboutLink);
            this.tabPageAbout.Controls.Add(this.label28);
            this.tabPageAbout.Controls.Add(this.label27);
            this.tabPageAbout.Controls.Add(this.label25);
            this.tabPageAbout.Controls.Add(this.labelAboutVersion);
            this.tabPageAbout.Controls.Add(this.label4);
            this.tabPageAbout.Controls.Add(this.label3);
            this.tabPageAbout.Controls.Add(this.pictureBox1);
            this.tabPageAbout.Location = new System.Drawing.Point(4, 24);
            this.tabPageAbout.Name = "tabPageAbout";
            this.tabPageAbout.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAbout.Size = new System.Drawing.Size(914, 915);
            this.tabPageAbout.TabIndex = 3;
            this.tabPageAbout.Text = "About / Help";
            // 
            // buttonGeneralDisplayIntro
            // 
            this.buttonGeneralDisplayIntro.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.buttonGeneralDisplayIntro.Location = new System.Drawing.Point(20, 119);
            this.buttonGeneralDisplayIntro.Name = "buttonGeneralDisplayIntro";
            this.buttonGeneralDisplayIntro.Size = new System.Drawing.Size(122, 25);
            this.buttonGeneralDisplayIntro.TabIndex = 15;
            this.buttonGeneralDisplayIntro.Text = "Show Intro Help";
            this.buttonGeneralDisplayIntro.UseVisualStyleBackColor = true;
            this.buttonGeneralDisplayIntro.Click += new System.EventHandler(this.buttonGeneralDisplayIntro_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.richTextBoxAboutAttribution);
            this.panel1.Location = new System.Drawing.Point(20, 180);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(869, 368);
            this.panel1.TabIndex = 14;
            // 
            // richTextBoxAboutAttribution
            // 
            this.richTextBoxAboutAttribution.BackColor = System.Drawing.SystemColors.Window;
            this.richTextBoxAboutAttribution.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBoxAboutAttribution.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxAboutAttribution.Location = new System.Drawing.Point(0, 0);
            this.richTextBoxAboutAttribution.Name = "richTextBoxAboutAttribution";
            this.richTextBoxAboutAttribution.ReadOnly = true;
            this.richTextBoxAboutAttribution.Size = new System.Drawing.Size(867, 366);
            this.richTextBoxAboutAttribution.TabIndex = 13;
            this.richTextBoxAboutAttribution.Text = "";
            this.richTextBoxAboutAttribution.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.richTextBoxAboutAttribution_LinkClicked);
            // 
            // linkLabelAboutLicense
            // 
            this.linkLabelAboutLicense.AutoSize = true;
            this.linkLabelAboutLicense.Location = new System.Drawing.Point(69, 77);
            this.linkLabelAboutLicense.Name = "linkLabelAboutLicense";
            this.linkLabelAboutLicense.Size = new System.Drawing.Size(77, 15);
            this.linkLabelAboutLicense.TabIndex = 12;
            this.linkLabelAboutLicense.TabStop = true;
            this.linkLabelAboutLicense.Text = "CC BY-SA 4.0";
            this.linkLabelAboutLicense.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelAboutLicense_LinkClicked);
            // 
            // linkLabelAboutLink
            // 
            this.linkLabelAboutLink.AutoSize = true;
            this.linkLabelAboutLink.Location = new System.Drawing.Point(261, 97);
            this.linkLabelAboutLink.Name = "linkLabelAboutLink";
            this.linkLabelAboutLink.Size = new System.Drawing.Size(252, 15);
            this.linkLabelAboutLink.TabIndex = 11;
            this.linkLabelAboutLink.TabStop = true;
            this.linkLabelAboutLink.Text = "https://github.com/smurferson1/MineControl";
            this.linkLabelAboutLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelGithub_LinkClicked);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(20, 97);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(245, 15);
            this.label28.TabIndex = 10;
            this.label28.Text = "For information, updates, and to donate, visit";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(20, 162);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(65, 15);
            this.label27.TabIndex = 9;
            this.label27.Text = "Attribution";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(20, 76);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(52, 15);
            this.label25.TabIndex = 4;
            this.label25.Text = "License: ";
            // 
            // labelAboutVersion
            // 
            this.labelAboutVersion.AutoSize = true;
            this.labelAboutVersion.Location = new System.Drawing.Point(70, 55);
            this.labelAboutVersion.Name = "labelAboutVersion";
            this.labelAboutVersion.Size = new System.Drawing.Size(23, 15);
            this.labelAboutVersion.TabIndex = 3;
            this.labelAboutVersion.Text = "<>";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 15);
            this.label4.TabIndex = 2;
            this.label4.Text = "Version:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(59, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 21);
            this.label3.TabIndex = 1;
            this.label3.Text = "MineControl";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::MineControl.Properties.Resources.MineControlLogo;
            this.pictureBox1.Location = new System.Drawing.Point(20, 19);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(33, 33);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // groupBoxControl
            // 
            this.groupBoxControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxControl.Controls.Add(this.labelStatusDisplay);
            this.groupBoxControl.Controls.Add(this.label1);
            this.groupBoxControl.Controls.Add(this.buttonStopAutomation);
            this.groupBoxControl.Controls.Add(this.checkBoxEnableMinerAutomation);
            this.groupBoxControl.Controls.Add(this.buttonStartAutomation);
            this.groupBoxControl.Controls.Add(this.checkBoxEnableTempControl);
            this.groupBoxControl.Location = new System.Drawing.Point(6, 951);
            this.groupBoxControl.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBoxControl.Name = "groupBoxControl";
            this.groupBoxControl.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBoxControl.Size = new System.Drawing.Size(912, 52);
            this.groupBoxControl.TabIndex = 8;
            this.groupBoxControl.TabStop = false;
            this.groupBoxControl.Text = "Automation Control";
            // 
            // labelStatusDisplay
            // 
            this.labelStatusDisplay.AutoSize = true;
            this.labelStatusDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelStatusDisplay.ForeColor = System.Drawing.Color.Red;
            this.labelStatusDisplay.Location = new System.Drawing.Point(69, 23);
            this.labelStatusDisplay.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelStatusDisplay.Name = "labelStatusDisplay";
            this.labelStatusDisplay.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelStatusDisplay.Size = new System.Drawing.Size(54, 13);
            this.labelStatusDisplay.TabIndex = 4;
            this.labelStatusDisplay.Text = "Stopped";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(19, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Status";
            // 
            // buttonStopAutomation
            // 
            this.buttonStopAutomation.Location = new System.Drawing.Point(817, 16);
            this.buttonStopAutomation.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonStopAutomation.Name = "buttonStopAutomation";
            this.buttonStopAutomation.Size = new System.Drawing.Size(88, 27);
            this.buttonStopAutomation.TabIndex = 1;
            this.buttonStopAutomation.Text = "Stop";
            this.buttonStopAutomation.UseVisualStyleBackColor = true;
            this.buttonStopAutomation.Click += new System.EventHandler(this.buttonStopAutomation_Click);
            // 
            // buttonStartAutomation
            // 
            this.buttonStartAutomation.Location = new System.Drawing.Point(721, 16);
            this.buttonStartAutomation.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonStartAutomation.Name = "buttonStartAutomation";
            this.buttonStartAutomation.Size = new System.Drawing.Size(88, 27);
            this.buttonStartAutomation.TabIndex = 0;
            this.buttonStartAutomation.Text = "Start";
            this.buttonStartAutomation.UseVisualStyleBackColor = true;
            this.buttonStartAutomation.Click += new System.EventHandler(this.buttonStartAutomation_Click);
            // 
            // notifyIconMain
            // 
            this.notifyIconMain.ContextMenuStrip = this.contextMenuStripSysTray;
            this.notifyIconMain.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIconMain.Icon")));
            this.notifyIconMain.Text = "MineControl";
            this.notifyIconMain.Visible = true;
            this.notifyIconMain.DoubleClick += new System.EventHandler(this.notifyIconMain_Open);
            // 
            // contextMenuStripSysTray
            // 
            this.contextMenuStripSysTray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemSysTrayOpen,
            this.toolStripMenuItemSysTrayExit});
            this.contextMenuStripSysTray.Name = "contextMenuStripSysTray";
            this.contextMenuStripSysTray.Size = new System.Drawing.Size(104, 48);
            // 
            // toolStripMenuItemSysTrayOpen
            // 
            this.toolStripMenuItemSysTrayOpen.Name = "toolStripMenuItemSysTrayOpen";
            this.toolStripMenuItemSysTrayOpen.Size = new System.Drawing.Size(103, 22);
            this.toolStripMenuItemSysTrayOpen.Text = "Open";
            this.toolStripMenuItemSysTrayOpen.Click += new System.EventHandler(this.notifyIconMain_Open);
            // 
            // toolStripMenuItemSysTrayExit
            // 
            this.toolStripMenuItemSysTrayExit.Name = "toolStripMenuItemSysTrayExit";
            this.toolStripMenuItemSysTrayExit.Size = new System.Drawing.Size(103, 22);
            this.toolStripMenuItemSysTrayExit.Text = "Exit";
            this.toolStripMenuItemSysTrayExit.Click += new System.EventHandler(this.toolStripMenuItemSysTrayExit_Click);
            // 
            // toolTipNotification
            // 
            this.toolTipNotification.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTipNotification.ToolTipTitle = "Notification";
            // 
            // openFileDialogBackups
            // 
            this.openFileDialogBackups.FileName = "openFileDialogBackupPath";
            // 
            // openFileDialogPresets
            // 
            this.openFileDialogPresets.Filter = "Metric Presets|*.MetricPreset|All Files|*.*";
            // 
            // saveFileDialogPresets
            // 
            this.saveFileDialogPresets.DefaultExt = "MetricPreset";
            this.saveFileDialogPresets.Filter = "Metric Presets|*.MetricPreset";
            // 
            // FormMineControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(922, 1011);
            this.Controls.Add(this.tabControlMain);
            this.Controls.Add(this.groupBoxControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MinimumSize = new System.Drawing.Size(938, 1050);
            this.Name = "FormMineControl";
            this.Text = "MineControl";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMineControl_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMineControl_FormClosed);
            this.Load += new System.EventHandler(this.FormMineControl_Load);
            this.Shown += new System.EventHandler(this.FormMineControl_Shown);
            this.Resize += new System.EventHandler(this.FormMineControl_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTempSteppingBuffer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTempMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarGPUPowerStep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTempMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTempPollingInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownArchivesClearOldChartsValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownArchivesDeleteOldFilesDays)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownArchivesArchiveInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownArchivesLogManagementValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTempGPUShutOffThresholdSecs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTempGPUShutOffSecs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTempTryStepUpSecs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownChartShowLastX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownChartMinGPUTempOnYAxisValue)).EndInit();
            this.tabControlMain.ResumeLayout(false);
            this.tabPageConfig.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBoxGeneral.ResumeLayout(false);
            this.groupBoxGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinerUserActivityTimeout)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBoxDataTracking.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMetrics)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBoxMinersAndApps.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewApps)).EndInit();
            this.tabPageSchedule.ResumeLayout(false);
            this.tabPageSchedule.PerformLayout();
            this.groupBoxScheduleNodeDetails.ResumeLayout(false);
            this.groupBoxScheduleNodeDetails.PerformLayout();
            this.panelScheduleNodeButtons.ResumeLayout(false);
            this.groupBoxScheduleCalendar.ResumeLayout(false);
            this.groupBoxScheduleCalendar.PerformLayout();
            this.groupBoxScheduleWeek.ResumeLayout(false);
            this.groupBoxScheduleWeek.PerformLayout();
            this.groupBoxScheduleResult.ResumeLayout(false);
            this.groupBoxScheduleTime.ResumeLayout(false);
            this.groupBoxScheduleTime.PerformLayout();
            this.tabPageAnalytics.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBoxCharts.ResumeLayout(false);
            this.groupBoxCharts.PerformLayout();
            this.tabControlAnalytics.ResumeLayout(false);
            this.tabPageStats.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStats)).EndInit();
            this.tabPageAnalyticsOptions.ResumeLayout(false);
            this.tabPageAnalyticsOptions.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLog)).EndInit();
            this.tabPageAbout.ResumeLayout(false);
            this.tabPageAbout.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBoxControl.ResumeLayout(false);
            this.groupBoxControl.PerformLayout();
            this.contextMenuStripSysTray.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion        
        private System.Windows.Forms.Timer timerMain;
        private System.Windows.Forms.ToolTip toolTipMain;
        private System.Windows.Forms.OpenFileDialog openFileDialogAppPath;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageConfig;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownTempSteppingBuffer;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.NumericUpDown numericUpDownTempPollingInterval;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.NumericUpDown numericUpDownTempGPUShutOffSecs;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.NumericUpDown numericUpDownTempGPUShutOffThresholdSecs;
        private System.Windows.Forms.CheckBox checkBoxStopWhenOverheat;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label labelMaxSpeed;
        private System.Windows.Forms.Label labelMinSpeed;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TrackBar trackBarGPUPowerStep;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox checkBoxEnableTempControl;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton radioButtonCPUModeAlwaysOn;
        private System.Windows.Forms.RadioButton radioButtonCPUModeSchedule;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton radioButtonGPUModeAlwaysOn;
        private System.Windows.Forms.RadioButton radioButtonGPUModeSchedule;
        private System.Windows.Forms.CheckBox checkBoxEnableMinerAutomation;
        private System.Windows.Forms.GroupBox groupBoxMinersAndApps;
        private System.Windows.Forms.DataGridView dataGridViewApps;
        private System.Windows.Forms.TabPage tabPageAnalytics;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox checkBoxLogAutoScroll;
        private System.Windows.Forms.GroupBox groupBoxControl;
        private System.Windows.Forms.CheckBox checkBoxStartupMinimize;
        private System.Windows.Forms.Label labelStatusDisplay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxStartUpAutomation;
        private System.Windows.Forms.Button buttonStopAutomation;
        private System.Windows.Forms.Button buttonStartAutomation;
        private System.Windows.Forms.GroupBox groupBoxCharts;
        private System.Windows.Forms.RadioButton radioButtonChartShowLastX;
        private System.Windows.Forms.RadioButton radioButtonChartShowAll;        
        private System.Windows.Forms.NotifyIcon notifyIconMain;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripSysTray;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSysTrayExit;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSysTrayOpen;
        private System.Windows.Forms.RadioButton radioButtonCPUModeAlwaysOff;
        private System.Windows.Forms.RadioButton radioButtonGPUModeAlwaysOff;
        private System.Windows.Forms.ComboBox comboBoxChartShowLastUnit;
        private System.Windows.Forms.NumericUpDown numericUpDownChartShowLastX;
        private System.Windows.Forms.GroupBox groupBoxGeneral;
        private System.Windows.Forms.CheckBox checkBoxMinimizeToSysTray;
        private System.Windows.Forms.DataGridView dataGridViewStats;
        private System.Windows.Forms.Button buttonChartClearData;
        private System.Windows.Forms.TextBox textBoxTempPowerStepParam5;
        private System.Windows.Forms.TextBox textBoxTempPowerStepParam1;
        private System.Windows.Forms.TextBox textBoxTempPowerStepParam2;
        private System.Windows.Forms.TextBox textBoxTempPowerStepParam3;
        private System.Windows.Forms.TextBox textBoxTempPowerStepParam4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown numericUpDownTempTryStepUpSecs;
        private System.Windows.Forms.CheckBox checkBoxTempTryStepUp;
        private System.Windows.Forms.RadioButton radioButtonCPUModeDontControl;
        private System.Windows.Forms.RadioButton radioButtonGPUModeDontControl;
        private System.Windows.Forms.CheckBox checkBoxTempStopWhenTempUnknown;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.ComboBox comboBoxMinerCPUSchedule;
        private System.Windows.Forms.ComboBox comboBoxMinerGPUSchedule;
        private System.Windows.Forms.TabPage tabPageSchedule;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBoxScheduleName;
        private System.Windows.Forms.Button buttonScheduleCreateSchedule;
        private System.Windows.Forms.ComboBox comboBoxScheduleSchedules;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button buttonScheduleDeleteSchedule;
        private System.Windows.Forms.Button buttonScheduleDuplicateSchedule;
        private System.Windows.Forms.Button buttonScheduleCreateNode;
        private System.Windows.Forms.TreeView treeViewSchedule;
        private System.Windows.Forms.Button buttonScheduleDeleteNode;
        private System.Windows.Forms.Button buttonScheduleCreateSubNode;
        private System.Windows.Forms.NumericUpDown numericUpDownChartMinGPUTempOnYAxisValue;
        private System.Windows.Forms.CheckBox checkBoxChartMinGPUTempOnYAxisEnabled;
        private System.Windows.Forms.CheckBox checkBoxMinerCPUUserActivityShutoff;
        private System.Windows.Forms.CheckBox checkBoxMinerGPUUserActivityShutoff;
        private System.Windows.Forms.NumericUpDown numericUpDownMinerUserActivityTimeout;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.GroupBox groupBoxScheduleNodeDetails;
        private System.Windows.Forms.RadioButton radioButtonScheduleCalendar;
        private System.Windows.Forms.GroupBox groupBoxScheduleCalendar;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox comboBoxScheduleEndDay;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ComboBox comboBoxScheduleEndMonth;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox comboBoxScheduleStartDay;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox comboBoxScheduleStartMonth;
        private System.Windows.Forms.RadioButton radioButtonScheduleResult;
        private System.Windows.Forms.GroupBox groupBoxScheduleWeek;
        private System.Windows.Forms.CheckBox checkBoxScheduleSunday;
        private System.Windows.Forms.CheckBox checkBoxScheduleThursday;
        private System.Windows.Forms.CheckBox checkBoxScheduleSaturday;
        private System.Windows.Forms.CheckBox checkBoxScheduleFriday;
        private System.Windows.Forms.CheckBox checkBoxScheduleWednesday;
        private System.Windows.Forms.CheckBox checkBoxScheduleTuesday;
        private System.Windows.Forms.CheckBox checkBoxScheduleMonday;
        private System.Windows.Forms.GroupBox groupBoxScheduleResult;
        private System.Windows.Forms.ComboBox comboBoxScheduleResult;
        private System.Windows.Forms.GroupBox groupBoxScheduleTime;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.DateTimePicker dateTimePickerScheduleEndTime;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.DateTimePicker dateTimePickerScheduleStartTime;
        private System.Windows.Forms.RadioButton radioButtonScheduleTime;
        private System.Windows.Forms.RadioButton radioButtonScheduleDaysOfWeek;
        private System.Windows.Forms.Button buttonScheduleUpdateNode;
        private System.Windows.Forms.Panel panelScheduleNodeButtons;
        private System.Windows.Forms.CheckBox checkBoxMinerCPUShowLogs;
        private System.Windows.Forms.CheckBox checkBoxMinerGPUShowLogs;
        private System.Windows.Forms.TabControl tabControlAnalytics;
        private System.Windows.Forms.TabPage tabPageStats;
        private System.Windows.Forms.TabPage tabPageAnalyticsOptions;
        private System.Windows.Forms.TabControl tabControlCharts;        
        private System.Windows.Forms.ToolTip toolTipNotification;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.DataGridView dataGridViewLog;
        private System.Windows.Forms.ComboBox comboBoxLogFilter;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.CheckBox checkBoxLogColorCode;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.ComboBox comboBoxArchivesArchiveIntervalUnit;
        private System.Windows.Forms.NumericUpDown numericUpDownArchivesArchiveInterval;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.ComboBox comboBoxArchivesLogManagementUnit;
        private System.Windows.Forms.NumericUpDown numericUpDownArchivesLogManagementValue;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.ComboBox comboBoxArchivesLogManagementType;
        private System.Windows.Forms.CheckBox checkBoxArchivesLogManagement;
        private System.Windows.Forms.CheckBox checkBoxArchivesArchiveConfig;
        private System.Windows.Forms.Button buttonGeneralExportConfig;
        private System.Windows.Forms.Button buttonGeneralImportConfig;
        private System.Windows.Forms.Button buttonBackupsBackupFolder;
        private System.Windows.Forms.TextBox textBoxArchivesArchiveFolder;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.OpenFileDialog openFileDialogBackups;
        private System.Windows.Forms.SaveFileDialog saveFileDialogBackups;
        private System.Windows.Forms.NumericUpDown numericUpDownArchivesDeleteOldFilesDays;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.CheckBox checkBoxArchivesDeleteOldFiles;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogBackups;
        private System.Windows.Forms.GroupBox groupBoxDataTracking;
        private System.Windows.Forms.DataGridView dataGridViewMetrics;
        private System.Windows.Forms.Button buttonDataViewSysTrayTooltips;
        private System.Windows.Forms.NumericUpDown numericUpDownTempMax;
        private System.Windows.Forms.NumericUpDown numericUpDownTempMin;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.ComboBox comboBoxArchivesClearOldChartsUnit;
        private System.Windows.Forms.NumericUpDown numericUpDownArchivesClearOldChartsValue;
        private System.Windows.Forms.CheckBox checkBoxArchivesClearOldCharts;
        private System.Windows.Forms.Button buttonGeneralResetConfig;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColLogSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColLogType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColLogTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColLogMessage;
        private System.Windows.Forms.TabPage tabPageAbout;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labelAboutVersion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.LinkLabel linkLabelAboutLink;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Button buttonScheduleMoveNodeDown;
        private System.Windows.Forms.Button buttonScheduleMoveNodeUp;
        private System.Windows.Forms.Button buttonDataRemoveUnusedQueryOptions;
        private System.Windows.Forms.LinkLabel linkLabelAboutLicense;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColDataEnableTracking;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColDataMetric;
        private System.Windows.Forms.DataGridViewComboBoxColumn ColDataType;
        private System.Windows.Forms.DataGridViewComboBoxColumn ColDataInputSource;
        private System.Windows.Forms.DataGridViewComboBoxColumn ColDataMethod;
        private System.Windows.Forms.DataGridViewComboBoxColumn ColDataQuery;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColStatsStat;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColStatsValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColStatsLastUpdate;
        private System.Windows.Forms.Button buttonDataLoadPreset;
        private System.Windows.Forms.OpenFileDialog openFileDialogPresets;
        private System.Windows.Forms.Button buttonDataSavePreset;
        private System.Windows.Forms.SaveFileDialog saveFileDialogPresets;
        private System.Windows.Forms.RichTextBox richTextBoxAboutAttribution;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonChartUpdate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColAppType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColAppStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColAppName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColAppPath;
        private System.Windows.Forms.DataGridViewButtonColumn ColAppChooseButton;
        private System.Windows.Forms.Button buttonGeneralDisplayIntro;
    }
}

