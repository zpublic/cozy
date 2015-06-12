using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyKxlol.Engine;
using Microsoft.Xna.Framework;
using CozyKxlol.Engine.Tiled;
using CozyKxlol.Engine.Tiled.Json;
using CozyKxlol.MapEditor.TiledLayer;
using CozyKxlol.MapEditor.OperateLayer;
using CozyKxlol.MapEditor.Tileds;
using CozyKxlol.MapEditor.Command;

namespace CozyKxlol.MapEditor
{
    class MapEditorScene : CozyScene
    {
        public static TiledMapDataContainer Container { get; set; }

        public const int MapSize_X  = 30;
        public const int MapSize_Y  = 20;
        public const int NodeSize   = 32;

        public MapEditorSceneTiledLayer Display { get; set; }
        public MapEditorSceneOperateLayer Operat { get; set; }

        public MapEditorScene()
        {
            // 地图编辑器应该分两层
            // 下层为Engine里的tile绘制层
            // 上层为编辑功能层，支持鼠标和键盘操作

            RegisterTiled();

            Container               = new TiledMapDataContainer(MapSize_X, MapSize_Y);

            Display                 = new MapEditorSceneTiledLayer(Container.MapSize, Vector2.One * NodeSize);
            this.AddChind(Display, 1);

            Operat                  = new MapEditorSceneOperateLayer(Vector2.One * NodeSize);
            this.AddChind(Operat, 2);

            Operat.TiledCommandMessages += (sender, msg) =>
            {
                CommandHistory.Instance.Do(msg.Command, Container);
            };

            Operat.TiledCommandUndo += (sender, msg) =>
            {
                CommandHistory.Instance.Undo(Container);
            };

            TestCase();
        }

        public void RegisterTiled()
        {
            //CozyTiledFactory.RegisterTiled(CozyGreenTiled.TiledId, new CozyGreenTiled());
            //CozyTiledFactory.RegisterTiled(CozyRedTiled.TiledId, new CozyRedTiled());
        }

        public void TestCase()
        {
            var json = new CozyTiledJsonParser();
            var data = json.parser(@"d:\tiles.json") as CozyTileJsonResult;

            if (data.tiles != null)
            {
                var tiles = data.tiles;
                if (tiles.type.Equals("tiles"))
                {
                    var CurrId = tiles.id;
                    // TODO 分割图片
                    var texture = CozyDirector.Instance.TextureCacheInstance.AddImage(tiles.path);
                    for (int i = 0; i < tiles.w; ++i)
                    {
                        for (int j = 0; j < tiles.h; ++j)
                        {
                            Rectangle rect = new Rectangle(32 * i, 32 * j, 32, 32);
                            var tiled = CozySpriteTiled.Create(texture, rect);
                            CozyTiledFactory.RegisterTiled(CurrId++, tiled);
                        }
                    }

                }
                else if (tiles.type.Equals("tile"))
                {
                    // TODO 取图片里的一块
                    var texture = CozyDirector.Instance.TextureCacheInstance.AddImage(tiles.path);
                    Rectangle rect = new Rectangle(32 * tiles.x, 32 * tiles.y, 32, 32);
                    var tiled = CozySpriteTiled.Create(texture, rect);
                    CozyTiledFactory.RegisterTiled(tiles.id, tiled);

                    Operat.CurrentTiledId = tiles.id;
                }
            }
            if (data.blocks != null)
            {
                // TODO 用于编辑器块绘制

                var blocks = data.blocks;
                switch (blocks.type)
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
