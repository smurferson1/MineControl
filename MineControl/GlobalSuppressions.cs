﻿// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Minor Code Smell", "S3604:Member initializer values should not be redundant", Justification = "<Pending>", Scope = "member", Target = "~P:MineControl.FormMineControl.CanSave")]
[assembly: SuppressMessage("Minor Code Smell", "S3247:Duplicate casts should not be made", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.FormMineControl.LoadNodesToTreeView(System.Windows.Forms.TreeNodeCollection,System.Collections.Generic.List{MineControl.ScheduleNode},MineControl.Schedule)")]
[assembly: SuppressMessage("Major Code Smell", "S3358:Ternary operators should not be nested", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.FormMineControl.SaveSettingsFromUI(System.Boolean)")]
[assembly: SuppressMessage("Major Code Smell", "S1066:Collapsible \"if\" statements should be merged", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.FormMineControl.DoGPUTempRules")]
[assembly: SuppressMessage("Major Code Smell", "S1168:Empty arrays and collections should be returned instead of null", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.ScheduleNode.GetNodesById(System.Guid)~System.Collections.Generic.List{MineControl.ScheduleNode}")]
[assembly: SuppressMessage("Major Code Smell", "S1168:Empty arrays and collections should be returned instead of null", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.BranchingNode.GetNodesById(System.Guid)~System.Collections.Generic.List{MineControl.ScheduleNode}")]
[assembly: SuppressMessage("Major Code Smell", "S1168:Empty arrays and collections should be returned instead of null", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.Schedule.GetNodesById(System.Guid)~System.Collections.Generic.List{MineControl.ScheduleNode}")]
[assembly: SuppressMessage("Major Code Smell", "S1066:Collapsible \"if\" statements should be merged", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.Metric.UpdateFromInput(System.String,System.Boolean,System.Boolean)~System.Boolean")]
[assembly: SuppressMessage("Wrong Usage", "DF0022:Marks undisposed objects assinged to a property, originated in an object creation.", Justification = "<Pending>", Scope = "member", Target = "~P:MineControl.FormMineControl.ProcessCPUMiner")]
[assembly: SuppressMessage("Wrong Usage", "DF0022:Marks undisposed objects assinged to a property, originated in an object creation.", Justification = "<Pending>", Scope = "member", Target = "~P:MineControl.FormMineControl.ProcessGPUController")]
[assembly: SuppressMessage("Wrong Usage", "DF0022:Marks undisposed objects assinged to a property, originated in an object creation.", Justification = "<Pending>", Scope = "member", Target = "~P:MineControl.FormMineControl.ProcessGPUMiner")]
[assembly: SuppressMessage("Wrong Usage", "DF0022:Marks undisposed objects assinged to a property, originated in an object creation.", Justification = "<Pending>", Scope = "member", Target = "~P:MineControl.FormMineControl.ProcessHardwareMonitor")]
[assembly: SuppressMessage("Wrong Usage", "DF0020:Marks undisposed objects assinged to a field, originated in an object creation.", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.FormMineControl.InitializeUI")]
[assembly: SuppressMessage("Wrong Usage", "DF0010:Marks undisposed local variables.", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.FormMineControl.CreateChartSeriesForMetric(System.String,MineControl.Metric,System.Windows.Forms.DataVisualization.Charting.SeriesChartType,System.Windows.Forms.DataVisualization.Charting.AxisType)")]
[assembly: SuppressMessage("Wrong Usage", "DF0001:Marks undisposed anonymous objects from method invocations.", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.FormMineControl.UpdatePolledStats")]
[assembly: SuppressMessage("Wrong Usage", "DF0001:Marks undisposed anonymous objects from method invocations.", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.FormMineControl.UpdateChartScales(System.Boolean)")]
[assembly: SuppressMessage("Wrong Usage", "DF0001:Marks undisposed anonymous objects from method invocations.", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.FormMineControl.trackBarGPUPowerStep_Scroll(System.Object,System.EventArgs)")]
[assembly: SuppressMessage("Wrong Usage", "DF0001:Marks undisposed anonymous objects from method invocations.", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.Metric.UpdateFromInput(System.String,System.Boolean,System.Boolean)~System.Boolean")]
[assembly: SuppressMessage("Wrong Usage", "DF0000:Marks undisposed anonymous objects from object creations.", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.Program.Main")]
[assembly: SuppressMessage("Wrong Usage", "DF0033:Marks undisposed objects assinged to a property, originated from a method invocation.", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.SysTrayIcon.SetTextIcon(System.Windows.Forms.NotifyIcon,System.String,System.String,System.Drawing.Color,System.Drawing.Color,System.Drawing.Color,System.Drawing.Color,System.Drawing.Color)")]
[assembly: SuppressMessage("Major Code Smell", "S125:Sections of code should not be commented out", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.SysTrayIcon.UpdateTextIcon(System.Windows.Forms.NotifyIcon,System.Boolean,MineControl.MinerState,MineControl.MinerState)~System.Boolean")]
[assembly: SuppressMessage("Wrong Usage", "DF0033:Marks undisposed objects assinged to a property, originated from a method invocation.", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.SysTrayIcon.SetTextIcon(System.Windows.Forms.NotifyIcon,System.Nullable{System.Char},System.Nullable{System.Char},System.Drawing.Color,System.Drawing.Color,System.Drawing.Color,System.Drawing.Color)")]
[assembly: SuppressMessage("Wrong Usage", "DF0010:Marks undisposed local variables.", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.ChartUtils.GetMaxYValue(System.Windows.Forms.DataVisualization.Charting.Series)~System.Double")]
[assembly: SuppressMessage("Wrong Usage", "DF0001:Marks undisposed anonymous objects from method invocations.", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.ChartUtils.GetMinYValue(System.Windows.Forms.DataVisualization.Charting.Series,System.Double)~System.Double")]
[assembly: SuppressMessage("Wrong Usage", "DF0010:Marks undisposed local variables.", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.ChartUtils.GetMinYValue(System.Windows.Forms.DataVisualization.Charting.Series,System.Double)~System.Double")]
