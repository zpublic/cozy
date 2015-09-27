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

        public int Money { get; set; }

        public int Exp { get; set; }

        public int Item { get; set; }

        public override void Read(NetBuffer im)
        {
            im.Position = 0;
            base.Read(im);
            //im.ReadUInt32();
            //Money = im.ReadInt32();
            //Exp = im.ReadInt32();
            //Item = im.ReadInt32();
        }

        public override void Write(NetBuffer om)
        {
            om.Position = 0;
            base.Write(om);
            //om.Write(Id);
            //om.Write(Money);
            //om.Write(Exp);
            //om.Write(Item);
        }
    }
}
