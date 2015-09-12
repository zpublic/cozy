using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyAdventure.Protocol.Msg
{
    public class RegisterResultMessage : MessageBase
    {
        public override uint Id { get { return (uint)MessageId.Inner.RegisterResultMessage; } }
    }
}
