using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace MineControl.Lib.Schedule
{
    public class CalendarNode : BranchingNode
    {
        public int StartDay { get; set; } = 0;
        public int EndDay { get; set; } = 0;
        public List<int> ValidMonths { get; set; } = new List<int>(); // lists first month first and last month last (i.e. December-February would be [12, 2])        

        public const int cLastDay = 32;

        public CalendarNode() : base() { }

        public CalendarNode(Guid id, int startMonth, int endMonth, int startDay, int endDay) : base(id)
        {
            StartDay = startDay;
            EndDay = endDay;

            UpdateValidMonths(startMonth, endMonth);
        }

        [JsonConstructor]
        public CalendarNode(Guid id, List<int> validMonths, int startDay, int endDay) : base(id) =>
            (ValidMonths, StartDay, EndDay) = (validMonths, startDay, endDay);

        public void UpdateValidMonths(int startMonth, int endMonth)
        {
            ValidMonths.Clear();

            if ((startMonth >= 1) && (startMonth <= 12) && (endMonth >= 1) && (endMonth <= 12))
            {
                if ((startMonth == endMonth) && (StartDay <= EndDay))
                {
                    // just a single month
                    ValidMonths.Add(startMonth);
                }
                else
                {
                    int i = startMonth;
                    do
                    {
                        ValidMonths.Add(i);
                        i++;
                        if (i > 12)
                        {
                            i = 1;
                        }
                    }
                    while (i != endMonth);

                    // add the last month
                    ValidMonths.Add(i);
                }
            }
        }

        public override bool Evaluate(List<ScheduleAction> actions)
        {
            DateTime today = DateTime.Today;
            bool result = true;

            if (ValidMonths.Count > 0 && ValidMonths.Contains(today.Month))
            {
                if (today.Month == ValidMonths[0] && today.Day < StartDay)
                {
                    result = false;
                }
                if (today.Month == ValidMonths[ValidMonths.Count] && today.Day > EndDay)
                {
                    result = false;
                }
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
            if ((StartDay > 0) && (EndDay > 0) && (ValidMonths.Count > 0))
            {
                DateTime temp = new(DateTime.Now.Year, ValidMonths[0], 1);
                string startMonth = temp.ToString("MMMM");
                temp = new(DateTime.Now.Year, ValidMonths.Last(), 1);
                string endMonth = temp.ToString("MMMM");
                string startDay = StartDay == 32 ? "[Last Day]" : StartDay.ToString();
                string endDay = EndDay == 32 ? "[Last Day]" : EndDay.ToString();
                return $"Day is between {startMonth} {startDay} and {endMonth} {endDay}";
            }

            return "Day is in an undefined month/day range";
        }
    }
}
