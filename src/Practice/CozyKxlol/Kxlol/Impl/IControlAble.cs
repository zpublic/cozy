using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Starbound.Input;

namespace CozyKxlol.Kxlol.Impl
{
    interface IControlAble
    {
        void OnKeyPressd(object sender, KeyboardEventArgs e);
        void OnKeyResleased(object sender, KeyboardEventArgs e);
    }
}
