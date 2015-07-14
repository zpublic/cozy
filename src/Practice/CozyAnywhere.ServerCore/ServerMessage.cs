using CozyAnywhere.Protocol;
using CozyAnywhere.Protocol.Messages;
using NetworkHelper;
using NetworkProtocol;
using CozyAnywhere.Plugin.WinFile;
using CozyAnywhere.Plugin.WinProcess;
using CozyAnywhere.Plugin.WinKeyboard;
using CozyAnywhere.Plugin.WinMouse;
using CozyAnywhere.PluginBase;

namespace CozyAnywhere.ServerCore
{
    public partial class AnywhereServer
    {
        public void InitServerMessage()
        {
            MessageReader.RegisterType<CozyAnywhere.Protocol.Messages.CommandMessage>(MessageId.CommandMessage);
            MessageReader.RegisterType<PluginLoadMessage>(MessageId.PluginLoadMessage);
        }

        public void OnCommandMessage(IMessage msg)
        {
            var comm = (CozyAnywhere.Protocol.Messages.CommandMessage)msg;

            if (comm.Command != null)
            {
                var result = ServerPluginMgr.ParsePluginCommand(comm.Command);

                if(result != null)
                {
                    var rspMsg = new CommandMessageRsp()
                    {
                        PluginName = result.PluginName,
                        MethodName = result.MethodName,
                        CommandRsp = result.MethodReturnValue,
                    };
                    client.SendMessage(rspMsg);
                }
            }
        }

        public void OnPluginLoadMessage(IMessage msg)
        {
            var loadMsg = (PluginLoadMessage)msg;
            switch(loadMsg.PluginName)
            {
                case "FilePlugin":
                    ServerPluginMgr.AddPlugin(new FilePlugin());
                    break;
                case "ProcessPlugin":
                    ServerPluginMgr.AddPlugin(new ProcessPlugin());
                    break;
                case "MousePlugin":
                    ServerPluginMgr.AddPlugin(new MousePlugin());
                    break;
                case "KeyboardPlugin":
                    ServerPluginMgr.AddPlugin(new KeyboardPlugin());
                    break;
                default:
                    break;
            }
            var rspMsg = new PluginQueryMessage()
            {
                Plugins = ServerPluginMgr.AllPluginName(),
            };
            client.SendMessage(rspMsg);
        }
    }
}