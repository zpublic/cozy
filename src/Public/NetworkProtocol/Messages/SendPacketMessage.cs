using Lidgren.Network;

namespace NetworkProtocol.Messages
{
    public class SendPacketMessage : IMessage
    {
        public uint Id { get { return DefaultMessageId.SendPacketMessage; } }

        public long UniqueIdentifier { get; set; }

        public int TargetSize { get; set; }

        public void Write(NetBuffer om)
        {
            om.Write(UniqueIdentifier);
            om.Write(TargetSize);
        }

        public void Read(NetBuffer im)
        {
            UniqueIdentifier    = im.ReadInt64();
            TargetSize          = im.ReadInt32();
        }
    }
}
