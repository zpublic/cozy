using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyDungeon.Public.Controls.Events
{
    public class ItemRemoveEventArgs : EventArgs
    {
        public CozyControl Control { get; set; }

        public ItemRemoveEventArgs(CozyControl control)
        {
            Control = control;
        }
    }
}
