using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;

namespace CozyKxlol.Network.Msg.Happy
{
    public struct Msg_HappyPlayerPack : MsgBase
    {
        public int Id { get { return MsgId.HappyPlayerPack; } }

        public List<Tuple<uint, int, int, bool, uint>> PlayerPack;

        public void W(NetOutgoingMessage om)
        {
            if(PlayerPack != null)
            {
                om.Write(PlayerPack.Count);
                foreach(var obj in PlayerPack)
                {
                    om.Write(obj.Item1);
                    om.Write(obj.Item2);
                    om.Write(obj.Item3);
                    om.Write(obj.Item4);
                    om.Write(obj.Item5);
                }
            }
        }

        public void R(NetIncomingMessage im)
        {
            if(PlayerPack == null)
            {
                PlayerPack = new List<Tuple<uint, int, int, bool, uint>>();
            }
            int l = im.ReadInt32();
            for(int i = 0; i < l; ++i)
            {
                var id      = im.ReadUInt32();
                var x       = im.ReadInt32();
                var y       = im.ReadInt32();
                var alive   = im.ReadBoolean();
                var sid     = im.ReadUInt32();
                PlayerPack.Add(Tuple.Create<uint, int, int, bool, uint>(id, x, y, alive, sid));
            }
        }
    }
}
