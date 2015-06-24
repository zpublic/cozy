using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Starbound.Input;
using Microsoft.Xna.Framework.Input;
using CozyKxlol.Kxlol.Interface;

namespace CozyKxlol.Kxlol.Scene
{
    public partial class HappinessGameLayer
    {
        KeyboardEvents keyboard { get; set; }

        private bool[] KeysStatus = new bool[4];

        private void OnKeyPressed(object sender, KeyboardEventArgs e)
        {
            switch (e.Key)
            {
                case Keys.W:
                    KeysStatus[(int)MoveDirection.Up]       = true;
                    break;
                case Keys.S:
                    KeysStatus[(int)MoveDirection.Down]     = true;
                    break;
                case Keys.A:
                    KeysStatus[(int)MoveDirection.Left]     = true;
                    break;
                case Keys.D:
                    KeysStatus[(int)MoveDirection.Right]    = true;
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
                    KeysStatus[(int)MoveDirection.Up]       = false;
                    break;
                case Keys.S:
                    KeysStatus[(int)MoveDirection.Down]     = false;
                    break;
                case Keys.A:
                    KeysStatus[(int)MoveDirection.Left]     = false;
                    break;
                case Keys.D:
                    KeysStatus[(int)MoveDirection.Right]    = false;
                    break;
                default:
                    break;
            }
        }

        private MoveDirection DirectionNow()
        {
            if(KeysStatus[(int)MoveDirection.Up])
            {
                if(KeysStatus[(int)MoveDirection.Left])
                {
                    return MoveDirection.LeftUp;
                }
                else if (KeysStatus[(int)MoveDirection.Right])
                {
                    return MoveDirection.RightUp;
                }
                return MoveDirection.Up;
            }
            else if (KeysStatus[(int)MoveDirection.Down])
            {
                if (KeysStatus[(int)MoveDirection.Left])
                {
                    return MoveDirection.LeftDown;
                }
                else if (KeysStatus[(int)MoveDirection.Right])
                {
                    return MoveDirection.RightDown;
                }
                return MoveDirection.Down;
            }
            else if (KeysStatus[(int)MoveDirection.Left])
            {
                if (KeysStatus[(int)MoveDirection.Up])
                {
                    return MoveDirection.LeftUp;
                }
                else if (KeysStatus[(int)MoveDirection.Down])
                {
                    return MoveDirection.LeftDown;
                }
                return MoveDirection.Left;
            }
            else if (KeysStatus[(int)MoveDirection.Right])
            {
                if (KeysStatus[(int)MoveDirection.Up])
                {
                    return MoveDirection.RightUp;
                }
                else if (KeysStatus[(int)MoveDirection.Down])
                {
                    return MoveDirection.RightDown;
                }
                return MoveDirection.Right;
            }
            return MoveDirection.Unknow;
        }

        public void InitKeyboard()
        {
            keyboard                = new KeyboardEvents();
            keyboard.KeyPressed     += new EventHandler<KeyboardEventArgs>(OnKeyPressed);
            keyboard.KeyReleased    += new EventHandler<KeyboardEventArgs>(OnKeyReleased);
        }
    }
}
