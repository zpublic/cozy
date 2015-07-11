using Lidgren.Network;
using NetworkProtocol;

namespace CozyAnywhere.Protocol.Messages
{
    public class CommandMessageRsp : IMessage
    {
        public uint Id { get { return MessageId.CommandMessageRsp; } }

        public string PluginName { get; set; }
        public string MethodName { get; set; }
        public string CommandRsp { get; set; }

        public void Write(NetOutgoingMessage om)
        {
            om.Write(PluginName);
            om.Write(MethodName);
            om.Write(CommandRsp);
        }

        public void Read(NetIncomingMessage im)
        {
            PluginName = im.ReadString();
            MethodName = im.ReadString();
            CommandRsp = im.ReadString();
        }
    }
}