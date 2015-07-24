using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;

namespace CozyKxlol.Network.Msg.Happy
{
    public struct Msg_HappyPlayerLoginRsp : MsgBase
    {
        public int Id { get { return MsgId.HappyPlayerLoginRsp; } }

        public uint Uid { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public uint SpriteId { get; set; }

        public void W(NetOutgoingMessage om)
        {
            om.Write(Uid);
            om.Write(X);
            om.Write(Y);
            om.Write(SpriteId);
        }

        public void R(NetIncomingMessage im)
        {
            Uid         = im.ReadUInt32();
            X           = im.ReadInt32();
            Y           = im.ReadInt32();
            SpriteId    = im.ReadUInt32();
        }
    }
}
