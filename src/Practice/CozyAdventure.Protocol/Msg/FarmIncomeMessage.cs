using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyNetworkProtocol;
using Lidgren.Network;

namespace CozyAdventure.Protocol.Msg
{
    public class FarmIncomeMessage : MessageBase
    {
        public override uint Id { get { return (uint)MessageId.Farm.FarmIncomeMessage; } }

        public long Money { get; set; }

        public long Exp { get; set; }

        public int Item { get; set; }
    }
}
