using NetworkProtocol;
using Lidgren.Network;

namespace CozyAnywhere.Protocol.Messages
{
    public class ConnectMessage : IMessage
    {
        public uint Id { get { return MessageId.ConnectMessage; } }

        public bool CanConnect { get; set; }

        public void Write(NetOutgoingMessage om)
        {
            om.Write(CanConnect);
        }

        public void Read(NetIncomingMessage im)
        {
            CanConnect = im.ReadBoolean();
        }
    }
}
