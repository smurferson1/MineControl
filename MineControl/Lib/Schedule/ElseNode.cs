using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MineControl.Lib.Schedule
{
    public class ElseNode : BranchingNode
    {
        public ElseNode() : base() { }

        [JsonConstructor]
        public ElseNode(Guid id) : base(id) { }

        public override bool Evaluate(List<ScheduleAction> actions)
        {
            // always evaluate children
            EvaluateChildren(actions);

            // always true
            return true;
        }

        public override string GetDescription()
        {
            return "Else";
        }
    }
}
