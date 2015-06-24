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
            var sp = new CozyTileSprite();
            if (!sp.Init())
            {
                return null;
            }
            return sp;
        }

        public static new CozyTileSprite Create(string path)
        {
            var sp = new CozyTileSprite();
            if (!sp.InitWithFile(path))
            {
                return null;
            }
            return sp;
        }

        public static new CozyTileSprite Create(string path, Rectangle rect)
        {
            var sp = new CozyTileSprite();
            if (!sp.InitWithRect(path, rect))
            {
                return null;
            }
            return sp;
        }

        public override bool Init()
        {
            if(!base.Init())
            {
                return false;
            }
            return true;
        }

        public override bool InitWithFile(string path)
        {
            if(!base.InitWithFile(path))
            {
                return false;
            }
            return true;
        }

        public override bool InitWithRect(string path, Rectangle rect)
        {
            if(!base.InitWithRect(path, rect))
            {
                return false;
            }
            return true;
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
