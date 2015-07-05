using Lidgren.Network;
using System;
using System.Collections.Generic;
using NetworkProtocol;

namespace CozyAnywhere.Protocol.Messages
{
    public struct ProcessEnumMessageRsp : IMessage
    {
        public uint Id { get { return MessageId.ProcessEnumMessageRsp; } }

        public List<Tuple<uint, string>> ProcessList;

        public void Write(NetOutgoingMessage om)
        {
            if (ProcessList != null)
            {
                om.Write(ProcessList.Count);
                foreach (var obj in ProcessList)
                {
                    om.Write(obj.Item1);
                    om.Write(obj.Item2);
                }
            }
        }

        public void Read(NetIncomingMessage im)
        {
            if (ProcessList == null)
            {
                ProcessList = new List<Tuple<uint, string>>();
            }
            int count = im.ReadInt32();
            for (int i = 0; i < count; ++i)
            {
                uint pid    = im.ReadUInt32();
                string name = im.ReadString();
                ProcessList.Add(Tuple.Create<uint, string>(pid, name));
            }
        }
    }
}