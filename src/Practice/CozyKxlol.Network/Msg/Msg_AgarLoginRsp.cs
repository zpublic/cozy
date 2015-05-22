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
        public float X { get; set; }
        public float Y { get; set; }
        public int Radius { get; set; }
        public uint Color { get; set; }

        public void W(NetOutgoingMessage om)
        {
            om.Write(Uid);
            om.Write(X);
            om.Write(Y);
            om.Write(Radius);
            om.Write(Color);
        }

        public void R(NetIncomingMessage im)
        {
            Uid     = im.ReadUInt32();
            X       = im.ReadFloat();
            Y       = im.ReadFloat();
            Radius  = im.ReadInt32();
            Color   = im.ReadUInt32();
        }
    }
}
