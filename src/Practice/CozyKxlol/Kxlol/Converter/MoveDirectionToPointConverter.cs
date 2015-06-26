using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using CozyKxlol.Kxlol.Interface;

namespace CozyKxlol.Kxlol.Converter
{
    public static class MoveDirectionToPointConverter
    {
        public static Point MoveDirectionConvertToPoint(MoveDirection dire)
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
            return offsetPos;
        }
        public static MoveDirection PointConvertToMoveDirection(Point point)
        {
            MoveDirection dire = MoveDirection.Unknow;
            if(point.X == 1)
            {
                if(point.Y == 1)
                {
                    dire = MoveDirection.RightDown;
                }
                else if(point.Y == -1)
                {
                    dire = MoveDirection.RightUp;
                }
                else
                {
                    dire = MoveDirection.Right;
                }
            }
            else if(point.X == -1)
            {
                if (point.Y == 1)
                {
                    dire = MoveDirection.LeftDown;
                }
                else if (point.Y == -1)
                {
                    dire = MoveDirection.LeftUp;
                }
                else
                {
                    dire = MoveDirection.Left;
                }
            }
            else
            {
                if(point.Y == 1)
                {
                    dire = MoveDirection.Down;
                }
                else if(point.Y == -1)
                {
                    dire = MoveDirection.Up;
                }
                else
                {
                    dire = MoveDirection.Unknow;
                }
            }
            return dire;
        }
    }
}
