using Lidgren.Network;


namespace NetworkProtocol.Messages
{
    public class SendPacketMessageRecv : IMessage
    {
        public uint Id { get { return DefaultMessageId.SendPacketMessageRecv; } }

        public long UniqueIdentifier { get; set; }

        public long MessagePacketId { get; set; }

        public void Write(NetBuffer om)
        {
            om.Write(UniqueIdentifier);
            om.Write(MessagePacketId);
        }


        public void Read(NetBuffer im)
        {
            UniqueIdentifier    = im.ReadInt64();
            MessagePacketId     = im.ReadInt64();
        }
    }
}
