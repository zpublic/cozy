using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;

namespace CozyKxlol.Network.Msg
{
    public struct Msg_AgarFixedBall : MsgBase
    {
        public const byte Add       = 0;
        public const byte Remove    = 1;
        public int Id { get { return MsgId.AgarFixedBall; } }

        public byte Operat { get; set; }
        public uint BallId { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public uint Color { get; set; }


        public void W(NetOutgoingMessage om)
        {
            om.Write(Operat);
            om.Write(BallId);
            if(Operat == Add)
            {
                om.Write(X);
                om.Write(Y);
                om.Write(Color);
            }
        }

        public void R(NetIncomingMessage im)
        {
            Operat = im.ReadByte();
            BallId = im.ReadUInt32();
            if(Operat == Add)
            {
                X       = im.ReadFloat();
                Y       = im.ReadFloat();
                Color   = im.ReadUInt32();
            }
        }
    }
}
