using Lidgren.Network;
using NetworkProtocol;

namespace CozyAnywhere.Protocol.Messages
{
    public struct FileDeleteMessage : IMessage
    {
        public uint Id { get { return MessageId.FileDeleteMessage; } }

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