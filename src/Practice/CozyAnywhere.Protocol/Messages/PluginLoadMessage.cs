using Lidgren.Network;
using NetworkProtocol;

namespace CozyAnywhere.Protocol.Messages
{
    public class PluginLoadMessage : IMessage
    {
        public uint Id { get { return MessageId.PluginLoadMessage; } }

        public void Write(NetOutgoingMessage om)
        {
        }

        public void Read(NetIncomingMessage im)
        {
        }
    }
}