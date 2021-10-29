﻿// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Minor Code Smell", "S3604:Member initializer values should not be redundant", Justification = "<Pending>", Scope = "member", Target = "~P:MineControl.FormMineControl.CanSave")]
[assembly: SuppressMessage("Major Code Smell", "S3358:Ternary operators should not be nested", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.FormMineControl.SaveSettingsFromUI(System.Boolean)")]
[assembly: SuppressMessage("Major Code Smell", "S1066:Collapsible \"if\" statements should be merged", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.FormMineControl.DoGPUTempRules")]
[assembly: SuppressMessage("Wrong Usage", "DF0022:Marks undisposed objects assinged to a property, originated in an object creation.", Justification = "<Pending>", Scope = "member", Target = "~P:MineControl.FormMineControl.ProcessGPUController")]
[assembly: SuppressMessage("Wrong Usage", "DF0022:Marks undisposed objects assinged to a property, originated in an object creation.", Justification = "<Pending>", Scope = "member", Target = "~P:MineControl.FormMineControl.ProcessHardwareMonitor")]
[assembly: SuppressMessage("Wrong Usage", "DF0020:Marks undisposed objects assinged to a field, originated in an object creation.", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.FormMineControl.InitializeUI")]
[assembly: SuppressMessage("Wrong Usage", "DF0001:Marks undisposed anonymous objects from method invocations.", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.FormMineControl.UpdatePolledStats")]
[assembly: SuppressMessage("Wrong Usage", "DF0001:Marks undisposed anonymous objects from method invocations.", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.FormMineControl.trackBarGPUPowerStep_Scroll(System.Object,System.EventArgs)")]
[assembly: SuppressMessage("Wrong Usage", "DF0000:Marks undisposed anonymous objects from object creations.", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.Program.Main")]
[assembly: SuppressMessage("Wrong Usage", "DF0010:Marks undisposed local variables.", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.FormMineControl.CreateChartSeriesForMetric(System.String,MineControl.Lib.Metric,System.Windows.Forms.DataVisualization.Charting.SeriesChartType,System.Windows.Forms.DataVisualization.Charting.AxisType)")]
[assembly: SuppressMessage("Wrong Usage", "DF0001:Marks undisposed anonymous objects from method invocations.", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.FormMineControl.UpdateChartScales(System.Boolean,System.Boolean)")]
[assembly: SuppressMessage("Minor Code Smell", "S3247:Duplicate casts should not be made", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.FormMineControl.LoadNodesToTreeView(System.Windows.Forms.TreeNodeCollection,System.Collections.Generic.List{MineControl.Lib.Schedule.ScheduleNode},MineControl.Lib.Schedule.Schedule)")]
[assembly: SuppressMessage("Wrong Usage", "DF0010:Marks undisposed local variables.", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.Lib.Utils.ChartUtils.GetMaxYValue(System.Windows.Forms.DataVisualization.Charting.Series)~System.Double")]
[assembly: SuppressMessage("Wrong Usage", "DF0010:Marks undisposed local variables.", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.Lib.Utils.ChartUtils.CalculateAreaAndTotalTime(System.Windows.Forms.DataVisualization.Charting.Series,MineControl.Lib.CalculationMethod)~System.ValueTuple{System.Double,System.Double}")]
[assembly: SuppressMessage("Wrong Usage", "DF0010:Marks undisposed local variables.", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.Lib.Utils.ChartUtils.GetMinYValue(System.Windows.Forms.DataVisualization.Charting.Series,System.Double)~System.Double")]
[assembly: SuppressMessage("Wrong Usage", "DF0001:Marks undisposed anonymous objects from method invocations.", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.Lib.Utils.ChartUtils.GetMinYValue(System.Windows.Forms.DataVisualization.Charting.Series,System.Double)~System.Double")]
[assembly: SuppressMessage("Wrong Usage", "DF0026:Marks undisposed objects assinged to a property, originated in an object creation.", Justification = "<Pending>", Scope = "member", Target = "~P:MineControl.Lib.Utils.MinerUtils.ProcessGPUMiner")]
[assembly: SuppressMessage("Wrong Usage", "DF0026:Marks undisposed objects assinged to a property, originated in an object creation.", Justification = "<Pending>", Scope = "member", Target = "~P:MineControl.Lib.Utils.MinerUtils.ProcessCPUMiner")]
[assembly: SuppressMessage("Wrong Usage", "DF0026:Marks undisposed objects assinged to a property, originated in an object creation.", Justification = "<Pending>", Scope = "member", Target = "~P:MineControl.Lib.Utils.ProcessUtils.Job")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.FormMineControl.buttonChartClearData_Click(System.Object,System.EventArgs)")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.FormMineControl.buttonScheduleCreateNode_Click(System.Object,System.EventArgs)")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.FormMineControl.buttonScheduleCreateSchedule_Click(System.Object,System.EventArgs)")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.FormMineControl.buttonScheduleCreateSubNode_Click(System.Object,System.EventArgs)")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.FormMineControl.buttonScheduleDeleteNode_Click(System.Object,System.EventArgs)")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.FormMineControl.buttonScheduleDeleteSchedule_Click(System.Object,System.EventArgs)")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.FormMineControl.buttonScheduleDuplicateSchedule_Click(System.Object,System.EventArgs)")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.FormMineControl.buttonScheduleUpdateNode_Click(System.Object,System.EventArgs)")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.FormMineControl.buttonStartAutomation_Click(System.Object,System.EventArgs)")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.FormMineControl.buttonStopAutomation_Click(System.Object,System.EventArgs)")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.FormMineControl.chartMain_GetToolTipText(System.Object,System.Windows.Forms.DataVisualization.Charting.ToolTipEventArgs)")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.FormMineControl.comboBoxLogFilter_Changed(System.Object,System.EventArgs)")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.FormMineControl.comboBoxScheduleSchedules_SelectedIndexChanged(System.Object,System.EventArgs)")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.FormMineControl.dataGridView_CellValueChanged(System.Object,System.Windows.Forms.DataGridViewCellEventArgs)")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.FormMineControl.dataGridViewApps_CellDoubleClick(System.Object,System.Windows.Forms.DataGridViewCellEventArgs)")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.FormMineControl.dataGridViewLog_CellFormatting(System.Object,System.Windows.Forms.DataGridViewCellFormattingEventArgs)")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.FormMineControl.notifyIconMain_Open(System.Object,System.EventArgs)")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.FormMineControl.radioButtonScheduleType_CheckedChanged(System.Object,System.EventArgs)")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.FormMineControl.textBoxScheduleName_Leave(System.Object,System.EventArgs)")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.FormMineControl.textBoxScheduleName_TextChanged(System.Object,System.EventArgs)")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.FormMineControl.timerMain_Tick(System.Object,System.EventArgs)")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.FormMineControl.toolStripMenuItemSysTrayExit_Click(System.Object,System.EventArgs)")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.FormMineControl.trackBarGPUPowerStep_Scroll(System.Object,System.EventArgs)")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.FormMineControl.buttonBackupsBackupFolder_Click(System.Object,System.EventArgs)")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.FormMineControl.buttonBackupsExportConfig_Click(System.Object,System.EventArgs)")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.FormMineControl.buttonBackupsImportConfig_Click(System.Object,System.EventArgs)")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.FormMineControl.comboBoxLogFilter_KeyDown(System.Object,System.Windows.Forms.KeyEventArgs)")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.FormMineControl.dataGridViewMetrics_EditingControlShowing(System.Object,System.Windows.Forms.DataGridViewEditingControlShowingEventArgs)")]
[assembly: SuppressMessage("Style", "IDE0018:Inline variable declaration", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.FormMineControl.DoGPUTempRules")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.FormMineControl.buttonDataLoadPreset_Click(System.Object,System.EventArgs)")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.FormMineControl.buttonDataRemoveUnusedQueryOptions_Click(System.Object,System.EventArgs)")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.FormMineControl.buttonDataSavePreset_Click(System.Object,System.EventArgs)")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.FormMineControl.buttonDataViewSysTray_Click(System.Object,System.EventArgs)")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.FormMineControl.buttonGeneralResetConfig_Click(System.Object,System.EventArgs)")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.FormMineControl.buttonScheduleMoveNodeDown_Click(System.Object,System.EventArgs)")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.FormMineControl.buttonScheduleMoveNodeUp_Click(System.Object,System.EventArgs)")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.FormMineControl.comboBoxScheduleMonth_SelectedIndexChanged(System.Object,System.EventArgs)")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.FormMineControl.dataGridViewApps_CellContentClick(System.Object,System.Windows.Forms.DataGridViewCellEventArgs)")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.FormMineControl.dataGridViewMetrics_CellEnter(System.Object,System.Windows.Forms.DataGridViewCellEventArgs)")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.FormMineControl.dataGridViewMetrics_CellValidating(System.Object,System.Windows.Forms.DataGridViewCellValidatingEventArgs)")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.FormMineControl.dataGridViewMetrics_CurrentCellDirtyStateChanged(System.Object,System.EventArgs)")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.FormMineControl.dataGridViewMetrics_DataError(System.Object,System.Windows.Forms.DataGridViewDataErrorEventArgs)")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.FormMineControl.dataGridViewMetrics_RowsAdded(System.Object,System.Windows.Forms.DataGridViewRowsAddedEventArgs)")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.FormMineControl.linkLabelAboutLicense_LinkClicked(System.Object,System.Windows.Forms.LinkLabelLinkClickedEventArgs)")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.FormMineControl.linkLabelGithub_LinkClicked(System.Object,System.Windows.Forms.LinkLabelLinkClickedEventArgs)")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.FormMineControl.numericUpDownTempMax_ValueChanged(System.Object,System.EventArgs)")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.FormMineControl.numericUpDownTempMin_ValueChanged(System.Object,System.EventArgs)")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.FormMineControl.richTextBoxAboutAttribution_LinkClicked(System.Object,System.Windows.Forms.LinkClickedEventArgs)")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.FormMineControl.tabControlMain_Selected(System.Object,System.Windows.Forms.TabControlEventArgs)")]
[assembly: SuppressMessage("Major Code Smell", "S125:Sections of code should not be commented out", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.Lib.SysTrayIcon.UpdateTextIcon(System.Windows.Forms.NotifyIcon,System.Boolean,MineControl.Lib.MinerState,MineControl.Lib.MinerState,System.Int32,MineControl.Lib.SysTrayIconTextMode)~System.Boolean")]
[assembly: SuppressMessage("Wrong Usage", "DF0033:Marks undisposed objects assinged to a property, originated from a method invocation.", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.Lib.SysTrayIcon.SetTextIcon(System.Windows.Forms.NotifyIcon,System.Nullable{System.Char},System.Nullable{System.Char},System.Drawing.Color,System.Drawing.Color,System.Drawing.Color,System.Drawing.Color)")]
[assembly: SuppressMessage("Major Code Smell", "S112:General exceptions should never be thrown", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.Lib.Archiver.GetArchiveFolder~System.String")]
[assembly: SuppressMessage("Major Code Smell", "S112:General exceptions should never be thrown", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.Lib.Archiver.GetTimeCutoff(System.DateTime,System.String,System.Int32)~System.DateTime")]
[assembly: SuppressMessage("Major Code Smell", "S112:General exceptions should never be thrown", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.Lib.Archiver.StartArchiveAndClear(System.DateTime@)~System.Boolean")]
[assembly: SuppressMessage("Major Code Smell", "S1066:Collapsible \"if\" statements should be merged", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.Lib.Metric.UpdateSeries")]
[assembly: SuppressMessage("Major Code Smell", "S112:General exceptions should never be thrown", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.Lib.Metric.UpdateFromInput(System.String,System.Boolean,System.Boolean)~System.Boolean")]
[assembly: SuppressMessage("Major Code Smell", "S1066:Collapsible \"if\" statements should be merged", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.Lib.Metric.UpdateFromInput(System.String,System.Boolean,System.Boolean)~System.Boolean")]
[assembly: SuppressMessage("Wrong Usage", "DF0001:Marks undisposed anonymous objects from method invocations.", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.Lib.Metric.UpdateFromInput(System.String,System.Boolean,System.Boolean)~System.Boolean")]
[assembly: SuppressMessage("Major Code Smell", "S1168:Empty arrays and collections should be returned instead of null", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.Lib.Schedule.BranchingNode.GetNodesById(System.Guid)~System.Collections.Generic.List{MineControl.Lib.Schedule.ScheduleNode}")]
[assembly: SuppressMessage("Major Code Smell", "S1168:Empty arrays and collections should be returned instead of null", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.Lib.Schedule.Schedule.GetNodesById(System.Guid)~System.Collections.Generic.List{MineControl.Lib.Schedule.ScheduleNode}")]
[assembly: SuppressMessage("Major Code Smell", "S112:General exceptions should never be thrown", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.Lib.Schedule.Schedule.GetNodeParent(System.Nullable{System.Guid})~MineControl.Lib.Schedule.ScheduleNode")]
[assembly: SuppressMessage("Major Code Smell", "S1168:Empty arrays and collections should be returned instead of null", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.Lib.Schedule.ScheduleNode.GetNodesById(System.Guid)~System.Collections.Generic.List{MineControl.Lib.Schedule.ScheduleNode}")]
[assembly: SuppressMessage("Major Code Smell", "S3881:\"IDisposable\" should be implemented correctly", Justification = "<Pending>", Scope = "type", Target = "~T:MineControl.Lib.WinAPI.Job")]
[assembly: SuppressMessage("Major Code Smell", "S112:General exceptions should never be thrown", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.Lib.WinAPI.Job.#ctor")]
[assembly: SuppressMessage("Major Code Smell", "S108:Nested blocks of code should not be left empty", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.Lib.WinAPI.Job.Dispose(System.Boolean)")]
[assembly: SuppressMessage("Wrong Usage", "DF0100:Marks return values that hides the IDisposable implementation of return value.", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.Lib.WinAPI.Job.AddProcess(System.Int32)~System.Boolean")]
[assembly: SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "<Pending>", Scope = "type", Target = "~T:MineControl.Lib.WinAPI.IO_COUNTERS")]
[assembly: SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "<Pending>", Scope = "type", Target = "~T:MineControl.Lib.WinAPI.JOBOBJECT_BASIC_LIMIT_INFORMATION")]
[assembly: SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "<Pending>", Scope = "type", Target = "~T:MineControl.Lib.WinAPI.JOBOBJECT_EXTENDED_LIMIT_INFORMATION")]
[assembly: SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "<Pending>", Scope = "type", Target = "~T:MineControl.Lib.WinAPI.SECURITY_ATTRIBUTES")]
[assembly: SuppressMessage("Major Code Smell", "S1144:Unused private types or members should be removed", Justification = "<Pending>", Scope = "member", Target = "~F:MineControl.Lib.WinAPI.LastUserInput.LastInputInfo.SizeOf")]
[assembly: SuppressMessage("Performance", "CA1806:Do not ignore method results", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.Lib.WinAPI.SysTrayTooltipReader.GetAllSysTrayToolbarText~System.String")]
[assembly: SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.Lib.WinAPI.SysTrayTooltipReader.GetAllSysTrayToolbarText~System.String")]
[assembly: SuppressMessage("Style", "IDE0059:Unnecessary assignment of a value", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.Lib.WinAPI.SysTrayTooltipReader.GetAllSysTrayToolbarText~System.String")]
[assembly: SuppressMessage("Globalization", "CA2101:Specify marshaling for P/Invoke string arguments", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.Lib.WinAPI.SysTrayTooltipReader.FindWindowEx(System.IntPtr,System.IntPtr,System.String,System.String)~System.IntPtr")]
[assembly: SuppressMessage("Critical Code Smell", "S4487:Unread \"private\" fields should be removed", Justification = "<Pending>", Scope = "member", Target = "~F:MineControl.Lib.WinAPI.SysTrayTooltipReader.TBButtonInfoW.cbSize")]
[assembly: SuppressMessage("Major Code Smell", "S1144:Unused private types or members should be removed", Justification = "<Pending>", Scope = "member", Target = "~F:MineControl.Lib.WinAPI.SysTrayTooltipReader.TBButtonInfoW.cchText")]
[assembly: SuppressMessage("Major Code Smell", "S1144:Unused private types or members should be removed", Justification = "<Pending>", Scope = "member", Target = "~F:MineControl.Lib.WinAPI.SysTrayTooltipReader.TBButtonInfoW.cx")]
[assembly: SuppressMessage("Critical Code Smell", "S4487:Unread \"private\" fields should be removed", Justification = "<Pending>", Scope = "member", Target = "~F:MineControl.Lib.WinAPI.SysTrayTooltipReader.TBButtonInfoW.dwMask")]
[assembly: SuppressMessage("Major Code Smell", "S1144:Unused private types or members should be removed", Justification = "<Pending>", Scope = "member", Target = "~F:MineControl.Lib.WinAPI.SysTrayTooltipReader.TBButtonInfoW.fsState")]
[assembly: SuppressMessage("Major Code Smell", "S1144:Unused private types or members should be removed", Justification = "<Pending>", Scope = "member", Target = "~F:MineControl.Lib.WinAPI.SysTrayTooltipReader.TBButtonInfoW.fsStyle")]
[assembly: SuppressMessage("Major Code Smell", "S1144:Unused private types or members should be removed", Justification = "<Pending>", Scope = "member", Target = "~F:MineControl.Lib.WinAPI.SysTrayTooltipReader.TBButtonInfoW.iImage")]
[assembly: SuppressMessage("Major Code Smell", "S1144:Unused private types or members should be removed", Justification = "<Pending>", Scope = "member", Target = "~F:MineControl.Lib.WinAPI.SysTrayTooltipReader.TBButtonInfoW.lParam")]
[assembly: SuppressMessage("Major Code Smell", "S1144:Unused private types or members should be removed", Justification = "<Pending>", Scope = "member", Target = "~F:MineControl.Lib.WinAPI.SysTrayTooltipReader.TBButtonInfoW.pszText")]
