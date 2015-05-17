using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.Network.Msg
{
    public class Msg_AccountModifyPassword : MsgBase
    {
        public Msg_AccountModifyPassword() : base(MsgId.AccountModifyPassword) { }

        public String pass { get; set; }

        public override void W(NetOutgoingMessage om)
        {
            base.W(om);
            om.Write(pass);
        }

        public override void R(NetIncomingMessage im)
        {
            base.R(im);
            pass = im.ReadString();
        }
    }
}
