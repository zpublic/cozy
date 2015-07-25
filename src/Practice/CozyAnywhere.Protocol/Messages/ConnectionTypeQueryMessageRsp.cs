using NetworkProtocol;
using Lidgren.Network;

namespace CozyAnywhere.Protocol.Messages
{
    public class ConnectionTypeQueryMessageRsp : IMessage
    {
        public uint Id { get { return MessageId.ConnectionTypeQueryMessageRsp; } }

        public const byte ServerType = 0;
        public const byte ClientType = 1;

        public byte ConnectionType { get; set; }

        public void Write(NetOutgoingMessage om)
        {
            om.Write(ConnectionType);
        }

        public void Read(NetIncomingMessage im)
        {
            ConnectionType = im.ReadByte();
        }
    }
}
