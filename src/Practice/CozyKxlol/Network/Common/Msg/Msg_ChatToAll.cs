using Microsoft.Xna.Framework.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.Network.Common.Msg
{
    public class Msg_ChatToAll : MsgBase
    {
        public Msg_ChatToAll()
            : base(MsgId.ChatToAll)
        {
        }

        public String ChatText
        {
            set;
            get;
        }

        override public PacketWriter GetPacket()
        {
            mPacketWriter.Write(ChatText);
            return mPacketWriter;
        }
    }
}
