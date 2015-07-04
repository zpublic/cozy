using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkHelper.Messages;
using Lidgren.Network;

namespace NetworkProtocol.Protocol
{
    public class CommandMessage : IMessage
    {
        public uint Id { get { return DefaultMessageId.CommandMessage; } }

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
