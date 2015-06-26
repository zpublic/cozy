using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;

namespace CozyKxlol.Network.Msg.Happy
{
    public struct Msg_HappyPlayerQuit : MsgBase
    {
        public int Id { get { return MsgId.HappyPlayerQuit; } }
        public uint Uid { get; set; }

        public void W(NetOutgoingMessage om)
        {
            om.Write(Uid);
        }

        public void R(NetIncomingMessage im)
        {
            Uid = im.ReadUInt32();
        }
    }
}
