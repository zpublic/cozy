using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyKxlol.Engine;
using Starbound.Input;
using Microsoft.Xna.Framework.Input;

namespace CozyKxlol.Kxlol.Scene
{
    public partial class BallGameSceneLayer
    {
        KeyboardEvents keyboard;

        private void OnKeyPressed(object sender, KeyboardEventArgs e)
        {
            switch (e.Key)
            {
                case Keys.W:
                case Keys.S:
                case Keys.A:
                case Keys.D:
                    if (Player != null)
                    {
                        Player.OnKeyPressd(sender, e);
                    }
                    break;
                default:
                    break;
            }
        }

        private void OnKeyReleased(object sender, KeyboardEventArgs e)
        {
            switch (e.Key)
            {
                case Keys.W:
                case Keys.S:
                case Keys.A:
                case Keys.D:
                    if (Player != null)
                    {
                        Player.OnKeyReleased(sender, e);
                    }
                    break;
                default:
                    break;
            }
        }

        public void InitKeyboard()
        {
            keyboard = new KeyboardEvents();
        }
    }
}
