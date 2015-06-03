using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CozyKxlol.Engine.Tiled
{
    public class CozyTiledLayer : CozyLayer
    {
        public Point TiledMapSize 
        { 
            get
            {
                return new Point(TiledData.GetLength(0), TiledData.GetLength(1));
            }
        }

        private uint[,] _TiledData;
        public uint [,] TiledData
        {
            get
            {
                return _TiledData;
            }
            set
            {
                _TiledData = value;
            }
        }

        public Vector2 NodeContentSize { get; set; }

        public CozyTiledLayer(Point Size)
        {
            TiledData       = new uint[Size.X, Size.Y];

            var winSize     = CozyDirector.Instance.WindowSize;
            NodeContentSize = new Vector2(winSize.X / Size.X, winSize.Y / Size.Y);
        }

        protected override void DrawSelf(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for(int i = 0; i < TiledMapSize.X; ++i)
            {
                for (int j = 0; j < TiledMapSize.Y; ++j)
                {
                    var node = CozyTiledFactory.GetInstance(TiledData[i, j]);
                    node.DrawAt(gameTime, spriteBatch, ConvertTiledPosToPosition(i, j));
                }
            }
        }

        // 要求锚点为(0.5, 0.5)
        public Vector2 ConvertTiledPosToPosition(int x, int y)
        {
            return new Vector2(
                NodeContentSize.X * (0.5f + x),
                NodeContentSize.Y * (0.5f + y));
        }
    }
}
