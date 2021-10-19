using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MineControl
{
    // TODO: clean up Guid.Empty using nullable types
    // TODO: clean up constructors to minimize waste from earlier Json code

    /// <summary>
    /// Top level of abstraction for schedule nodes
    /// </summary>
    public abstract class ScheduleNode
    {        
        public Guid Id { get; set; }

        protected ScheduleNode() 
        {
            Id = Guid.NewGuid();
        }

        [JsonConstructor]
        protected ScheduleNode(Guid id)
        {            
            Id = id;
            if (Id == Guid.Empty)
            {
                Id = Guid.NewGuid();
            }
        }

        /// <summary>
        /// Returns the list of nodes associated with supplied GUID, if present in this node or children.        
        /// </summary>
        /// <param name="id">GUID to find node list for</param>
        /// <returns>Child node list for the node with supplied guid, if present. Null otherwise.</returns>
        public virtual List<ScheduleNode> GetNodesById(Guid id)
        {
            // only branching nodes will care about this, so it will always be null here
            return null;
        }

        /// <summary>
        /// Returns the node supplied GUID, if present in this node or children.        
        /// </summary>
        /// <param name="id">GUID of the node</param>
        /// <returns>Node with supplied guid, if present. Null otherwise.</returns>
        public virtual ScheduleNode GetNodeById(Guid? id)
        {
            if (id != null && Id.Equals(id))
            {
                return this;
            }

            return null;
        }

        /// <summary>
        /// Returns parent node of the node with the given ID, or null if the parent is not present/not determined
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual ScheduleNode GetNodeParent(Guid? id)
        {
            return null;
        }

        /// <summary>
        /// Evaluates the node, appending any valid actions to the list
        /// and returning true if node evaluation is complete (i.e. further evaluation unnecessary)
        /// </summary>
        /// <param name="actions">Accumulates evaluated actions, first evaluated action at the top</param>
        /// <returns>True if node evaluation is complete (i.e. further evaluation unnecessary), false otherwise</returns>
        public abstract bool Evaluate(List<ScheduleAction> actions);

        /// <summary>
        /// Gets text description of the node and its properties
        /// </summary>        
        public virtual string GetDescription()
        {
            return "Unknown (ScheduleNode)";
        }
    }

    /// <summary>
    /// A schedule node with branching capability (i.e. 0 or more children)
    /// </summary>
    public abstract class BranchingNode: ScheduleNode
    {       
        // ignore children, because there is custom JSON conversion to avoid circular loops (see ScheduleNodeConverter)
        [JsonIgnore]
        public List<ScheduleNode> Children { get; set; } = new List<ScheduleNode>();

        protected BranchingNode(): base() { }

        [JsonConstructor]
        protected BranchingNode(Guid id) : base(id) { }

        public override List<ScheduleNode> GetNodesById(Guid id)
        {
            if (Id.Equals(id))
            {
                return Children;
            }
            else
            {
                List<ScheduleNode> childNodes;
                foreach (ScheduleNode child in Children)
                {
                    childNodes = child.GetNodesById(id);
                    if (childNodes != null)
                    {
                        return childNodes;
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// Returns the node supplied GUID, if present in this node or children.        
        /// </summary>
        /// <param name="id">GUID of the node</param>
        /// <returns>Node with supplied guid, if present. Null otherwise.</returns>
        public override ScheduleNode GetNodeById(Guid? id)
        {
            ScheduleNode nodeAnswer;
            foreach (ScheduleNode child in Children)
            {
                nodeAnswer = child.GetNodeById(id);
                if (nodeAnswer != null)
                {
                    return nodeAnswer;
                }
            }            

            return base.GetNodeById(id);
        }
        
        public override ScheduleNode GetNodeParent(Guid? id)
        {
            if (this.Children.Count == 0)
            {
                return null;
            }
            else
            {
                foreach (ScheduleNode node in Children)
                {
                    if (id != null && node.Id.Equals(id))
                    {
                        return this;
                    }
                    else
                    {
                        ScheduleNode childResult = node.GetNodeParent(id);
                        if (childResult != null)
                        {
                            return childResult;
                        }
                    }
                }
                return null;
            }
        }

        protected void EvaluateChildren(List<ScheduleAction> actions)
        {
            bool result;

            foreach (ScheduleNode childNode in Children)
            {
                result = childNode.Evaluate(actions);
                if (result)
                {
                    return;
                }
            }            
        }

        public override string GetDescription()
        {
            return "Unknown (BranchingScheduleNode)";
        }
    }
        

    public class CalendarNode : BranchingNode
    {
        public int StartDay { get; set; } = 0;
        public int EndDay { get; set; } = 0;
        public List<int> ValidMonths { get; set; } = new List<int>(); // lists first month first and last month last (i.e. December-February would be [12, 2])        

        public const int cLastDay = 32;

        public CalendarNode() : base() { }

        public CalendarNode(Guid id, int startMonth, int endMonth, int startDay, int endDay): base(id)
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
                        if ((i > 12))
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

            if ((ValidMonths.Count > 0) && ValidMonths.Contains(today.Month))
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
                DateTime temp = new DateTime(DateTime.Now.Year, this.ValidMonths[0], 1);
                string startMonth = temp.ToString("MMMM");
                temp = new DateTime(DateTime.Now.Year, this.ValidMonths.Last(), 1);
                string endMonth = temp.ToString("MMMM");
                string startDay = StartDay == 32 ? "[Last Day]" : StartDay.ToString();
                string endDay = EndDay == 32 ? "[Last Day]" : EndDay.ToString();
                return $"Day is between {startMonth} {startDay} and {endMonth} {endDay}";
            }

            return "Day is in an undefined month/day range";
        }
    }

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

    public class TimeNode : BranchingNode
    {
        public DateTime StartTime { get; set; } = DateTime.MinValue;
        public DateTime EndTime { get; set; } = DateTime.MinValue;

        public TimeNode() : base() { }

        [JsonConstructor]
        public TimeNode(Guid id, DateTime startTime, DateTime endTime): base(id)
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
                return $"Time is between {StartTime.ToString("hh:mm tt")} and {EndTime.ToString("hh:mm tt")}";
            }

            return "Undefined time range";
        }
    }
    
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

    public class ActionNode: ScheduleNode
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

    /// <summary>
    /// Json converter for serializing and deserializing ScheduleNodes polymorphically    
    /// </summary>
    public class ScheduleNodeConverter : JsonConverter<ScheduleNode>
    {
        private enum TypeDiscriminator
        {
            ScheduleNode = 0,
            BranchingNode = 1,
            CalendarNode = 2,
            WeekNode = 3,
            TimeNode = 4,            
            ElseNode = 5,
            ActionNode = 6
        }

        public override bool CanConvert(Type typeToConvert)
        {
            return typeof(ScheduleNode).IsAssignableFrom(typeToConvert);
        }

        public override ScheduleNode Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            if (!reader.Read()
                    || reader.TokenType != JsonTokenType.PropertyName
                    || reader.GetString() != "TypeDiscriminator")
            {
                throw new JsonException();
            }

            if (!reader.Read() || reader.TokenType != JsonTokenType.Number)
            {
                throw new JsonException();
            }

            ScheduleNode scheduleNode;
            TypeDiscriminator typeDiscriminator = (TypeDiscriminator)reader.GetInt32();
            switch (typeDiscriminator)
            {
                case TypeDiscriminator.CalendarNode:
                    if (!reader.Read() || reader.GetString() != "TypeValue")
                    {
                        throw new JsonException();
                    }
                    if (!reader.Read() || reader.TokenType != JsonTokenType.StartObject)
                    {
                        throw new JsonException();
                    }
                    scheduleNode = (CalendarNode)JsonSerializer.Deserialize(ref reader, typeof(CalendarNode));
                    break;
                case TypeDiscriminator.WeekNode:
                    if (!reader.Read() || reader.GetString() != "TypeValue")
                    {
                        throw new JsonException();
                    }
                    if (!reader.Read() || reader.TokenType != JsonTokenType.StartObject)
                    {
                        throw new JsonException();
                    }
                    scheduleNode = (WeekNode)JsonSerializer.Deserialize(ref reader, typeof(WeekNode));
                    break;
                case TypeDiscriminator.TimeNode:
                    if (!reader.Read() || reader.GetString() != "TypeValue")
                    {
                        throw new JsonException();
                    }
                    if (!reader.Read() || reader.TokenType != JsonTokenType.StartObject)
                    {
                        throw new JsonException();
                    }
                    scheduleNode = (TimeNode)JsonSerializer.Deserialize(ref reader, typeof(TimeNode));
                    break;
                case TypeDiscriminator.ActionNode:
                    if (!reader.Read() || reader.GetString() != "TypeValue")
                    {
                        throw new JsonException();
                    }
                    if (!reader.Read() || reader.TokenType != JsonTokenType.StartObject)
                    {
                        throw new JsonException();
                    }
                    scheduleNode = (ActionNode)JsonSerializer.Deserialize(ref reader, typeof(ActionNode));
                    break;
                case TypeDiscriminator.ElseNode:
                    if (!reader.Read() || reader.GetString() != "TypeValue")
                    {
                        throw new JsonException();
                    }
                    if (!reader.Read() || reader.TokenType != JsonTokenType.StartObject)
                    {
                        throw new JsonException();
                    }
                    scheduleNode = (ElseNode)JsonSerializer.Deserialize(ref reader, typeof(ElseNode));
                    break;
                default:
                    throw new NotSupportedException();
            }

            // read children separately, since we need to pass options here (can't pass options above due to circular read loop)
            if (scheduleNode is BranchingNode branchingNode)
            {
                if (!reader.Read())
                {                    
                    throw new JsonException();
                }
                branchingNode.Children = (List<ScheduleNode>)JsonSerializer.Deserialize(ref reader, typeof(List<ScheduleNode>), options);
            }

            if (!reader.Read() || reader.TokenType != JsonTokenType.EndObject)
            {
                throw new JsonException();
            }

            return scheduleNode;
        }

        public override void Write(
            Utf8JsonWriter writer,
            ScheduleNode value,
            JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            if (value is CalendarNode calendarNode)
            {
                writer.WriteNumber("TypeDiscriminator", (int)TypeDiscriminator.CalendarNode);
                writer.WritePropertyName("TypeValue");
                JsonSerializer.Serialize(writer, calendarNode);
            }
            else if (value is WeekNode weekNode)
            {
                writer.WriteNumber("TypeDiscriminator", (int)TypeDiscriminator.WeekNode);
                writer.WritePropertyName("TypeValue");
                JsonSerializer.Serialize(writer, weekNode);
            }
            else if (value is TimeNode timeNode)
            {
                writer.WriteNumber("TypeDiscriminator", (int)TypeDiscriminator.TimeNode);
                writer.WritePropertyName("TypeValue");
                JsonSerializer.Serialize(writer, timeNode);
            }
            else if (value is ElseNode elseNode)
            {
                writer.WriteNumber("TypeDiscriminator", (int)TypeDiscriminator.ElseNode);
                writer.WritePropertyName("TypeValue");                    
                JsonSerializer.Serialize(writer, elseNode);
            }            
            else if (value is ActionNode actionNode)
            {
                writer.WriteNumber("TypeDiscriminator", (int)TypeDiscriminator.ActionNode);
                writer.WritePropertyName("TypeValue");
                JsonSerializer.Serialize(writer, actionNode);
            }
            else
            {
                throw new NotSupportedException();
            }

            // write children separately, since we need to pass options here (can't pass options above due to circular write loop)
            if (value is BranchingNode branchingNode)
            {
                writer.WriteStartArray("Children");
                foreach (ScheduleNode node in branchingNode.Children)
                {
                    JsonSerializer.Serialize(writer, node, options);
                }
                writer.WriteEndArray();
            }

            writer.WriteEndObject();
        }
    }
}
