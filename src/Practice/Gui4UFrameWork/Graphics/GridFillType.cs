// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GridFillType.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the GridFillType type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UFramework.Graphics
{
    /// <summary>
    /// How the grid fill's it cells. With what kind of control.
    /// </summary>
    public enum GridFillType
    {
        /// <summary>
        /// No control in the cell.
        /// </summary>
        None,

        /// <summary>A text-control.</summary>
        Text,

        /// <summary>A button-control.</summary>
        Button
    }
}