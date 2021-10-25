using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MineControl
{
    /// <summary>
    /// A schedule node with branching capability (i.e. 0 or more children)
    /// </summary>
    public abstract class BranchingNode : ScheduleNode
    {
        // ignore children, because there is custom JSON conversion to avoid circular loops (see ScheduleNodeConverter)
        [JsonIgnore]
        public List<ScheduleNode> Children { get; set; } = new List<ScheduleNode>();

        protected BranchingNode() : base() { }

        [JsonConstructor]
        protected BranchingNode(Guid id) : base(id) { }

        public override void RegenerateIds()
        {
            foreach (ScheduleNode child in Children)
            {
                child.RegenerateIds();
            }
            base.RegenerateIds();
        }

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
}
