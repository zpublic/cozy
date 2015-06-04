#define EnableMouse

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

namespace CozyKxlol.MapEditor
{
    public class MapEditorSceneLayer : CozyLayer
    {
        public static TiledMapDataContainer Container { get; set; }

        KeyboardEvents keyboard;
        MouseEvents mouse;

        public MapEditorSceneLayer()
        {
            Container = new TiledMapDataContainer(15, 20);

            var display = new MapEditorSceneTiledLayer();
            this.AddChind(display);
            var operat = new MapEditorSceneOperateLayer();
            this.AddChind(operat);

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

            mouse = new MouseEvents();
            mouse.MouseMoved += (sender, msg) =>
            {

            };

            mouse.ButtonClicked += (sender, msg) =>
            {
                MapEditorSceneLayer.Container.Write(1, 2, 1);
            };
        }

        public override void Update(GameTime gameTime)
        {
            keyboard.Update(gameTime);

#if EnableMouse
            mouse.Update(gameTime);
#endif
        }
    }
}
