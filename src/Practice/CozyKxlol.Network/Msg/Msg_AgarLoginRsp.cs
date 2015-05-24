using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;

namespace CozyKxlol.Network.Msg
{
    public struct Msg_AgarLoginRsp : MsgBase
    {
        public int Id { get { return MsgId.AgarLoginRsp; } }
        public uint Uid { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public void W(NetOutgoingMessage om)
        {
            om.Write(Uid);
            om.Write(Width);
            om.Write(Height);
        }

        public void R(NetIncomingMessage im)
        {
            Uid     = im.ReadUInt32();
            Width   = im.ReadInt32();
            Height  = im.ReadInt32();
        }
    }
}
