using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.Network.Msg
{
    public class MsgBase
    {
        public readonly int Id;
        public MsgBase(int id = MsgId.Zero)
        {
            Id = id;
        }

        public virtual void W(NetOutgoingMessage om)
        {
            om.Write(Id);
        }

        public virtual void R(NetIncomingMessage im)
        {
            im.ReadInt32(Id);
        }
    }
}
