using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;

namespace CozyKxlol.Network.Msg.Agar
{
    public struct Msg_AgarPlayInfoPack : MsgBase
    {
        public int Id { get { return MsgId.AgarPlayInfoPack; } }

        private List<Tuple<uint, float, float, int, uint, string>> _PLayerList;
        public List<Tuple<uint, float, float, int, uint, string>> PLayerList
        {
            get
            {
                return _PLayerList = _PLayerList ?? new List<Tuple<uint, float, float, int, uint, string>>();
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
                om.Write(obj.Item6);
            }
        }

        public void R(NetIncomingMessage im)
        {
            int Count = im.ReadInt32();
            for (int i = 0; i < Count; ++i)
            {
                uint uid    = im.ReadUInt32();
                float x     = im.ReadFloat();
                float y     = im.ReadFloat();
                int r     = im.ReadInt32();
                uint color  = im.ReadUInt32();
                string name = im.ReadString();
                PLayerList.Add(Tuple.Create<uint, float, float, int, uint, string>(uid, x, y, r, color, name));
            }
        }
    }
}
