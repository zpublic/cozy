using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyKxlol.Engine;
using CozyKxlol.Kxlol.Interface;
using Microsoft.Xna.Framework;

namespace CozyKxlol.Kxlol.Object
{
    public class CozyTileSprite : CozySprite , ITileMoveable
    {
        private Point _TilePosition;
        public Point TilePosition
        {
            get
            {
                return _TilePosition;
            }
            set
            {
                _TilePosition = value;
                Position = new Vector2(value.X * TiledContentSize.X, value.Y * ContentSize.Y);
            }
        }

        public Vector2 TiledContentSize { get; set; }

        #region Create

        public static new CozyTileSprite Create()
        {
            return new CozyTileSprite();
        }

        public static new CozyTileSprite Create(string path)
        {
            var sp = new CozyTileSprite();
            sp.Texture = CozyDirector.Instance.TextureCacheInstance.AddImage(path);
            return sp;
        }

        public static new CozyTileSprite Create(string path, Rectangle rect)
        {
            var sp = new CozyTileSprite();
            sp.Texture = CozyDirector.Instance.TextureCacheInstance.AddImage(path);
            sp.Rect = rect;
            return sp;
        }
        #endregion

        public void Move(MoveDirection dire)
        {

        }
    }
}
