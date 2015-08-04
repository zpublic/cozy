using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkProtocol
{
    public static class DefaultMessageId
    {
        public const uint SendPacketMessage = 000;
        public const uint SendPacketMessageRecv = 001;
        public const uint PacketMessage = 002;
    }
}
