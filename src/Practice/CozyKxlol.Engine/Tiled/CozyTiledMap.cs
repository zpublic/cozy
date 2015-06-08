using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyKxlol.Engine.Tiled.Impl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CozyKxlol.Engine.Tiled
{
    public class CozyTiledMap : CozyNode
    {
        public Point TiledMapSize { get; private set; }
        private CozyTiledData TiledData { get; set; }
        public Vector2 NodeContentSize { get; set; }

        public CozyTiledMap(Point MapSize)
        {
            TiledMapSize    = MapSize;
            TiledData       = new CozyTiledData(MapSize.X, MapSize.Y);
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
                    var drawPos = CozyTiledPositionHelper.ConvertTiledPositionToPosition(new Point(i, j), NodeContentSize) + GlobalPosition;
                    node.DrawAt(gameTime, spriteBatch, drawPos, NodeContentSize);
                }
            }
        }

        public void LoadData(ICozyLoader loader)
        {
            loader.Load(TiledData);
        }

        public void Change(int x, int y, uint data)
        {
            TiledData.Change(x, y, data);
        }
    }
}
