using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.Network.Msg
{
    public interface MsgBase
    {
        int Id { get; }
        void W(NetOutgoingMessage om);
        void R(NetIncomingMessage im);
    }
}
