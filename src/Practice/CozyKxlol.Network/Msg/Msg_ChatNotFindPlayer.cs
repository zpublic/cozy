using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;

namespace CozyKxlol.Network.Msg
{
    public class Msg_ChatNotFindPlayer : MsgBase
    {
        public int Id { get { return MsgId.ChatNotFindPlayer; } }

        public string name { get; set; }

        public void W(NetOutgoingMessage om)
        {
            om.Write(name);
        }

        public void R(NetIncomingMessage im)
        {
            name = im.ReadString();
        }
    }
}
