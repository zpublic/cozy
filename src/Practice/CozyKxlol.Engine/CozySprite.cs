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
        private CozyTexture _Texture;
        public CozySprite()
        {
            
        }

        public CozySprite(string path)
        {
            _Texture = CozyDirector.Instance.TextureCacheInstance.AddImage(path);
        }

        protected override void DrawSelf(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if(_Texture != null)
            {
                spriteBatch.Draw(_Texture.Get(), GlobalPosition + Transform);
            }
        }
    }
}
