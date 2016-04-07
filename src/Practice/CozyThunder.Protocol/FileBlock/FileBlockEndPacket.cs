namespace CozyThunder.Protocol.FileBlock
{
    public class FileBlockEndPacket : Packet
    {
        internal static readonly int PacketId = 10003;
        private const int PacketLength = 8;

        public FileBlockEndPacket()
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
