namespace CozyThunder.Protocol.FileBlock
{
    public class FileBlockDataPacket : Packet
    {
        internal static readonly int PacketId = 10002;
        private const int PacketLength = 12;
        public int len;
        public byte[] data3k;

        public FileBlockDataPacket()
        {
        }

        public FileBlockDataPacket(byte[] data)
        {
            len = data.Length;
            data3k = data;
        }

        public override void Encode(byte[] buffer, int offset)
        {
            int written = offset;
            written += Write(buffer, written, PacketLength + len);
            written += Write(buffer, written, PacketId);
            written += Write(buffer, written, len);
            written += Write(buffer, written, data3k);
        }

        public override void Decode(byte[] buffer, int offset, int length)
        {
            ReadInt(buffer, ref offset);
            ReadInt(buffer, ref offset);
            len = ReadInt(buffer, ref offset);
            this.data3k = ReadBytes(buffer, offset, len);
        }

        public override int ByteLength
        {
            get { return PacketLength + data3k.Length; }
        }
    }
}
