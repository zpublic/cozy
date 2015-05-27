using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CozyKxlol.Engine
{
    public class CozyLabel : CozyNode
    {
        public string Text { get; set; }
        public Color FontColor { get; set; }

        public CozyLabel()
        {
            FontColor = Color.WhiteSmoke;
        }

        public CozyLabel(string str, Color color)
        {
            Text        = str;
            FontColor   = color;
        }

        protected override void DrawSelf(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Text != null)
            {
                var FontOrigin = CozyGame.nolmalFont.MeasureString(Text) / 2;
                spriteBatch.DrawString(CozyGame.nolmalFont, Text, GlobalPosition, FontColor, 0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
            }
        }
    }
}
