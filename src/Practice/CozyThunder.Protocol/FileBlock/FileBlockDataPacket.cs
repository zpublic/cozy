namespace CozyThunder.Protocol.FileBlock
{
    public class FileBlockDataPacket : Packet
    {
        internal static readonly int PacketId = 10002;
        private const int PacketLength = 8 + 512;
        public byte[] data512B;

        public FileBlockDataPacket()
        {
        }

        public FileBlockDataPacket(byte[] data)
        {
            data512B = data;
        }

        public override void Encode(byte[] buffer, int offset)
        {
            int written = offset;
            written += Write(buffer, written, PacketLength);
            written += Write(buffer, written, PacketId);
            written += Write(buffer, written, data512B);
        }

        public override void Decode(byte[] buffer, int offset, int length)
        {
            this.data512B = ReadBytes(buffer, offset + 8, length - 8);
        }

        public override int ByteLength
        {
            get { return PacketLength; }
        }
    }
}
