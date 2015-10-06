using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyNetworkProtocol;
using Lidgren.Network;

namespace CozyAdventure.Protocol.Msg
{
    public class HireFollowerMessage : MessageBase
    {
        public override uint Id { get { return (uint)MessageId.Mercenary.HireFollowerMessage; } }

        public int PlayerId { get; set; }

        public List<int> FollowerId { get; set; } = new List<int>();

        public override void Read(NetBuffer im)
        {
            base.Read(im);
            int c = im.ReadInt32();
            for(int i = 0; i < c; ++i)
            {
                int id = im.ReadInt32();
                FollowerId.Add(id);
            }
        }

        public override void Write(NetBuffer om)
        {
            base.Write(om);
            om.Write(FollowerId.Count);
            foreach(var obj in FollowerId)
            {
                om.Write(obj);
            }
        }
    }
}
