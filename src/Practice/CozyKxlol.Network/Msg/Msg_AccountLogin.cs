using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.Network.Msg
{
    public struct Msg_AccountLogin : MsgBase
    {
        public int Id { get { return MsgId.AccountLogin; } }

        public String name { get; set; }
        public String pass { get; set; }

        public void W(NetOutgoingMessage om)
        {
            om.Write(name);
            om.Write(pass);
        }

        public void R(NetIncomingMessage im)
        {
            name = im.ReadString();
            pass = im.ReadString();
        }
    }
}
