using System.Text;

namespace CozyThunder.Protocol
{
    public class StringPacket : Packet
    {
        internal static readonly int PacketId = 1;
        private const int PacketLength = 8;
        public string data;

        public StringPacket()
        {
        }

        public StringPacket(string d)
        {
            this.data = d;
        }

        public override void Encode(byte[] buffer, int offset)
        {
            int written = offset;
            written += Write(buffer, written, PacketLength + Encoding.ASCII.GetByteCount(data));
            written += Write(buffer, written, PacketId);
            written += WriteAscii(buffer, written, data);
        }

        public override void Decode(byte[] buffer, int offset, int length)
        {
            this.data = ReadString(buffer, offset + PacketLength, length - PacketLength);
        }

        public override int ByteLength
        {
            get { return PacketLength + Encoding.ASCII.GetByteCount(data); }
        }
    }
}
