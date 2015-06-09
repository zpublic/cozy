using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CozyKxlol.Engine.Tiled
{
    public class CozyDefaultTiled : CozyTiledNode
    {
        public override uint Id
        {
            get { return 0; }
        }

        public override void DrawAt(GameTime gameTime, SpriteBatch spriteBatch, Vector2 pos, Vector2 size)
        {

        }
    }
}
