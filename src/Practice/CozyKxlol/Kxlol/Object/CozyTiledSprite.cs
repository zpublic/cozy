using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyKxlol.Engine;
using Microsoft.Xna.Framework;

namespace CozyKxlol.Kxlol.Object
{
    public class CozyTiledSprite : CozySprite
    {
        private Point _TiledSize;
        public Point TiledSize
        {
            get
            {
                return _TiledSize;
            }
            set
            {
                _TiledSize = value;
                Position = new Vector2(value.X * TiledContentSize.X, value.Y * ContentSize.Y);
            }
        }

        public Vector2 TiledContentSize { get; set; }

        public static new CozyTiledSprite Create()
        {
            return new CozyTiledSprite();
        }

        public static new CozyTiledSprite Create(string path)
        {
            var sp = new CozyTiledSprite();
            sp.Texture = CozyDirector.Instance.TextureCacheInstance.AddImage(path);
            return sp;
        }

        public static new CozyTiledSprite Create(string path, Rectangle rect)
        {
            var sp = new CozyTiledSprite();
            sp.Texture = CozyDirector.Instance.TextureCacheInstance.AddImage(path);
            sp.Rect = rect;
            return sp;
        }
    }
}
