using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkHelper.Messages;
using Lidgren.Network;

namespace CozyAnywhere.Protocol.Messages
{
    public struct FileEnumMessageRsp : IMessage
    {
        public uint Id { get { return MessageId.FileEnumMessageRsp; } }

        public List<Tuple<string, uint, bool>> FileInfoList { get; set; }

        public void Write(NetOutgoingMessage om)
        {
            if(FileInfoList != null)
            {
                om.Write(FileInfoList.Count);
                foreach(var obj in FileInfoList)
                {
                    om.Write(obj.Item1);
                    om.Write(obj.Item2);
                    om.Write(obj.Item3);
                }
            }
        }

        public void Read(NetIncomingMessage im)
        {
            if(FileInfoList == null)
            {
                FileInfoList = new List<Tuple<string, uint, bool>>();
            }
            int ListCount = im.ReadInt32();
            for(int i = 0; i < ListCount; ++i)
            {
                string Name = im.ReadString();
                uint Size   = im.ReadUInt32();
                bool IsDire = im.ReadBoolean();
                FileInfoList.Add(Tuple.Create<string, uint, bool>(Name, Size, IsDire));
            }
        }
    }
}
