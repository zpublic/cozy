// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DGroup.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the DGroup type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Containers
{
    using GUI4UFramework.Structural;

    /// <summary>
    /// A control that contains a group of child controls.
    /// All child controls should be clipped inside this control.
    /// </summary>
    public class DGroup : Control
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DGroup"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public DGroup(string name) : base(name)
        {
        }
    }
}
