using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyNetworkProtocol;

namespace CozyAdventure.Protocol.Msg
{
    public class GotoResultMessage : MessageBase
    {
        public const byte ToHome    = 0;
        public const byte ToMap     = 1;

        public override uint Id { get { return (uint)MessageId.Farm.GotoResultMessage; } }

        public byte GoToType { get; set; }

        public string UserData { get; set; }

        public long Exp { get; set; }

        public long Money { get; set; }

        public int Level { get; set; }
    }
}
