using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyDungeon.Public.Controls.Events
{
    public class ItemAddEventArgs : EventArgs
    {
        public CozyControl Control { get; set; }

        public ItemAddEventArgs(CozyControl control)
        {
            Control = control;
        }
    }
}
