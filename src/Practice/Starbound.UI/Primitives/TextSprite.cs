using Starbound.UI.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.UI.Primitives
{
    public enum HorizontalAlignment { Left, Center, Right };
    public enum VerticalAlignment { Top, Center, Bottom };

    public class TextSprite : Primitive
    {
        public readonly Vector2 Position;
        public readonly Vector2 Size;
        public readonly string Text;
        public readonly IFontResource Font;
        public readonly double Opacity;
        public readonly SBColor Color;

        /// <summary>
        /// Indicates whether the specified point indicates the left side of the text
        /// (HorizontalAlignment.Left) the center point of the text (HorizontalAlignment.Center)
        /// or the right edge of the text (HorizontalAlignment.Right).
        /// </summary>
        public readonly HorizontalAlignment HorizontalAlignment;

        /// <summary>
        /// Indicates whether the specified point indicates the top edge of the text
        /// (VerticalAlignment.Top) the center point of the text (VerticalAlignment.Center)
        /// or the bottom edge of the text (VerticalAlignment.Bottom).
        /// </summary>
        public readonly VerticalAlignment VerticalAlignment;

        public TextSprite(string text, IFontResource font, Vector2 position, Vector2 size, 
            HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment, 
            SBColor color,
            double opacity = 1.0)
        {
            Text = text;
            Font = font;
            Position = position;
            Size = size;
            HorizontalAlignment = horizontalAlignment;
            VerticalAlignment = verticalAlignment;
            Color = color;
            Opacity = opacity;
        }
    }
}
