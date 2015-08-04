using Lidgren.Network;


namespace NetworkProtocol
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
            UniqueIdentifier = im.ReadInt64();
            MessagePacketId = im.ReadInt64();
        }
    }
}
