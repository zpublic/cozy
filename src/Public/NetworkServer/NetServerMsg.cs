using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;

namespace NetworkServer
{
    public class NetServerMsg
    {
        public NetOutgoingMessage Msg { get; set; }
        public NetDeliveryMethod Method { get; set; }
        public NetServerMsg(NetOutgoingMessage msg, NetDeliveryMethod method)
        {
            Msg     = msg;
            Method  = method;
        }
    }
}
