using Lidgren.Network;
using NetworkProtocol;

namespace CozyAnywhere.Protocol.Messages
{
    public struct FileEnumMessage : IMessage
    {
        public uint Id { get { return MessageId.FileEnumMessage; } }

        public string Path { get; set; }

        public void Write(NetOutgoingMessage om)
        {
            om.Write(Path);
        }

        public void Read(NetIncomingMessage im)
        {
            Path = im.ReadString();
        }
    }
}