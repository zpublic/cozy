using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyKxlol.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CozyKxlol.Kxlol.Object
{
    class CozyCircle : CozyNode
    {
        #region Property

        public Vector2 Position { get; set; }
        public float Radius { get; set; }
        public Color ColorProperty { get; set; }

        #endregion

        public CozyCircle()
        {
            Position        = new Vector2(0.0f, 0.0f);
            Radius          = 0.0f;
            ColorProperty   = Color.Black;
        }

        public CozyCircle(Vector2 pos, float radius, Color color)
        {
            Position        = pos;
            Radius          = radius;
            ColorProperty   = color;
        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawCircle(Position, Radius, 20, ColorProperty, 20.0f);
        }
    }
}
