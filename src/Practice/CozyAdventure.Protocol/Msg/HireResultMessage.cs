using CozyNetworkProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyAdventure.Protocol.Msg
{
    public class HireResultMessage : MessageBase
    {
        public override uint Id { get { return (uint)MessageId.Mercenary.HireResultMessage; } }

        public string Result { get; set; }
    }
}
