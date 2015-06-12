using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyKxlol.Engine;
using CozyKxlol.Engine.Tiled;
using CozyKxlol.Kxlol.Object.Tiled;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace CozyKxlol.Kxlol.Scene
{
    public class HappinessGameLayer : CozyLayer
    {
        public CozyTiledMap Tileds { get; set; }
        public static HappinessGameLayer Create()
        {
            var layer = new HappinessGameLayer();
            layer.Init();
            return layer;
        }

        public virtual bool Init()
        {
            RegisterTiled();

            Tileds                  = new CozyTiledMap(new Point(15, 20));
            Tileds.NodeContentSize  = Vector2.One * 32;
            this.AddChind(Tileds);

            TestCase();
            return true;
        }

        private void RegisterTiled()
        {
            CozyTiledFactory.RegisterTiled(CozyTiledId.RedTiled, new CozyRedTiled());
            CozyTiledFactory.RegisterTiled(CozyTiledId.GreenTiled, new CozyGreenTiled());
        }

        public void TestCase()
        {
            var json = new CozyTiledJsonParser();
            
            var fs = new StreamReader(new FileStream("d:\\tiles.json", FileMode.Open, FileAccess.Read));

            var result = json.parser(fs.ReadToEnd());
            var data = result as CozyTileJsonData;
            if(data.tiles != null)
            {
                var tiles = data.tiles;
                if(tiles.type.Equals("tiles"))
                {
                    // TODO 分割图片
                }
                else if(tiles.type.Equals("tile"))
                {
                    // TODO 取图片里的一块
                }
            }
            if(data.blocks != null)
            {
                // TODO 用于编辑器块绘制

                var blocks = data.blocks;
                switch(blocks.type)
                {
                    case "rect":

                        break;
                    case "square":

                        break;
                    default:
                        break;
                }
            }
        }
    }
}
