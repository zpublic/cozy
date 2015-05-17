using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.Network.Msg
{
    public class Msg_AccountModifyPasswordRsp : MsgBase
    {
        public Msg_AccountModifyPasswordRsp() : base(MsgId.AccountModifyPasswordRsp) { }

        public Boolean suc { get; set; }
        public String detail { get; set; }

        public override void W(NetOutgoingMessage om)
        {
            base.W(om);
            om.Write(suc);
            om.Write(detail);
        }

        public override void R(NetIncomingMessage im)
        {
            base.R(im);
            suc = im.ReadBoolean();
            detail = im.ReadString();
        }
    }
}
