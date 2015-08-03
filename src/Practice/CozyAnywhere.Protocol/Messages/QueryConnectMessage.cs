using NetworkProtocol;
using Lidgren.Network;

namespace CozyAnywhere.Protocol.Messages
{
    public class QueryConnectMessage : IMessage
    {
        public uint Id { get { return MessageId.QueryConnectMessage; } }

        public void Write(NetOutgoingMessage om)
        {
        }

        public void Read(NetIncomingMessage im)
        {
        }
    }
}
