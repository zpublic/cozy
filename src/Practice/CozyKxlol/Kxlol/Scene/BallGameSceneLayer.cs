using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyKxlol.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CozyKxlol.Kxlol.Scene
{
    class BallGameSceneLayer : CozyLayer
    {
        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawCircle(new Vector2(300.0f, 300.0f), 20.0f, 20, Color.Red, 20.0f);
        }
    }
}
