using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MineControl.Lib.Schedule
{
    /// <summary>
    /// Top level of abstraction for schedule nodes
    /// </summary>
    public abstract class ScheduleNode
    {
        public Guid Id { get; set; }

        protected ScheduleNode()
        {
            GenerateId();
        }

        [JsonConstructor]
        protected ScheduleNode(Guid id)
        {
            Id = id;
            if (Id == Guid.Empty)
            {
                GenerateId();
            }
        }

        protected void GenerateId()
        {
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// Regenerates ID for this node
        /// </summary>
        public virtual void RegenerateIds()
        {
            GenerateId();
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
}
