using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;

namespace NetwrokClient
{
    class NetClientMsg
    {
        public NetOutgoingMessage Msg { get; set; }
        public NetDeliveryMethod Method { get; set; }
        public NetClientMsg(NetOutgoingMessage msg, NetDeliveryMethod method)
        {
            Msg = msg;
            Method = method;
        }
    }
}
