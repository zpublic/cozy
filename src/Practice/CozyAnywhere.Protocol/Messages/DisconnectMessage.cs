using Lidgren.Network;
using NetworkProtocol;

namespace CozyAnywhere.Protocol.Messages
{
    public class DisconnectMessage : IMessage
    {
        public uint Id { get { return MessageId.DisconnectMessage; } }

        public void Write(NetBuffer om)
        {
        }

        public void Read(NetBuffer im)
        {
        }
    }
}
