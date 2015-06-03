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
    class MapEditorSceneLayer : CozyLayer
    {
        KeyboardEvents keyboard;
        MouseEvents mouse;

        public MapEditorSceneLayer()
        {
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
