using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Starbound.Input;

namespace CozyKxlol.Kxlol.Interface
{
    interface IControlAble
    {
        void OnKeyPressd(object sender, KeyboardEventArgs e);
        void OnKeyReleased(object sender, KeyboardEventArgs e);
    }
}
