using System;

namespace NetworkHelper.Event
{
    public class InternalMessageArgs : EventArgs
    {
        public String Msg { get; set; }

        public InternalMessageArgs(String msg)
        {
            Msg = msg;
        }
    }
}