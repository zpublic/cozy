// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RootNode.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the RootNode type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UFramework.Structural
{
    using System;

    /// <summary>
    /// Its the beginning of the tree.
    /// </summary>
    public class RootNode : NotDrawnNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RootNode"/> class.
        /// </summary>
        /// <param name="name">The name for this instance.</param>
        public RootNode(string name) : base(name)
        {
            this.CreationDate = DateTime.Now;
        }

        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        /// <value>
        /// The creation date.
        /// </value>
        public DateTime CreationDate { get; set; }
    }
}
