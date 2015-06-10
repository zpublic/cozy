// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotDrawnNode.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the NotDrawnNode type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UFramework.Structural
{
    /// <summary>
    /// This is a node that is not drawn / does not have a extra function
    /// Its just a example class.
    /// </summary>
    public class NotDrawnNode : Node
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotDrawnNode"/> class.
        /// </summary>
        /// <param name="name">The name for this instance.</param>
        public NotDrawnNode(string name)
        {
            this.Name = name;
        }
    }
}
