using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CozyKxlol.Engine.Tiled
{
    public class CozyNoneTiled : CozyTiledNode, ITiledNodeBase
    {
        public uint Id { get { return CozyTiledId.NoneTiled; } }

        public override void DrawAt(GameTime gameTime, SpriteBatch spriteBatch, Vector2 pos)
        {
            
        }
    }
}
