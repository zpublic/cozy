using Lidgren.Network;
using NetworkProtocol;

namespace CozyAnywhere.Protocol.Messages
{
    public class CommandMessage : IMessage
    {
        public uint Id { get { return MessageId.CommandMessage; } }

        public string Command { get; set; }

        public void Write(NetOutgoingMessage om)
        {
            om.Write(Command);
        }

        public void Read(NetIncomingMessage im)
        {
            Command = im.ReadString();
        }
    }
}