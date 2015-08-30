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
        public CCNode Control { get; set; }

        public ItemAddEventArgs(CCNode control)
        {
            Control = control;
        }
    }
}
