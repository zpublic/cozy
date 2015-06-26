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

        public bool Moving { get; set; }

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

        public CozyTileSprite()
        {
            AnchorPoint = Vector2.Zero;
        }

        public void Move(MoveDirection dire)
        {
            if(!Moving)
            {
                Point offsetPos = new Point();
                switch (dire)
                {
                    case MoveDirection.Left:
                        offsetPos = new Point(-1, 0);
                        break;
                    case MoveDirection.Right:
                        offsetPos = new Point(1, 0);
                        break;
                    case MoveDirection.Up:
                        offsetPos = new Point(0, -1);
                        break;
                    case MoveDirection.Down:
                        offsetPos = new Point(0, 1);
                        break;
                    case MoveDirection.LeftUp:
                        offsetPos = new Point(-1, -1);
                        break;
                    case MoveDirection.LeftDown:
                        offsetPos = new Point(-1, 1);
                        break;
                    case MoveDirection.RightUp:
                        offsetPos = new Point(1, -1);
                        break;
                    case MoveDirection.RightDown:
                        offsetPos = new Point(1, 1);
                        break;
                    default:
                        break;
                }
                var TileMove = CozyMoveBy.Create(0.5f, offsetPos.ToVector2() * ContentSize);
                var SetPosAction = CozyCallFunc.Create(() => { this.TilePosition += offsetPos; Moving = false; });
                var ActSeq = CozySequence.Create(TileMove, SetPosAction);
                Moving = true;
                this.RunAction(ActSeq);
            }
        }
    }
}
