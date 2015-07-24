using System;

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