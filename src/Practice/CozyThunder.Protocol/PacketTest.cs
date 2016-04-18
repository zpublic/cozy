using System;
using System.Net;

namespace CozyThunder.Protocol
{
    public class PacketTest
    {
        public int PacketId;
        public int PacketLength;

        public PacketTest(byte[] buffer, int offset)
        {
            PacketLength = ReadInt(buffer, offset);
            PacketId = ReadInt(buffer, offset + 4);
        }

        static public int ReadInt(byte[] buffer, int offset)
        {
            int ret = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, offset));
            return ret;
        }
    }
}
