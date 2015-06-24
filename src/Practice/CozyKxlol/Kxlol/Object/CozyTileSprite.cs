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
                Position = new Vector2(value.X * ContentSize.X, value.Y * ContentSize.Y);
            }
        }


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
            switch(dire)
            {
                case MoveDirection.Left:
                    TilePosition = new Point(TilePosition.X - 1, TilePosition.Y);
                    break;
                case MoveDirection.Right:
                    TilePosition = new Point(TilePosition.X + 1, TilePosition.Y);
                    break;
                case MoveDirection.Up:
                    TilePosition = new Point(TilePosition.X, TilePosition.Y - 1);
                    break;
                case MoveDirection.Down:
                    TilePosition = new Point(TilePosition.X, TilePosition.Y + 1);
                    break;
                case MoveDirection.LeftUp:
                    TilePosition = new Point(TilePosition.X - 1, TilePosition.Y - 1);
                    break;
                case MoveDirection.LeftDown:
                    TilePosition = new Point(TilePosition.X - 1, TilePosition.Y + 1);
                    break;
                case MoveDirection.RightUp:
                    TilePosition = new Point(TilePosition.X + 1, TilePosition.Y - 1);
                    break;
                case MoveDirection.RightDown:
                    TilePosition = new Point(TilePosition.X + 1, TilePosition.Y + 1);
                    break;
                default:
                    break;
            }
        }
    }
}
