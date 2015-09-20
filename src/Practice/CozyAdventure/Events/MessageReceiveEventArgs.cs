using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyNetworkProtocol;

namespace CozyAdventure.Events
{
    public class MessageReceiveEventArgs : EventArgs
    {
        public uint Id { get; set; }

        public MessageBase Msg { get; set; }

        public MessageReceiveEventArgs(uint id, MessageBase msg)
        {
            Id = id;
            Msg = msg;
        }
    }
}
