using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.Engine
{
    public class CozyScene : CozyNode
    {
        // 先只支持一层
        public CozyLayer Layer
        {
            get;
            set;
        }

        public override void Update(GameTime gameTime)
        {
            if (Layer != null)
            {
                Layer.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Layer != null)
            {
                Layer.Draw(gameTime, spriteBatch);
            }
        }
    }
}
