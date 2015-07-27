using CozyAnywhere.Protocol.Messages;
using NetworkHelper;
using CozyAnywhere.PluginBase;
using NetworkProtocol;

namespace CozyAnywhere.ServerCore
{
    public partial class AnywhereServer
    {
        public void InitServerMessage()
        {
            MessageReader.RegisterTypeWithAssembly("CozyAnywhere.Protocol", "CozyAnywhere.Protocol.Messages");
        }

        public void OnCommandMessage(IMessage msg)
        {
            var comm = (CozyAnywhere.Protocol.Messages.CommandMessage)msg;

            if (comm.Command != null)
            {
                var result = ServerPluginMgr.ParsePluginCommand(comm.Command);

                if (result != null)
                {
                    var rspMsg = new CommandMessageRsp()
                    {
                        PluginName  = result.PluginName,
                        MethodName  = result.MethodName,
                        RspType     = result.MethodReturnValue.DataType,
                    };

                    if (rspMsg.RspType == PluginMethodReturnValueType.StringDataType)
                    {
                        rspMsg.StringCommandRsp = result.MethodReturnValue.Data as string;
                    }
                    else if (rspMsg.RspType == PluginMethodReturnValueType.BinaryDataType)
                    {
                        rspMsg.BinaryCommandRsp = result.MethodReturnValue.Data as byte[];
                    }
                    else if (rspMsg.RspType == PluginMethodReturnValueType.PacketBinaryDataType)
                    {
                        var data = result.MethodReturnValue.Data as PluginMehtodReturnValuePacket;
                        if (data != null)
                        {
                            foreach (var obj in data.Packet)
                            {
                                client.SendMessage(new BinaryPacketMessage()
                                {
                                    Data        = obj.Data,
                                    MetaData    = obj.MetaData,
                                });
                            }
                        }
                        return;
                    }
                    client.SendMessage(rspMsg);
                }
            }
        }

        public void OnPluginLoadMessage(IMessage msg)
        {
            var loadMsg = (PluginLoadMessage)msg;

            var list    = EnumPluginFolder();
            ServerPluginMgr.AddPluginsWithFileNames(list);

            var rspMsg = new PluginQueryMessage()
            {
                Plugins = ServerPluginMgr.AllPluginName(),
            };
            client.SendMessage(rspMsg);
        }

        private void OnConnectQueryMessage(IMessage msg)
        {
            var queryMsg = (QueryConnectMessage)msg;
            var rspMsg = new QueryConnectMessageRsp()
            {
                ConnectionType = QueryConnectMessageRsp.ClientType,
            };
            client.SendMessage(rspMsg);
        }

        private void OnConnectQueryMessageRsp(IMessage msg)
        {
            var rspMsg = (QueryConnectMessageRsp)msg;
            if (rspMsg.ConnectionType == QueryConnectMessageRsp.ClientType)
            {
                client.DisConnect();
            }
        }

        private void OnConnectMessage(IMessage msg)
        {
            var connMsg = (ConnectMessage)msg;
        }
    }
}