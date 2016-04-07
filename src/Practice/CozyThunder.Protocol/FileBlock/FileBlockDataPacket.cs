namespace CozyThunder.Protocol.FileBlock
{
    public class FileBlockDataPacket : Packet
    {
        internal static readonly int PacketId = 10002;
        private const int PacketLength = 8 + 3072;
        public byte[] data3k;

        public FileBlockDataPacket()
        {
        }

        public FileBlockDataPacket(byte[] data)
        {
            data3k = data;
        }

        public override void Encode(byte[] buffer, int offset)
        {
            int written = offset;
            written += Write(buffer, written, PacketLength);
            written += Write(buffer, written, PacketId);
            written += Write(buffer, written, data3k);
        }

        public override void Decode(byte[] buffer, int offset, int length)
        {
            this.data3k = ReadBytes(buffer, offset + 8, length - 8);
        }

        public override int ByteLength
        {
            get { return PacketLength; }
        }
    }
}
