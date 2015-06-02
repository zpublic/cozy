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
        public string _Text;
        public string Text 
        { 
            get
            {
                return _Text;
            }
            set
            {
                _Text = value;
                ContentSize = CozyGame.nolmalFont.MeasureString(Text);
            }
        }
        public Color FontColor { get; set; }

        public CozyLabel()
        {
            FontColor = Color.WhiteSmoke;
        }

        public CozyLabel(string str, Color color)
        {
            Text        = str;
            FontColor   = color;
            AnchorPoint = Vector2.One / 2;
        }

        protected override void DrawSelf(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Text != null)
            {
                var FontOrigin = CozyGame.nolmalFont.MeasureString(Text) / 2;
                spriteBatch.DrawString(CozyGame.nolmalFont, Text, GlobalPosition + Transform, FontColor);
            }
        }
    }
}
