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
            var sp = new CozySprite();
            if(!sp.Init())
            {
                return null;
            }
            return sp;
        }

        public static CozySprite Create(string path)
        {
            var sp = new CozySprite();
            if(!sp.InitWithFile(path))
            {
                return null;
            }
            return sp;
        }

        public static CozySprite Create(string path, Rectangle rect)
        {
            var sp = new CozySprite();
            if (!sp.InitWithRect(path, rect))
            {
                return null;
            }
            return sp;
        }

        public virtual bool Init()
        {
            return true;
        }

        public virtual bool InitWithFile(string path)
        {
            Texture = CozyDirector.Instance.TextureCacheInstance.AddImage(path);
            return true;
        }

        public virtual bool InitWithRect(string path, Rectangle rect)
        {
            Texture = CozyDirector.Instance.TextureCacheInstance.AddImage(path);
            Rect = rect;
            return true;
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
