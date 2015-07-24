// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TreeViewNodeFlat.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the TreeViewNodeFlat type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Tree
{
    /// <summary>
    /// Contains information for list that contains a flattened tree-view-node structure.
    /// </summary>
    public class TreeViewNodeFlat
    {
        /// <summary>
        /// Gets or sets the node.
        /// </summary>
        /// <value>
        /// The node.
        /// </value>
        public TreeViewNode Node { get; set; }

        /// <summary>
        /// Gets or sets the original depth of the tree-node.
        /// </summary>
        /// <value>
        /// The depth.
        /// </value>
        public int Depth { get; set; }
    }
}