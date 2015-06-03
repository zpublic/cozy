using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CozyKxlol.Engine.Tiled
{
    public class CozyTiledMap : CozyNode
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

        public CozyTiledMap(Point MapSize)
        {
            TiledData       = new uint[MapSize.X, MapSize.Y];
            NodeContentSize = Vector2.One * 32;
            ContentSize     = new Vector2(NodeContentSize.X * MapSize.X, 
                NodeContentSize.Y * MapSize.Y);
        }

        protected override void DrawSelf(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for(int i = 0; i < TiledMapSize.X; ++i)
            {
                for (int j = 0; j < TiledMapSize.Y; ++j)
                {
                    var node = CozyTiledFactory.GetInstance(TiledData[i, j]);
                    node.ContentSize = NodeContentSize;
                    node.DrawAt(gameTime, spriteBatch, ConvertTiledPosToPosition(i, j));
                }
            }
        }

        // 锚点为(0, 0)
        public Vector2 ConvertTiledPosToPosition(int x, int y)
        {
            return new Vector2(NodeContentSize.X * x, NodeContentSize.Y * y);
        }
    }
}
