using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyNetworkProtocol;
using CozyAdventure.Model;
using Lidgren.Network;

namespace CozyAdventure.Protocol.Msg
{
    public class PushMessage : MessageBase
    {
        public override uint Id { get { return (uint)MessageId.User.PushMessage; } }

        public int Exp { get; set; }

        public int Money { get; set; }

        public List<int> FollowerList { get; set; }

        public List<int> FightFollowerList { get; set; }

        public PushMessage()
        {
            FollowerList        = new List<int>();
            FightFollowerList   = new List<int>();
        }

        public override void Write(NetBuffer om)
        {
            base.Write(om);
            om.Write(FollowerList.Count);
            foreach(var obj in FollowerList)
            {
                om.Write(obj);
            }
            om.Write(FightFollowerList.Count);
            foreach (var obj in FightFollowerList)
            {
                om.Write(obj);
            }
        }

        public override void Read(NetBuffer im)
        {
            base.Read(im);
            int c = im.ReadInt32();
            for(int i = 0; i < c; ++i)
            {
                var follower = im.ReadInt32();
                FollowerList.Add(follower);
            }
            c = im.ReadInt32();
            for (int i = 0; i < c; ++i)
            {
                var follower = im.ReadInt32();
                FightFollowerList.Add(follower);
            }
        }
    }
}
