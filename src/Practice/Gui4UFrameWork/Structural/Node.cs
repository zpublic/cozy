// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Node.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   The node contains all functionality for recursive actions (tree structure)
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UFramework.Structural
{
    using System.Collections.Generic;

    /// <summary>The node contains all functionality for recursive actions (tree structure).</summary>
    public class Node
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Node"/> class.
        /// </summary>
        protected Node()
        {
            this.Parent = null;
            this.Children = new NodeCollection();
        }

        /// <summary>Gets the children of this scene node.</summary>
        /// <value>The children.</value>
        public NodeCollection Children { get; private set; }

        /// <summary>Gets or sets  the parent control.</summary>
        /// <value>Gets or sets the parent control.</value>
        public Control Parent { get; set; }

        /// <summary>Gets the main identity.</summary>
        /// <value>Gets or sets the name for this control.</value>
        public string Name { get; internal set; }

        /// <summary>Gets or sets a user definable object.</summary>
        /// <value>a placeholder for extra info when you want.</value>
        public object Tag { get; set; }

        /// <summary>Flattens the whole tree structure underneath me into a simple list.</summary>
        /// <returns>A enumerable list of flattened nodes.</returns>
        public IEnumerable<Node> FlattenChildrenRecursive()
        {
            // create the list to populate
            var list = new NodeCollection();

            // go recursive ! 
            this.FlattenChildren(this, ref list);

            return list;
        }

        /// <summary>
        /// Get all the children recursive and flattens it into the given list.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="listToFill">The list to fill.</param>
        private void FlattenChildren(Node parent, ref NodeCollection listToFill)
        {
            listToFill.Add(parent);
            foreach (var child in parent.Children)
            {
                this.FlattenChildren(child, ref listToFill);
            }
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("{0}", GetType());
        }
    }
}