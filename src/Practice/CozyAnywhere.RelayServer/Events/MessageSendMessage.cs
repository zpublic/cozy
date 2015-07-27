using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyAnywhere.RelayServerCore.Events
{
    public class MessageSendMessage : EventArgs
    {
        public string From { get; set; }
        public string To { get; set; }
        public uint Id { get; set; }

        public MessageSendMessage(string from, string to, uint id)
        {
            From = from;
            To = to;
            Id = id;
        }
    }
}
