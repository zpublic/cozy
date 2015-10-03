using CozyNetworkProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;

namespace CozyAdventure.Protocol.Msg
{
    public class HireResultMessage : MessageBase
    {
        public override uint Id { get { return (uint)MessageId.Mercenary.HireResultMessage; } }

        public string Result { get; set; }

        public List<KeyValuePair<int, int>> Followers { get; set; } = new List<KeyValuePair<int, int>>();

        public override void Read(NetBuffer im)
        {
            base.Read(im);
            int c = im.ReadInt32();
            for(int i = 0; i < c; ++i)
            {
                int objId = im.ReadInt32();
                int folId = im.ReadInt32();
                Followers.Add(new KeyValuePair<int, int>(objId, folId));
            }
        }

        public override void Write(NetBuffer om)
        {
            base.Write(om);
            om.Write(Followers.Count);
            foreach(var obj in Followers)
            {
                om.Write(obj.Key);
                om.Write(obj.Value);
            }
        }
    }
}
