using Lidgren.Network;
using System;

namespace NetworkHelper.Event
{
    public class DataMessageArgs : EventArgs
    {
        public NetIncomingMessage Input { get; set; }

        public DataMessageArgs(NetIncomingMessage input)
        {
            Input = input;
        }
    }
}