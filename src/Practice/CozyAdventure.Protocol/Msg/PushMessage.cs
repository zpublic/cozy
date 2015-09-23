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

        public List<Follower> FollowerList { get; set; }

        public PushMessage()
        {
            FollowerList = new List<Follower>();
        }

        public override void Write(NetBuffer om)
        {
            base.Write(om);
            om.Write(FollowerList.Count);
            foreach(var obj in FollowerList)
            {
                om.WriteAllProperties(obj);
            }
        }

        public override void Read(NetBuffer im)
        {
            base.Read(im);
            int c = im.ReadInt32();
            for(int i = 0; i < c; ++i)
            {
                var follower = new Follower();
                im.ReadAllProperties(follower);
                FollowerList.Add(follower);
            }
        }
    }
}
