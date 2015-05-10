using Microsoft.Xna.Framework.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.Network.Msg
{
    public class MsgBase
    {
        public MsgBase(int id = 0)
        {
            mPacketWriter.Write(id);
        }

        protected PacketWriter mPacketWriter = new PacketWriter();
        virtual public PacketWriter GetPacket()
        {
            return mPacketWriter;
        }
    }
}
