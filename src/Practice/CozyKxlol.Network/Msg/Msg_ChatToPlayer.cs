using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;

namespace CozyKxlol.Network.Msg
{
    public class Msg_ChatToPlayer : MsgBase
    {
        public int Id { get { return MsgId.ChatToPlayer; } }

        public string name { get; set; }
        public string chatMsg { get; set; }

        public void W(NetOutgoingMessage om)
        {
            om.Write(name);
            om.Write(chatMsg);
        }

        public void R(NetIncomingMessage im)
        {
            name    = im.ReadString();
            chatMsg = im.ReadString();
        }
    }
}
