using CozyAnywhere.Protocol;
using CozyAnywhere.Protocol.Messages;
using NetworkHelper;
using NetworkProtocol;

namespace CozyAnywhere.ServerCore
{
    public partial class AnywhereServer
    {
        public void InitServerMessage()
        {
            MessageReader.RegisterType<FileEnumMessage>(MessageId.FileEnumMessage);
            MessageReader.RegisterType<FileDeleteMessage>(MessageId.FileDeleteMessage);
            MessageReader.RegisterType<CozyAnywhere.Protocol.Messages.CommandMessage>(MessageId.CommandMessage);
        }

        public void OnFileEnumMessage(IMessage msg)
        {
            var enumMsg = (FileEnumMessage)msg;

            // TODO
        }

        public void OnFileDeleteMessage(IMessage msg)
        {
            var enumMsg = (FileDeleteMessage)msg;

            // TODO
        }

        public void OnCommandMessage(IMessage msg)
        {
            var comm = (CozyAnywhere.Protocol.Messages.CommandMessage)msg;

            if (comm.Command != null)
            {
                ServerPluginMgr.ParsePluginCommand(comm.Command);
            }
        }
    }
}