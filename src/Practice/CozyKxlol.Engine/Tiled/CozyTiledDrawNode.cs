using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CozyKxlol.Engine.Tiled
{
    public class CozyTiledDrawNode : CozyTiledNode, ITiledNodeBase
    {
        public uint Id { get { return CozyTiledId.DrawNode; } }

        protected override void DrawSelf(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawRectangle(
                new Rectangle(Position.ToPoint(),ContentSize.ToPoint()),
                Color.White);
        }
    }
}
