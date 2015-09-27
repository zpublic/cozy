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

        public override void Read(NetBuffer im)
        {
            base.Read(im);
            //im.Position = 0;
            //im.ReadUInt32();

            //Level = im.ReadInt32();
            //Exp = im.ReadInt32();
            //Money = im.ReadInt32();
        }

        public override void Write(NetBuffer om)
        {
            base.Write(om);
            //om.Position = 0;
            //om.Write(Id);
            //om.Write(Level);
            //om.Write(Exp);
            //om.Write(Money);
        }
    }
}
