using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MineControl.Lib.Schedule
{
    public class TimeNode : BranchingNode
    {
        public DateTime StartTime { get; set; } = DateTime.MinValue;
        public DateTime EndTime { get; set; } = DateTime.MinValue;

        public TimeNode() : base() { }

        [JsonConstructor]
        public TimeNode(Guid id, DateTime startTime, DateTime endTime) : base(id)
        {
            UpdateTimes(startTime, endTime);
        }

        public void UpdateTimes(DateTime startTime, DateTime endTime)
        {
            StartTime = startTime;
            EndTime = endTime;
        }

        public override bool Evaluate(List<ScheduleAction> actions)
        {
            bool result = false;

            // represent times independently from dates, as timespans
            TimeSpan startSpan = StartTime.TimeOfDay;
            TimeSpan endSpan = EndTime.TimeOfDay;
            TimeSpan now = DateTime.Now.TimeOfDay;

            if (StartTime > EndTime)
            {
                // time rolls over to the next day, so we need to be after start or before end
                if (now >= startSpan || now <= endSpan)
                {
                    result = true;
                }
            }
            else
            {
                // time doesn't roll over to the next day, so we need to be between start and end
                if (now >= startSpan && now <= endSpan)
                {
                    result = true;
                }
            }

            // evaluate children only if our condition is true
            if (result)
            {
                // note: result of children don't matter at this level, so is ignored
                EvaluateChildren(actions);
            }
            return result;
        }

        public override string GetDescription()
        {
            if ((StartTime != DateTime.MinValue) && (EndTime != DateTime.MinValue))
            {   
                return $"Time is between {StartTime:hh:mm tt} and {EndTime:hh:mm tt}";
            }

            return "Undefined time range";
        }
    }
}
