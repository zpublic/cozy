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
        public override uint Id { get { return (uint)MessageId.Farm.GotoResultMessage; } }

        public string Result { get; set; }
    }
}
