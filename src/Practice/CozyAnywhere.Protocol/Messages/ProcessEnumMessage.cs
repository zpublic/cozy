using Lidgren.Network;
using NetworkProtocol;

namespace CozyAnywhere.Protocol.Messages
{
    public struct ProcessEnumMessage : IMessage
    {
        public uint Id { get { return MessageId.ProcessEnumMessage; } }

        public void Write(NetOutgoingMessage om)
        {
        }

        public void Read(NetIncomingMessage im)
        {
        }
    }
}