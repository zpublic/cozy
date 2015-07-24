using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;

namespace CozyKxlol.Network.Msg.Agar
{
    public struct Msg_AgarBorn : MsgBase
    {
        public int Id { get { return MsgId.AgarBorn; } }

        public uint UserId { get; set; }

        public string Name { get; set; }
        public void W(NetOutgoingMessage om)
        {
            om.Write(UserId);
            om.Write(Name);
        }

        public void R(NetIncomingMessage im)
        {
            UserId = im.ReadUInt32();
            Name = im.ReadString();
        }
    }
}
