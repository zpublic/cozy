using Lidgren.Network;
using System;

namespace NetworkHelper.Event
{
    public class DataMessageArgs : EventArgs
    {
        public NetBuffer Input { get; set; }

        public uint MessageId { get; set; }

        public NetConnection Connection { get; set; }

        public DataMessageArgs(NetBuffer input, uint id, NetConnection conn)
        {
            Input = input;
            MessageId = id;
            Connection = conn;
        }
    }
}