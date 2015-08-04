using Lidgren.Network;
using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;

namespace NetworkProtocol
{
    public class PacketNetBuffer
    {
        public NetConnection SendConnection { get; private set; }

        public int Size { get; set; }

        public int TargetSize { get; private set; }

        public bool IsComplete { get { return TargetSize == Size; } }

        public List<PacketMessage> PacketList { get; set; }

        public void Add(PacketMessage msg)
        {
            PacketList.Add(msg);
            ++Size;
        }

        public PacketNetBuffer(int targetSize, NetConnection conn)
        {
            TargetSize      = targetSize;
            SendConnection  = conn;
            PacketList = new List<PacketMessage>();
        }

        public NetBuffer ToBuffer()
        {
            if(IsComplete)
            {
                var Buffer  = new NetBuffer();
                var t       = from p in PacketList orderby p.Number select p;
                foreach(var obj in t)
                {
                    Buffer.Write(obj.Bytes);
                }
                return Buffer;
            }
            return null;
        }
    }
}
