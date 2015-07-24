// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DrawStyle.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the DrawStyle type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UFramework.Graphics
{
    /// <summary>
    /// With what style the object is being drawn.
    /// </summary>
    public enum DrawStyle
    {
        /// <summary>Show the hue of the color.</summary>
        Hue,

        /// <summary>Show the saturation of the color.</summary>
        Saturation,

        /// <summary>Show the brightness of the color.</summary>
        Brightness,

        /// <summary>Show the red component of the color.</summary>
        Red,

        /// <summary>Show the green component of the color.</summary>
        Green,

        /// <summary>Show the blue component of the color.</summary>
        Blue
    }
}
