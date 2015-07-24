using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;

namespace CozyKxlol.Network.Msg.Agar
{
    public struct Msg_AgarMarkListPack : MsgBase
    {
        public int Id { get { return MsgId.AgarMarkListPark; } }


        private List<KeyValuePair<uint, int>> _MarkList; 
        public List<KeyValuePair<uint, int>> MarkList
        {
            get
            {
                return _MarkList = _MarkList ?? new List<KeyValuePair<uint, int>>();
            }
            set
            {
                _MarkList = value;
            }
        }

        public void W(NetOutgoingMessage om)
        {
            if(MarkList != null)
            {
                om.Write(MarkList.Count);
                foreach(var obj in MarkList)
                {
                    om.Write(obj.Key);
                    om.Write(obj.Value);
                }
            }
        }

        public void R(NetIncomingMessage im)
        {
            int Count = im.ReadInt32();
            for(int i = 0; i < Count; ++i)
            {
                uint id = im.ReadUInt32();
                int mark = im.ReadInt32();
                MarkList.Add(new KeyValuePair<uint, int>(id, mark));
            }
        }
    }
}
