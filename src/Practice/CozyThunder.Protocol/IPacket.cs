namespace CozyThunder.Protocol
{
    public interface IPacket
    {
        int ByteLength { get; }
        byte[] Encode();
        void Encode(byte[] buffer, int offset);
        void Decode(byte[] buffer, int offset, int length);
    }
}
