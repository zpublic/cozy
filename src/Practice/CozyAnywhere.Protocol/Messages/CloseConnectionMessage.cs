using NetworkProtocol;
using Lidgren.Network;

namespace CozyAnywhere.Protocol.Messages
{
    public class CloseConnectionMessage : IMessage
    {
        public uint Id { get { return MessageId.CloseConnectionMessage; } }

        public void Write(NetOutgoingMessage om)
        {
        }

        public void Read(NetIncomingMessage im)
        {
        }
    }
}
