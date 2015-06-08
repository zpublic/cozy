using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyKxlol.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CozyKxlol.Engine.Tiled
{
    public abstract class CozyTiledNode : CozyNode
    {
        public abstract uint Id { get; }
        public Point TiledPosition { get; set; }

        public CozyTiledNode()
        {
            AnchorPoint = Vector2.One * 0.5f;
        }

        public abstract void DrawAt(GameTime gameTime, SpriteBatch spriteBatch, 
            Vector2 pos, Vector2 size);
    }
}
