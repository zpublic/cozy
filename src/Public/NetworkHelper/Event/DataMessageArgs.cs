using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;

namespace NetworkHelper.Event
{
    public class DataMessageArgs : EventArgs
    {
        public NetIncomingMessage Input { get; set; }
        public DataMessageArgs(NetIncomingMessage input)
        {
            Input = input;
        }
    }
}
