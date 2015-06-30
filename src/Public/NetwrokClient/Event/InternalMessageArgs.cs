using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetwrokClient.Event
{
    public class InternalMessageArgs : EventArgs
    {
        public String Msg { get; set; }
        public InternalMessageArgs(String msg)
        {
            Msg = msg;
        }
    }
}
