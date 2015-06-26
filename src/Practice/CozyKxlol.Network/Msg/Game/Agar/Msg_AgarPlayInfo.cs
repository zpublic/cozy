using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;
using CozyKxlol.Network.Msg;

namespace CozyKxlol.Network.Msg.Agar
{
    public struct Msg_AgarPlayInfo : MsgBase
    {
        public const byte Add       = 0;
        public const byte Remove    = 1;
        public const byte Changed   = 2;

        public int Id { get { return MsgId.AgarPlayInfo; } }
        public byte Operat { get; set; }
        public uint UserId { get; set; }
        public uint Tag { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public int Radius { get; set; }
        public uint Color { get; set; }
        public string Name { get; set; }

        public void W(NetOutgoingMessage om)
        {
            om.Write(Operat);
            om.Write(UserId);
            if (Operat == Add || Operat == Changed)
            {
                om.Write(Tag);
                if (GameMessageHelper.Is_Changed(Tag, GameMessageHelper.POSITION_TAG))
                {
                    om.Write(X);
                    om.Write(Y);
                }
                if (GameMessageHelper.Is_Changed(Tag, GameMessageHelper.RADIUS_TAG))
                {
                    om.Write(Radius);
                }
                if (GameMessageHelper.Is_Changed(Tag, GameMessageHelper.COLOR_TAG))
                {
                    om.Write(Color);
                }
                if (GameMessageHelper.Is_Changed(Tag, GameMessageHelper.NAME_TAG))
                {
                    om.Write(Name);
                }
            }
        }

        public void R(NetIncomingMessage im)
        {
            Operat          = im.ReadByte();
            UserId          = im.ReadUInt32();
            if (Operat == Add || Operat == Changed)
            {
                Tag         = im.ReadUInt32();
                if (GameMessageHelper.Is_Changed(Tag, GameMessageHelper.POSITION_TAG))
                {
                    X       = im.ReadFloat();
                    Y       = im.ReadFloat();
                }
                if (GameMessageHelper.Is_Changed(Tag, GameMessageHelper.RADIUS_TAG))
                {
                    Radius  = im.ReadInt32();
                }
                if (GameMessageHelper.Is_Changed(Tag, GameMessageHelper.COLOR_TAG))
                {
                    Color   = im.ReadUInt32();
                }
                if (GameMessageHelper.Is_Changed(Tag, GameMessageHelper.NAME_TAG))
                {
                    Name    = im.ReadString();
                }
            }
        }
    }
}
