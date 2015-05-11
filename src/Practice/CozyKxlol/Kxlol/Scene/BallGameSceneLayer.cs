using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyKxlol.Kxlol.Object;
using CozyKxlol.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CozyKxlol.Kxlol.Scene
{
    class BallGameSceneLayer : CozyLayer
    {
        List<CozyCircle> CircleList = new List<CozyCircle>();

        public BallGameSceneLayer()
        {
            var circle1 = new CozyCircle(new Vector2(300.0f, 300.0f), 20.0f, Color.Red);
            var circle2 = new CozyCircle(new Vector2(400.0f, 300.0f), 20.0f, Color.Blue);
            CircleList.Add(circle1);
            CircleList.Add(circle2);
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var obj in CircleList)
            {
                obj.Draw(gameTime, spriteBatch);
            }
        }
    }
}
