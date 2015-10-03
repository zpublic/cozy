using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyNetworkProtocol;

namespace CozyAdventure.Protocol.Msg
{
    public class FightResultMessage : MessageBase
    {
        public override uint Id { get { return (uint)MessageId.Mercenary.FightResultMessage; } }

        public string Result { get; set; }

        public byte StatusNow { get; set; }

        public int ObjectId { get; set; }
    }
}
