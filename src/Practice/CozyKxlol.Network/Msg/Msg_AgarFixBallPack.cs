using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;

namespace CozyKxlol.Network.Msg
{
    public struct Msg_AgarFixBallPack : MsgBase
    {
        public int Id { get { return Msg.MsgId.AgarFixBallPack; } }

        private List<Tuple<uint, float, float, uint>> _FixedList;
        public List<Tuple<uint, float, float, uint>> FixedList
        {
            get
            {
                return _FixedList = _FixedList ?? new List<Tuple<uint, float, float, uint>>();
            }
            set
            {
                _FixedList = value;
            }
        }

        public void W(NetOutgoingMessage om)
        {
            om.Write(FixedList.Count);
            foreach(var obj in FixedList)
            {
                om.Write(obj.Item1);
                om.Write(obj.Item2);
                om.Write(obj.Item3);
                om.Write(obj.Item4);
            }
        }

        public void R(NetIncomingMessage im)
        {
            int Count = im.ReadInt32();
            for(int i = 0; i < Count; ++i)
            {
                uint uid = im.ReadUInt32();
                float x = im.ReadFloat();
                float y = im.ReadFloat();
                uint color = im.ReadUInt32();
                FixedList.Add(Tuple.Create<uint, float, float, uint>(uid, x, y, color));
            }
        }
    }
}
