using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;

namespace CozyKxlol.Network.Msg
{
    public struct Msg_AgarPlayInfo : MsgBase
    {
        public const byte Add       = 0;
        public const byte Remove    = 1;
        public const byte Changed   = 2;

        public int Id { get { return MsgId.AgarPlayInfo; } }
        public byte Operat { get; set; }
        public uint PlayerId { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Radius { get; set; }
        public uint Color { get; set; }

        public void W(NetOutgoingMessage om)
        {
            om.Write(Operat);
            om.Write(PlayerId);
            if(Operat == Add || Operat == Changed)
            {
                om.Write(X);
                om.Write(Y);
                om.Write(Radius);
                om.Write(Color);
            }
        }

        public void R(NetIncomingMessage im)
        {
            Operat      = im.ReadByte();
            PlayerId    = im.ReadUInt32();
            if (Operat == Add || Operat == Changed)
            {
                X       = im.ReadFloat();
                Y       = im.ReadFloat();
                Radius  = im.ReadFloat();
                Color   = im.ReadUInt32();
            }
        }
    }
}
