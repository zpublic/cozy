using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;

namespace CozyKxlol.Network.Msg
{
    public struct Msg_AgarSelf : MsgBase
    {
        public const byte GroupUp   = 0;
        public const byte Dead      = 1;
        public int Id { get { return MsgId.AgarSelf; } }
        public byte Operat { get; set; }
        public uint UserId { get; set; }
        public float Radius { get; set; }

        public void W(NetOutgoingMessage om)
        {
            om.Write(Operat);
            om.Write(UserId);
            if(Operat == GroupUp)
            {
                om.Write(Radius);
            }
        }

        public void R(NetIncomingMessage im)
        {
            Operat      = im.ReadByte();
            UserId      = im.ReadUInt32();
            if(Operat == GroupUp)
            {
                Radius  = im.ReadFloat();
            }
        }
    }
}
