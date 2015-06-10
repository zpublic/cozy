// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CollapseControl.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the CollapseControl type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Containers
{
    using GUI4UFramework.Structural;

    /// <summary>
    /// A control that will collapse its children or show them all.
    /// </summary>
    public class CollapseControl : Control
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CollapseControl"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public CollapseControl(string name) : base(name)
        {
        }
    }
}
