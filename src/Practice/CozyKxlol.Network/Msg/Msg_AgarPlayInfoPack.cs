using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;

namespace CozyKxlol.Network.Msg
{
    public struct Msg_AgarPlayInfoPack : MsgBase
    {
        public int Id { get { return MsgId.AgarPlayInfoPack; } }

        private List<Tuple<uint, float, float, float, uint>> _PLayerList;
        public List<Tuple<uint, float, float, float, uint>> PLayerList
        {
            get
            {
                return _PLayerList = _PLayerList ?? new List<Tuple<uint, float, float, float, uint>>();
            }
            set
            {
                _PLayerList = value;
            }
        }

        public void W(NetOutgoingMessage om)
        {
            om.Write(PLayerList.Count);
            foreach (var obj in PLayerList)
            {
                om.Write(obj.Item1);
                om.Write(obj.Item2);
                om.Write(obj.Item3);
                om.Write(obj.Item4);
                om.Write(obj.Item5);
            }
        }

        public void R(NetIncomingMessage im)
        {
            int Count = im.ReadInt32();
            for (int i = 0; i < Count; ++i)
            {
                uint uid = im.ReadUInt32();
                float x = im.ReadFloat();
                float y = im.ReadFloat();
                float r = im.ReadFloat();
                uint color = im.ReadUInt32();
                PLayerList.Add(Tuple.Create<uint, float, float, float, uint>(uid, x, y, r, color));
            }
        }
    }
}
