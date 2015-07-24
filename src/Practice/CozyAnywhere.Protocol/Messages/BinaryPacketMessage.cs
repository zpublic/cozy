using Lidgren.Network;
using NetworkProtocol;

namespace CozyAnywhere.Protocol.Messages
{
    public struct BinaryPacketMessage : IMessage
    {
        public uint Id { get { return MessageId.BinaryPacketMessage; } }

        public string MetaData { get; set; }

        public byte[] Data { get; set; }

        public void Write(NetOutgoingMessage om)
        {
            if (MetaData == null) MetaData = "Null";
            om.Write(MetaData);
            om.Write(Data.Length);
            om.Write(Data);
        }

        public void Read(NetIncomingMessage im)
        {
            MetaData = im.ReadString();
            int l = im.ReadInt32();
            Data = im.ReadBytes(l);
        }
    }
}