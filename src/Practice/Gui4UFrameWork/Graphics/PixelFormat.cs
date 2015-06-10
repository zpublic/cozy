// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PixelFormat.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the PixelFormat type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UFramework.Graphics
{
    /// <summary>
    /// The description-format for the pixel.
    /// </summary>
    public enum PixelFormat
    {
        /// <summary>1 Byte RGB.</summary>
        RGB8,

        /// <summary>2 Byte RGB.</summary>
        RGB16,

        /// <summary>3 Byte RGB (1 Byte for R,G and B).</summary>
        RGB24,

        /// <summary>4 byte RGBA (1 byte for R,G,B and A).</summary>
        RGB32,
    }
}