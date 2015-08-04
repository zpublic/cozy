using NetworkProtocol;
using Lidgren.Network;

namespace CozyAnywhere.Protocol.Messages
{
    public class QueryConnectMessageRsp : IMessage
    {
        public const byte ServerType        = 0;
        public const byte ClientType        = 1;
        public const byte RelayServerType   = 2;

        public uint Id { get { return MessageId.QueryConnectMessageRsp; } }

        public byte ConnectionType { get; set; }

        public void Write(NetBuffer om)
        {
            om.Write(ConnectionType);
        }

        public void Read(NetBuffer im)
        {
            ConnectionType = im.ReadByte();
        }
    }
}
