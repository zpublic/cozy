using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyKxlol.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Starbound.Input;
using Starbound.UI.Resources;
using Starbound.UI.Controls;
using Starbound.UI.XNA.Resources;
using Starbound.UI.XNA.Renderers;
using CozyKxlol.Engine.Tiled;

namespace CozyKxlol.MapEditor
{
    public class MapEditorSceneLayer : CozyLayer
    {
        public static TiledMapDataContainer Container { get; set; }

        KeyboardEvents keyboard;
        MouseEvents mouse;

        public MapEditorSceneTiledLayer Display { get; set; }
        public MapEditorSceneOperateLayer Operat { get; set; }

        public const int MapSize_X = 30;
        public const int MapSize_Y = 20;

        public MapEditorSceneLayer()
        {
            CozyTiledFactory.Create = OnCreate;

            Container   = new TiledMapDataContainer(MapSize_X, MapSize_Y);

            Display     = new MapEditorSceneTiledLayer();
            this.AddChind(Display, 1);
            Operat      = new MapEditorSceneOperateLayer();
            this.AddChind(Operat, 2);

            Operat.TiledCommandMessages += (sender, msg) =>
            {
                msg.Command.Execute(Container);
            };


            #region Keyboard Event

            keyboard = new KeyboardEvents();
            keyboard.KeyPressed += (sender, e) =>
            {
                switch (e.Key)
                {
                    default:
                        break;
                }
            };
            keyboard.KeyReleased += (sender, e) =>
            {
                switch (e.Key)
                {
                    default:
                        break;
                }
            };

            #endregion

            #region Mouse Event

            mouse = new MouseEvents();
            mouse.MouseMoved += (sender, msg) =>
            {

            };

            mouse.ButtonClicked += (sender, msg) =>
            {
                MapEditorSceneLayer.Container.Write(1, 2, 1);
            };

            #endregion 

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            keyboard.Update(gameTime);

#if EnableMouse
            mouse.Update(gameTime);
#endif
        }

        public CozyTiledNode OnCreate(uint id)
        {
            CozyTiledNode node = null;
            switch (id)
            {
                case 1:
                    node = new CozyGreenTiled();
                    break;
                default:
                    node = new CozyDefaultTiled();
                    break;
            }
            return node;
        }
    }
}
