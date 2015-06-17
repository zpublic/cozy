using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyKxlol.Engine;
using Microsoft.Xna.Framework;
using CozyKxlol.Engine.Tiled;
using CozyKxlol.Engine.Tiled.Json;
using CozyKxlol.Engine.Tiled.Json.Strategy;
using CozyKxlol.MapEditor.Gui.TiledLayer;
using CozyKxlol.MapEditor.Gui.OperateLayer;
using CozyKxlol.MapEditor.Command;
using CozyKxlol.MapEditor.Plugin.TilesPlugin;

namespace CozyKxlol.MapEditor.Gui
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

            TilesPluginMgr.RegistAllTiles();

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
            //TestCase();
        }

        public void TestCase()
        {
            var data = CozyDirector.Instance.JsonManagerInstance.ParseWithFile(@".\Content\tiles.json");

            Random rd = new Random();
            // TODO 用于编辑器块绘制
            if(data.square != null)
            {
                foreach(var square in data.square)
                {
                    var command = new ContainerModifyBlockCommand(square);
                    command.Origion = new Point(rd.Next() % 20, rd.Next() % 20);
                    CommandHistory.Instance.Do(command, Container);
                }
            }
            if(data.rect != null)
            {
                foreach (var rectangle in data.rect)
                {
                    var command = new ContainerModifyBlockCommand(rectangle);
                    command.Origion = new Point(rd.Next() % 20, rd.Next() % 20);
                    CommandHistory.Instance.Do(command, Container);
                }
            }
        }
    }
}
