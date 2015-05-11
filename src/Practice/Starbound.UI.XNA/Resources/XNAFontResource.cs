using Microsoft.Xna.Framework.Graphics;
using Starbound.UI.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.UI.XNA.Resources
{
    public class XNAFontResource : IFontResource
    {
        public readonly SpriteFont Font;

        public XNAFontResource(SpriteFont font)
        {
            Font = font;
        }

        public Vector2 Measure(string text)
        {
            Microsoft.Xna.Framework.Vector2 xnaSize = Font.MeasureString(text);
            return new Vector2(xnaSize.X, xnaSize.Y);
        }
    }
}
