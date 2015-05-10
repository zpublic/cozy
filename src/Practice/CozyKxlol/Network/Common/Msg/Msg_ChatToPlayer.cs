using Microsoft.Xna.Framework.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.Network.Common.Msg
{
    public class Msg_ChatToPlayer : MsgBase
    {
        public Msg_ChatToPlayer()
            : base(MsgId.ChatToPlayer)
        {
        }

        public String ChatTo
        {
            set;
            get;
        }

        public String ChatText
        {
            set;
            get;
        }

        override public PacketWriter GetPacket()
        {
            mPacketWriter.Write(ChatTo);
            mPacketWriter.Write(ChatText);
            return mPacketWriter;
        }
    }
}
