using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;

namespace NetworkProtocol
{
    public interface IMessage
    {
        uint Id { get; }

        void Write(NetOutgoingMessage om);
        void Read(NetIncomingMessage im);
    }
}
