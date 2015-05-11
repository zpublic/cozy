using Starbound.UI.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.UI.Primitives
{
    /// <summary>
    /// A SpriteBorder is like a Sprite, only the edges (determined by the Border property) don't
    /// scale to fill the whole area of the sprite, only the center. As such, this makes it easy
    /// to draw things of various sizes that still look good.
    /// </summary>
    public class BorderSprite : Sprite
    {
        public readonly Thickness Border;

        public BorderSprite(IImageResource resource, Vector2 position, Vector2 size, Thickness border, SBColor tintColor, double opacity = 1.0)
            : base(resource, position, size, tintColor, opacity)
        {
            Border = border;
        }
    }
}
