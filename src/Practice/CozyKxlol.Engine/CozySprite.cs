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
        #region Property

        private CozyTexture _Texture;
        public CozyTexture Texture 
        { 
            get
            {
                return _Texture;
            }
            set
            {
                _Texture = value;
                if(!_Rect.HasValue)
                {
                    ContentSize = Texture.ContentSize;
                }
            }
        }
        private Rectangle? _Rect;
        public Rectangle? Rect 
        { 
            get
            {
                return _Rect;
            }
            set
            {
                _Rect = value;
                if(_Texture != null)
                {
                    ContentSize = new Vector2(Rect.Value.Width, Rect.Value.Height);
                }
            }
        }

        #endregion

        public CozySprite()
        {
            AnchorPoint = Vector2.One / 2;
        }

        #region Create

        public static CozySprite Create()
        {
            return new CozySprite();
        }

        public static CozySprite Create(string path)
        {
            var sp      = new CozySprite();
            sp.Texture  = CozyDirector.Instance.TextureCacheInstance.AddImage(path);
            return sp;
        }

        public static CozySprite Create(string path, Rectangle rect)
        {
            var sp              = new CozySprite();
            sp.Texture          = CozyDirector.Instance.TextureCacheInstance.AddImage(path);
            sp.Rect             = rect;
            return sp;
        }

        #endregion

        protected override void DrawSelf(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Texture != null)
            {
                spriteBatch.Draw(Texture.Get(), GlobalPosition + Transform, null, Rect);
            }
        }
    }
}
