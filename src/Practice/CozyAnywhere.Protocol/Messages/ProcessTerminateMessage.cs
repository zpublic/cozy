using Lidgren.Network;
using NetworkProtocol;

namespace CozyAnywhere.Protocol.Messages
{
    public struct ProcessTerminateMessage : IMessage
    {
        public uint Id { get { return MessageId.ProcessTerminateMessage; } }

        public uint ProcessId { get; set; }

        public void Write(NetOutgoingMessage om)
        {
            om.Write(ProcessId);
        }

        public void Read(NetIncomingMessage im)
        {
            ProcessId = im.ReadUInt32();
        }
    }
}