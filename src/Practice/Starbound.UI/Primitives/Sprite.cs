using Starbound.UI.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starbound.UI.Primitives
{
    /// <summary>
    /// Represents a single sprite to be drawn on the screen.
    /// </summary>
    public class Sprite : Primitive
    {
        /// <summary>
        /// The image that should be drawn.
        /// </summary>
        public readonly IImageResource Resource;

        /// <summary>
        /// The location that the sprite should be drawn at.
        /// </summary>
        public readonly Vector2 Position;

        /// <summary>
        /// The size that the sprite should have (X is the width, Y is the height).
        /// </summary>
        public readonly Vector2 Size;

        /// <summary>
        /// The opacity that the sprite should have when drawn.
        /// </summary>
        public readonly double Opacity;

        public readonly SBColor TintColor;

        public Sprite(IImageResource resource, Vector2 position, Vector2 size, SBColor tintColor, double opacity = 1.0)
        {
            Resource = resource;
            Position = position;
            Size = size;
            TintColor = tintColor;
            Opacity = opacity;
        }
    }
}
