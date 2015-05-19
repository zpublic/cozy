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

        public void W(NetOutgoingMessage om)
        {
            om.Write(Uid);
            om.Write(X);
            om.Write(Y);
        }

        public void R(NetIncomingMessage im)
        {
            Uid = im.ReadUInt32();
            X = im.ReadFloat();
            Y = im.ReadFloat();
        }
    }
}
