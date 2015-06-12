using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CozyKxlol.Engine.Tiled
{
    public class CozySpriteTiled : CozyTiledNode
    {
        public uint SpriteId { get; set; }
        public override uint Id 
        { 
            get
            {
                return SpriteId;
            }
        }
        public CozyTexture Texture { get; set; }
        public Rectangle Rect { get; set; }

        public static CozySpriteTiled Create(CozyTexture texture, Rectangle rect)
        {
            var tiled = new CozySpriteTiled();
            tiled.Texture = texture;
            tiled.Rect = rect;
            return tiled;
        }

        public override void DrawAt(GameTime gameTime, SpriteBatch spriteBatch,
            Vector2 pos, Vector2 size)
        {
            if (Texture != null)
            {
                spriteBatch.Draw(Texture.Get(), pos, null, Rect);
            }
        }
    }
}
