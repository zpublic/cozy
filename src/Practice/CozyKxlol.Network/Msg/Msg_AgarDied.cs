using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;

namespace CozyKxlol.Network.Msg
{
    public class Msg_AgarDied : MsgBase
    {
        public int Id { get { return MsgId.AgarDied; } }

        public void W(NetOutgoingMessage om)
        {

        }

        public void R(NetIncomingMessage im)
        {

        }
    }
}
