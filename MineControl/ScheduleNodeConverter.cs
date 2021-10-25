using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MineControl
{
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
