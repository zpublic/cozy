using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkHelper.Messages;
using Lidgren.Network;
using NetworkProtocol;

namespace NetworkProtocol.Protocol
{
    public class CommandMsg : IMessage
    {
        public uint Id { get { return MessageId.CommandMessage; } }

        public uint CommandId { get; set; }

        public void Write(NetOutgoingMessage om)
        {
            om.Write(CommandId);
        }

        public void Read(NetIncomingMessage im)
        {
            CommandId = im.ReadUInt32();
        }
    }
}
