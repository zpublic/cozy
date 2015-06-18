using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;

namespace CozyKxlol.Network.Msg
{
    public struct Msg_AgarSelf : MsgBase
    {
        public const byte Born      = 0;
        public const byte GroupUp   = 1;
        public const byte Dead      = 2;
        public int Id { get { return MsgId.AgarSelf; } }
        public byte Operat { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public int Radius { get; set; }
        public uint Color { get; set; }

        public void W(NetOutgoingMessage om)
        {
            om.Write(Operat);
            if(Operat == GroupUp)
            {
                om.Write(Radius);
            }
            else if(Operat == Born)
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
            if(Operat == GroupUp)
            {
                Radius  = im.ReadInt32();
            }
            else if(Operat == Born)
            {
                X = im.ReadFloat();
                Y = im.ReadFloat();
                Radius = im.ReadInt32();
                Color = im.ReadUInt32();
            }
        }
    }
}
