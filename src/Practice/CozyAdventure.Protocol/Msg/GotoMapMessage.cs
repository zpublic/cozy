using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyNetworkProtocol;
using Lidgren.Network;

namespace CozyAdventure.Protocol.Msg
{
    public class GotoMapMessage : MessageBase
    {
        public override uint Id { get { return (uint)MessageId.Farm.GotoMapMessage; } }

        public int Level { get; set; }

        public int Exp { get; set; }

        public int Money { get; set; }
    }
}
