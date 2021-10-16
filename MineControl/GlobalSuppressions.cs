// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Minor Code Smell", "S3604:Member initializer values should not be redundant", Justification = "<Pending>", Scope = "member", Target = "~P:MineControl.FormMineControl.IsInitializing")]
[assembly: SuppressMessage("Minor Code Smell", "S3247:Duplicate casts should not be made", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.FormMineControl.LoadNodesToTreeView(System.Windows.Forms.TreeNodeCollection,System.Collections.Generic.List{MineControl.ScheduleNode},MineControl.Schedule)")]
[assembly: SuppressMessage("Major Code Smell", "S3358:Ternary operators should not be nested", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.FormMineControl.SaveSettingsFromUI(System.Boolean)")]
[assembly: SuppressMessage("Major Code Smell", "S1066:Collapsible \"if\" statements should be merged", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.FormMineControl.DoGPUTempRules")]
[assembly: SuppressMessage("Major Code Smell", "S1168:Empty arrays and collections should be returned instead of null", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.ScheduleNode.GetNodesById(System.Guid)~System.Collections.Generic.List{MineControl.ScheduleNode}")]
[assembly: SuppressMessage("Major Code Smell", "S1168:Empty arrays and collections should be returned instead of null", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.BranchingNode.GetNodesById(System.Guid)~System.Collections.Generic.List{MineControl.ScheduleNode}")]
[assembly: SuppressMessage("Major Code Smell", "S1168:Empty arrays and collections should be returned instead of null", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.Schedule.GetNodesById(System.Guid)~System.Collections.Generic.List{MineControl.ScheduleNode}")]
[assembly: SuppressMessage("Major Code Smell", "S1066:Collapsible \"if\" statements should be merged", Justification = "<Pending>", Scope = "member", Target = "~M:MineControl.Metric.UpdateFromInput(System.String,System.Boolean,System.Boolean)~System.Boolean")]
