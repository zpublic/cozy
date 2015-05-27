using CozyKxlol.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.Kxlol.Scene
{
    class WelcomeSceneLayer : CozyLayer
    {
        public override void Update(GameTime gameTime)
        {
        }

        protected override void DrawSelf(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(CozyGame.nolmalFont, "hehe", new Vector2(100, 100), Color.Black);
        }
    }
}
