using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyKxlol.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CozyKxlol.Engine.Tiled
{
    public class CozyTiledNode : CozyNode
    {
        public Point TiledPosition { get; set; }
        public Color ColorProperty { get; set; }

        public CozyTiledNode()
        {
            AnchorPoint = Vector2.One / 2;
        }

        public virtual void DrawAt(GameTime gameTime, SpriteBatch spriteBatch, Vector2 pos)
        {
            spriteBatch.FillRectangle(
                new Rectangle(pos.ToPoint(), ContentSize.ToPoint()), 
                ColorProperty);
        }
    }
}
