using Lidgren.Network;
using NetworkProtocol;

namespace CozyAnywhere.Protocol.Messages
{
    public class DisconnectMessage : IMessage
    {
        public uint Id { get { return MessageId.DisconnectMessage; } }

        public void Write(NetOutgoingMessage om)
        {
        }

        public void Read(NetIncomingMessage im)
        {
        }
    }
}
