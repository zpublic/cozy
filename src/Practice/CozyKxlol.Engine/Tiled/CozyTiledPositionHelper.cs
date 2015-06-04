using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CozyKxlol.Engine.Tiled
{
    public static class CozyTiledPositionHelper
    {
        public static Point ConvertPositionToTiledPosition(Vector2 pos, Vector2 NodeContentSize)
        {
            float x = pos.X / NodeContentSize.X;
            float y = pos.Y / NodeContentSize.Y;
            return new Point((int)x, (int)y);
        }

        public static Vector2 ConvertTiledPositionToPosition(Point pos, Vector2 NodeContentSize)
        {
            return new Vector2(NodeContentSize.X * pos.X, NodeContentSize.Y * pos.Y);
        }
    }
}
