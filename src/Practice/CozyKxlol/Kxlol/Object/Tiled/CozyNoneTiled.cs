using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyKxlol.Engine.Tiled;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CozyKxlol.Kxlol.Object.Tiled
{
    public class CozyNoneTiled :CozyTiledNode
    {
        public override uint Id
        {
            get { return CozyTiledId.NoneTiled; }
        }
        
        public override void DrawAt(GameTime gameTime, SpriteBatch spriteBatch, Vector2 pos)
        {

        }
    }
}
