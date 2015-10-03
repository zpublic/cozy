using CozyNetworkProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyAdventure.Protocol.Msg
{
    public class FightMessage : MessageBase
    {
        public const byte GoToFight = 0;
        public const byte GoToRest  = 1;

        public override uint Id { get { return (uint)MessageId.Mercenary.FightMessage; } }

        public byte FightType { get; set; } 

        public int PlayerId { get; set; }

        public int ObjectId { get; set; }
    }
}
