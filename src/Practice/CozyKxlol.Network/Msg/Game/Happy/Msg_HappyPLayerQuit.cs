using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;

namespace CozyKxlol.Network.Msg.Game.Happy
{
    public struct Msg_HappyPLayerQuit : MsgBase
    {
        public int Id { get { return MsgId.HappyPLayerQuit; } }

        public void W(NetOutgoingMessage om)
        {

        }

        public void R(NetIncomingMessage im)
        {

        }
    }
}
