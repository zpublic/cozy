using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.Network.Msg
{
    public class Msg_ChatToAll : MsgBase
    {
        public Msg_ChatToAll() : base(MsgId.ChatToAll) { }

        public String chatMsg { get; set; }

        public override void W(NetOutgoingMessage om)
        {
            base.W(om);
            om.Write(chatMsg);
        }

        public override void R(NetIncomingMessage im)
        {
            base.R(im);
            chatMsg = im.ReadString();
        }
    }
}
