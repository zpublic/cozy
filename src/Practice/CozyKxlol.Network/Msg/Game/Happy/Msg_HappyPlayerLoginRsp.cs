using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;

namespace CozyKxlol.Network.Msg.Happy
{
    public struct Msg_HappyPlayerLoginRsp : MsgBase
    {
        public int Id { get { return MsgId.HappyPlayerLoginRsp; } }

        public void W(NetOutgoingMessage om)
        {

        }

        public void R(NetIncomingMessage im)
        {

        }
    }
}
