using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.Network.Msg
{
    public struct Msg_ChatToAll : MsgBase
    {
        public int Id { get { return MsgId.ChatToAll; } }

        public String chatMsg { get; set; }

        public void W(NetOutgoingMessage om)
        {
            om.Write(chatMsg);
        }

        public void R(NetIncomingMessage im)
        {
            chatMsg = im.ReadString();
        }
    }
}
