using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.Network.Msg
{
    public struct Msg_AccountModifyPassword : MsgBase
    {
        public int Id { get { return MsgId.AccountModifyPassword; } }

        public String pass { get; set; }

        public void W(NetOutgoingMessage om)
        {
            om.Write(pass);
        }

        public void R(NetIncomingMessage im)
        {
            pass = im.ReadString();
        }
    }
}
