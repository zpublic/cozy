using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyKxlol.Engine;
using CozyKxlol.Engine.Tiled;
using CozyKxlol.Kxlol.Object.Tiled;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CozyKxlol.Kxlol.Scene
{
    public class HappinessGameLayer : CozyLayer
    {
        public CozyTiledMap Tileds { get; set; }
        private string DataPath = @".\Content\Data.db";

        public static HappinessGameLayer Create()
        {
            var layer = new HappinessGameLayer();
            layer.Init();
            return layer;
        }

        public virtual bool Init()
        {
            Tileds                  = new CozyTiledMap(new Point(20, 15));
            Tileds.NodeContentSize  = Vector2.One * 32;
            this.AddChind(Tileds);

            LoadMap();
            return true;
        }

        private void LoadMap()
        {
            var loader = new CozyTiledDataLoader(DataPath);
            loader.Load(Tileds);
        }
    }
}
