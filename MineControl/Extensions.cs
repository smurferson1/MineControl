using System.Runtime.InteropServices;


namespace System.Windows.Forms
{
    public static class Extensions
    {        
        public static void MoveUp(this TreeNode node)
        {
            TreeNodeCollection parentNodes = node.Parent == null ? node.TreeView.Nodes : node.Parent.Nodes;
            int nodeIndex = node.Index;

            if (nodeIndex > 0)
            {
                parentNodes.Remove(node);
                parentNodes.Insert(nodeIndex - 1, node);
            }
        }

        public static void MoveDown(this TreeNode node)
        {
            TreeNodeCollection parentNodes = node.Parent == null ? node.TreeView.Nodes : node.Parent.Nodes;
            int nodeIndex = node.Index;

            if (nodeIndex < parentNodes.Count - 1)
            {
                parentNodes.Remove(node);
                parentNodes.Insert(nodeIndex + 1, node);
            }
        }
        
    }
}