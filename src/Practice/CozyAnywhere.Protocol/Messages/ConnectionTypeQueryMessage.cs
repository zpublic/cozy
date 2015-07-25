using NetworkProtocol;
using Lidgren.Network;

namespace CozyAnywhere.Protocol.Messages
{
    public class ConnectionTypeQueryMessage : IMessage
    {
        public uint Id { get { return MessageId.ConnectionTypeQueryMessage; } }

        public void Write(NetOutgoingMessage om)
        {
        }

        public void Read(NetIncomingMessage im)
        {
        }
    }
}
