// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TreeViewRootNode.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   I am the RootNode.
//   I am the node that is used by DTreeViewCanvas.
//   This is because sometimes tree-views do some weird smart stuff , and you always end up that you need to have a RootNode-class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Tree
{
    /// <summary>
    /// I am the RootNode.
    /// I am the node that is used by DTreeViewCanvas.
    /// This is because sometimes tree-views do some weird smart stuff , and you always end up that you need to have a RootNode-class.
    /// </summary>
    public class TreeViewRootNode : TreeViewNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TreeViewRootNode"/> class.
        /// </summary>
        /// <param name="name">The name for this node , will be used also as text.</param>
        public TreeViewRootNode(string name) : base(name)
        {
        }
    }
}