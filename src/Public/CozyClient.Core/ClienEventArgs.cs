using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyClient.Core
{
    public class ClienEventArgs : EventArgs
    {
        public NetClient Client { get; set; }

        public NetIncomingMessage Message { get; set; }

        public ClienEventArgs(NetClient client, NetIncomingMessage msg)
        {
            Client = client;
            Message = msg;
        }
    }
}
