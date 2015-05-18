using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;

namespace CozyKxlol.Network.Msg
{
    public class Msg_AgarNpc : MsgBase
    {
        public int Id { get { return MsgId.AgarNpc; } }

        public int X_Pos { get; set; }
        public int Y_Pos { get; set; }

        public void W(NetOutgoingMessage om)
        {
            om.Write(X_Pos);
            om.Write(Y_Pos);
        }

        public void R(NetIncomingMessage im)
        {
            X_Pos = im.ReadInt32();
            Y_Pos = im.ReadInt32();
        }
    }
}
