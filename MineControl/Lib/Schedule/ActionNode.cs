using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MineControl.Lib.Schedule
{
    public class ActionNode : ScheduleNode
    {
        public ScheduleAction SelectedAction { get; set; }

        public ActionNode() : base() { }

        [JsonConstructor]
        public ActionNode(Guid id, ScheduleAction selectedAction) : base(id)
        {
            SelectedAction = selectedAction;
        }

        public override bool Evaluate(List<ScheduleAction> actions)
        {
            actions.Add(SelectedAction);

            // never stop due to an action, since it's not conditional
            return false;
        }

        public override string GetDescription()
        {
            string actionText = "Undefined";
            switch (SelectedAction)
            {
                case ScheduleAction.MinerOff:
                    actionText = "Miner OFF";
                    break;
                case ScheduleAction.MinerOn:
                    actionText = "Miner ON";
                    break;
            }

            return $"Result is '{actionText}'";
        }
    }
}
