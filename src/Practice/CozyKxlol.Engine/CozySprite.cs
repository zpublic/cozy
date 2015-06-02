using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace CozyKxlol.Engine
{
    public class CozySprite : CozyNode
    {
        public CozyTexture Texture { get; set; }
        public CozySprite()
        {
            
        }

        public CozySprite(string path)
        {
            Texture = CozyDirector.Instance.TextureCacheInstance.AddImage(path);
        }

        protected override void DrawSelf(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Texture != null)
            {
                spriteBatch.Draw(Texture.Get(), GlobalPosition + Transform);
            }
        }
    }
}
