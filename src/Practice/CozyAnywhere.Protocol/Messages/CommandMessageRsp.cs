using Lidgren.Network;
using NetworkProtocol;

namespace CozyAnywhere.Protocol.Messages
{
    public class CommandMessageRsp : IMessage
    {
        public const byte NoDataType            = 0;
        public const byte StringDataType        = 1;
        public const byte BinaryDataType        = 2;
        public const byte PacketBinaryDataType  = 3;

        public uint Id { get { return MessageId.CommandMessageRsp; } }

        public string PluginName { get; set; }

        public string MethodName { get; set; }

        public byte RspType { get; set; }

        public string StringCommandRsp { get; set; }

        public byte[] BinaryCommandRsp { get; set; }

        public void Write(NetBuffer om)
        {
            om.Write(PluginName);
            om.Write(MethodName);
            om.Write(RspType);
            if (RspType == StringDataType)
            {
                om.Write(StringCommandRsp);
            }
            else if (RspType == BinaryDataType)
            {
                om.Write(BinaryCommandRsp.Length);
                om.Write(BinaryCommandRsp);
            }
        }

        public void Read(NetBuffer im)
        {
            PluginName  = im.ReadString();
            MethodName  = im.ReadString();
            RspType     = im.ReadByte();
            if (RspType == StringDataType)
            {
                StringCommandRsp = im.ReadString();
            }
            else if (RspType == BinaryDataType)
            {
                int l = im.ReadInt32();
                BinaryCommandRsp = im.ReadBytes(l);
            }
        }
    }
}