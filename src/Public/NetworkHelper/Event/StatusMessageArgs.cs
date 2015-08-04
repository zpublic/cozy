using System;
using Lidgren.Network;

namespace NetworkHelper.Event
{
    public class StatusMessageArgs : EventArgs
    {
        public NetConnectionStatus Status { get; set; }

        public String Reason { get; set; }

        public NetConnection Connection { get; set; }

        public StatusMessageArgs(NetConnectionStatus status, String reason, NetConnection conn)
        {
            Status = status;
            Reason = reason;
            Connection = conn;
        }
    }
}