using Lidgren.Network;
using NetworkProtocol;

namespace CozyAnywhere.Protocol.Messages
{
    public class CommandMessageRsp : IMessage
    {
        public static readonly string NoReturnValue = "NoReturnValue";

        public uint Id { get { return MessageId.CommandMessageRsp; } }

        public string PluginName { get; set; }
        public string MethodName { get; set; }
        public string CommandRsp { get; set; }

        public void Write(NetOutgoingMessage om)
        {
            om.Write(PluginName);
            om.Write(MethodName);
            if (CommandRsp != null)
            {
                om.Write(CommandRsp);
            }
            else
            {
                om.Write(NoReturnValue);
            }
        }

        public void Read(NetIncomingMessage im)
        {
            PluginName = im.ReadString();
            MethodName = im.ReadString();
            CommandRsp = im.ReadString();
        }
    }
}