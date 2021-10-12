using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MineControl
{
    public class Schedule
    {
        public List<ScheduleNode> Nodes { get; set; } = new List<ScheduleNode>();
        public string Name { get; set; } = string.Empty;
        public Guid Id { get; set; }

        /// <summary>
        /// Just holds the last evaluation result in case app needs it, not saved
        /// </summary>
        [JsonIgnore]
        public List<ScheduleAction> LastEvaluatedActions { get; private set; } = new List<ScheduleAction>();

        public Schedule(): this(Guid.Empty) { }

        [JsonConstructor]
        public Schedule(Guid id)
        {
            this.Id = id;
            if (Id == Guid.Empty)
            {
                Id = Guid.NewGuid();
            }
        }

        /// <summary>
        /// Load a Schedule object from serialized Json
        /// </summary>
        /// <param name="scheduleAsSerializedJson">Serialized Schedule</param>
        /// <returns>The loaded Schedule object</returns>
        public static Schedule Load(string scheduleAsSerializedJson)
        {
            return JsonSerializer.Deserialize<Schedule>(scheduleAsSerializedJson);
        }

        public bool ValidateNode(Guid parentGuid, ScheduleNode node, ScheduleNode nextNode)
        {
            return true;
        }

        /// <summary>
        /// Adds node to parent with parentId if present, before nextNode if present.
        /// </summary>
        /// <param name="parentId">ID of parent node. If null, adds to the top level.</param>
        /// <param name="node">Node being added</param>
        /// <param name="nextNode">New node is inserted before nextNode, or at the last valid location in the list if null</param>
        /// <returns></returns>
        public bool AddNode(Guid parentId, ScheduleNode node, ScheduleNode nextNode)
        {
            if (ValidateNode(parentId, node, nextNode))
            {
                List<ScheduleNode> nodes = GetNodesById(parentId);
                if ((nextNode == null) || (nodes.Find(x => x == nextNode) == null))
                {
                    nodes.Add(node);
                }
                else
                {
                    nodes.Insert(nodes.IndexOf(nodes.Find(x => x == nextNode)), node);
                }

                // now ensure the else node is at the end
                ElseNode elseNode = (ElseNode)nodes.Find(x => x is ElseNode);
                if ((elseNode != null) && (elseNode != nodes.Last()))
                {
                    nodes.Remove(elseNode);
                    nodes.Add(elseNode);
                }
                return true;
            }    
            return false;            
        }

        /// <summary>
        /// Returns the child nodes list of the node with the given ID, if it exists.
        /// </summary>       
        private List<ScheduleNode> GetNodesById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return Nodes;
            }
            else
            {
                foreach(ScheduleNode node in Nodes)
                {
                    List<ScheduleNode> nodes = node.GetNodesById(id);
                    if (nodes != null)
                    {
                        return nodes;
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// Returns the node with the given ID, if it exists.
        /// </summary>       
        public ScheduleNode GetNodeById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return null;
            }
            else
            {
                foreach (ScheduleNode node in Nodes)
                {
                    ScheduleNode nodeAnswer = node.GetNodeById(id);

                    if (nodeAnswer != null)
                    {
                        return nodeAnswer;
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// Returns string description of the node with the given guid, if present, otherwise empty string.
        /// Considers context (i.e. if/else/else if).
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetNodeDescription(Guid id)
        {
            ScheduleNode node = GetNodeById(id);
            if (node != null)
            {
                string result = node.GetDescription();                

                if ((node is BranchingNode) && !(node is ElseNode))
                {
                    List<ScheduleNode> parentNodes = GetNodeParentNodes(id);
                    if (parentNodes[0] == node)
                    {
                        result = $"If ({result})";
                    }
                    else
                    {
                        result = $"Else If ({result})";
                    }
                }

                return result;
            }

            return "";
        }
        
        /// <summary>
        /// Returns the parent of the node with the given ID if present, null if it's a top level node, and an exception if node with that ID doesn't exist.
        /// </summary>
        /// <param name="id">ID of node whose parent we're looking for</param>
        public ScheduleNode GetNodeParent(Guid id)
        {
            foreach (ScheduleNode node in Nodes)
            {
                if (node.Id.Equals(id))
                {
                    // top level node, no parent
                    return null;
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

            // if we looped through everything without returning, this should mean we couldn't find the node with that ID at all
            throw new Exception($"Node with ID {id} could not be found");
        }

        /// <summary>
        /// Returns the nodes list of the parent of the node with the given ID, and an exception if node with that ID doesn't exist.
        /// </summary>
        /// <param name="id">ID of node whose parent we're looking for</param>
        public List<ScheduleNode> GetNodeParentNodes(Guid id)
        {
            ScheduleNode parent = GetNodeParent(id);
            return parent == null ? Nodes : ((BranchingNode)parent).Children;            
        }

        /// <summary>
        /// Evaluates the schedule represented by the Nodes list for applicable actions
        /// Note: does NOT execute actions, only determines which actions apply
        /// </summary>
        /// <returns>All evaluated actions</returns>
        public List<ScheduleAction> Evaluate()
        {
            List<ScheduleAction> result = new List<ScheduleAction>();

            foreach (ScheduleNode node in Nodes)
            {
                if (node.Evaluate(result))
                {
                    LastEvaluatedActions = result;
                    return result;
                }
            }

            LastEvaluatedActions = result;
            return result;
        }
        
        /// <summary>
        /// Attempts to delete node with the given ID and returns true if successful.
        /// Will ensure correct schedule format after node deletion, i.e. no orphan ifs or elses.
        /// </summary>
        /// <param name="id">ID of node to delete</param>
        /// <returns></returns>
        public bool DeleteNode(Guid id)
        {
            ScheduleNode nodeToDelete = GetNodeById(id);

            if (nodeToDelete == null)
            {
                return false;
            }
            else
            {
                // find the collection nodeToDelete belongs to (sisterNodes)
                List<ScheduleNode> sisterNodes;
                BranchingNode nodeParent = (BranchingNode)GetNodeParent(id);
                if (nodeParent == null)
                {
                    sisterNodes = Nodes;
                }
                else
                {
                    sisterNodes = nodeParent.Children;
                }

                // delete all nodes needed to ensure no orphans remain
                if (nodeToDelete is ElseNode)
                {
                    // for elsenodes, we must delete everything in the collection (ifs cannot exist without an else)
                    sisterNodes.Clear();
                    return true;                    
                }
                else if (nodeToDelete is BranchingNode)
                {
                    // for other branching nodes, two should always be present, otherwise we have to delete them all
                    if (sisterNodes.Count(x => x is BranchingNode) > 2)
                    {
                        // two will remain, so we can just delete this node
                        return sisterNodes.Remove(nodeToDelete);                        
                    }
                    else
                    {
                        // only one would be left, we have to clear them all
                        sisterNodes.Clear();
                        return true;
                    }
                }
                else
                {
                    // for non-branching nodes, it's safe to just delete it
                    return sisterNodes.Remove(nodeToDelete);                    
                }
            }
        }

        public string Serialize(JsonSerializerOptions options)
        {
            return JsonSerializer.Serialize(this, options);
        }

        /// <summary>
        /// Generates all IDs in this schedule again
        /// </summary>
        public void RegenerateIds()
        {
            // TODO
            throw new NotImplementedException();
        }
    }
}
