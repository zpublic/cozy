using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;

namespace NetworkProtocol
{
    public class PacketMessage : IMessage
    {
        public uint Id { get { return DefaultMessageId.PacketMessage; } }

        public long MessagePacketId { get; set;}

        public int Number { get; set; }

        // MaxBytesLength = 60000
        public byte[] Bytes { get; set; }

        public void Write(NetBuffer om)
        {
            om.Write(MessagePacketId);
            om.Write(Number);
            if (Bytes != null)
            {
                om.Write(Bytes.Length);
                om.Write(Bytes);
            }
            else
            {
                om.Write(0);
            }
        }


        public void Read(NetBuffer im)
        {
            MessagePacketId = im.ReadInt64();
            Number = im.ReadInt32();
            int length = im.ReadInt32();
            if (length != 0)
            {
                Bytes = im.ReadBytes(length);
            }
        }
    }
}
