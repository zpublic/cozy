using Lidgren.Network;

namespace NetworkRelayServer
{
    public class NetRelayMsg
    {
        public NetOutgoingMessage Msg { get; set; }

        public NetDeliveryMethod Method { get; set; }

        public NetRelayMsg(NetOutgoingMessage msg, NetDeliveryMethod method)
        {
            Msg     = msg;
            Method  = method;
        }
    }
}
