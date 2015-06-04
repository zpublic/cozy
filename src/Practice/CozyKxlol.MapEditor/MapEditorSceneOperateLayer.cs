using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyKxlol.Engine;
using Starbound.Input;

namespace CozyKxlol.MapEditor
{
    public class MapEditorSceneOperateLayer : CozyLayer
    {
        // accept keyboard shortcuts
        // and accept mouse click
        // then modify TiledMapDataContainer`s data

        public MouseEvents Mouse { get; set; }
        public KeyboardEvents Keyboard { get; set; }

        public const uint S_Add = 0000;
        public const uint S_Remove = 0001;

        public uint Status { get; set; }

        public MapEditorSceneOperateLayer()
        {
            Mouse = new MouseEvents();
            Keyboard = new KeyboardEvents();

            Mouse.ButtonClicked += (sender, msg) =>
            {
                if(msg.Button == MouseButton.Left)
                {
                    if (Status == S_Add)
                    {
                        // add Tiled
                    }
                }
            };

            Mouse.MouseMoved += (sender, msg) =>
            {
                // draw Tiled with Mouse

            };

            Keyboard.KeyPressed += (sender, msg) =>
            {
                // doSomething
            };

            Keyboard.KeyReleased += (sender, msg) =>
            {
                // doSomething
            };
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);

            Mouse.Update(gameTime);
            Keyboard.Update(gameTime);
        }
    }
}
