using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;

namespace NetworkHelper.Event
{
    public class StatusMessageArgs : EventArgs
    {
        public NetConnectionStatus Status { get; set; }
        public String Reason { get; set; }
        public StatusMessageArgs(NetConnectionStatus status, String reason)
        {
            Status = status;
            Reason = reason;
        }
    }
}
