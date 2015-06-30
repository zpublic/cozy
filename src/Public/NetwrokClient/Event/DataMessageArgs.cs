using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkHelper.Messages;

namespace NetwrokClient.Event
{
    public class DataMessageArgs : EventArgs
    {
        IMessage Message { get; set; }
        public DataMessageArgs(IMessage msg)
        {
            Message = msg;
        }
    }
}
