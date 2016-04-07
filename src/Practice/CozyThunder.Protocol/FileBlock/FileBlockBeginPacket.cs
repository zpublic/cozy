namespace CozyThunder.Protocol.FileBlock
{
    public class FileBlockBeginPacket : Packet
    {
        internal static readonly int PacketId = 10001;
        private const int PacketLength = 8;

        public FileBlockBeginPacket()
        {
        }

        public override void Encode(byte[] buffer, int offset)
        {
            int written = offset;
            written += Write(buffer, written, PacketLength);
            written += Write(buffer, written, PacketId);
        }

        public override void Decode(byte[] buffer, int offset, int length)
        {
        }

        public override int ByteLength
        {
            get { return PacketLength; }
        }
    }
}
