using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MineControl
{
    public class WeekNode : BranchingNode
    {
        public List<DayOfWeek> SelectedDays { get; set; } = new List<DayOfWeek>();

        public WeekNode() : base() { }

        public WeekNode(Guid id, bool isSunday, bool isMonday, bool isTuesday, bool isWednesday, bool isThursday, bool isFriday, bool isSaturday): base(id)
        {
            UpdateSelectedDays(isSunday, isMonday, isTuesday, isWednesday, isThursday, isFriday, isSaturday);
        }

        [JsonConstructor]
        public WeekNode(Guid id, List<DayOfWeek> selectedDays): base(id)
        {
            SelectedDays = selectedDays;
        }

        public void UpdateSelectedDays(bool isSunday, bool isMonday, bool isTuesday, bool isWednesday, bool isThursday, bool isFriday, bool isSaturday)
        {
            SelectedDays.Clear();
            if (isSunday)
            {
                SelectedDays.Add(DayOfWeek.Sunday);
            }
            if (isMonday)
            {
                SelectedDays.Add(DayOfWeek.Monday);
            }
            if (isTuesday)
            {
                SelectedDays.Add(DayOfWeek.Tuesday);
            }
            if (isWednesday)
            {
                SelectedDays.Add(DayOfWeek.Wednesday);
            }
            if (isThursday)
            {
                SelectedDays.Add(DayOfWeek.Thursday);
            }
            if (isFriday)
            {
                SelectedDays.Add(DayOfWeek.Friday);
            }
            if (isSaturday)
            {
                SelectedDays.Add(DayOfWeek.Saturday);
            }
        }

        public override bool Evaluate(List<ScheduleAction> actions)
        {
            DayOfWeek currentDay = DateTime.Today.DayOfWeek;
            bool result;

            if ((SelectedDays.Count > 0) && SelectedDays.Contains(currentDay))
            {
                result = true;
            }
            else
            {
                result = false;
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
            if (SelectedDays.Count > 0)
            {
                StringBuilder result = new StringBuilder("Day is ");
                bool firstDay = true;

                foreach (DayOfWeek day in SelectedDays)
                {
                    result.Append(firstDay ? "" : " or ");
                    result.Append(Enum.GetName(day.GetType(), day));
                    firstDay = false;
                }

                return result.ToString();
            }

            return "Undefined day of the week";
        }
    }
}
