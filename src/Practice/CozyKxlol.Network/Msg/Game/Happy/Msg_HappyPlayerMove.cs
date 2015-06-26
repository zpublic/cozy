using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;

namespace CozyKxlol.Network.Msg.Happy
{
    public struct Msg_HappyPlayerMove : MsgBase
    {
        public int Id { get { return MsgId.HappyPlayerMove; } }

        public void W(NetOutgoingMessage om)
        {

        }

        public void R(NetIncomingMessage im)
        {

        }
    }
}
