using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyAdventure.Protocol.Msg
{
    public class UpgradeMessage : IMessage
    {
        public uint Id { get { return (uint)MessageId.Mercenary.UpgradeMessage; } }
    }
}
