using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Starbound.UI.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Vector2 = Microsoft.Xna.Framework.Vector2;
using Color = Microsoft.Xna.Framework.Color;

namespace Starbound.UI.XNA
{
    /// <summary>
    /// Extracts the SpriteBatch type into an interface so it can be mocked in testing.
    /// </summary>
    public interface ISpriteBatch
    {
        void Begin();
        void End();
        void Draw(IImageResource texture, Rectangle rectangle, Color color);
        void Draw(IImageResource texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color);
        void DrawString(IFontResource font, string text, Microsoft.Xna.Framework.Vector2 position, Color color);
    }
}
