using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyNetworkProtocol;

namespace CozyAdventure.Protocol.Msg
{
    public class RegisterMessage : MessageBase
    {
        public override uint Id { get { return (uint)MessageId.Inner.RegisterMessage; } }

        public string Name { get; set; }

        public string Pass { get; set; }
    }
}
