using CozyAnywhere.Protocol.Messages;
using NetworkHelper;
using CozyAnywhere.PluginBase;
using NetworkProtocol;
using System;
using Lidgren.Network;

namespace CozyAnywhere.ServerCore
{
    public partial class AnywhereServer
    {
        public void InitServerMessage()
        {
            var asm = "CozyAnywhere.Protocol";
            var ns  = "CozyAnywhere.Protocol.Messages";
            MessageReader.RegisterTypeWithAssembly(asm, ns);
            MessageCallbackInvoker.LoadMessage(asm, ns);
            RegisterCallback();
        }

        private void RegisterCallback()
        {
            MessageCallbackInvoker.RegisterCallback<CommandMessage>(new Action<IMessage, NetConnection>(OnCommandMessage));
            MessageCallbackInvoker.RegisterCallback<ConnectMessage>(new Action<IMessage, NetConnection>(OnConnectMessage));
            MessageCallbackInvoker.RegisterCallback<PluginLoadMessage>(new Action<IMessage, NetConnection>(OnPluginLoadMessage));
            MessageCallbackInvoker.RegisterCallback<QueryConnectMessage>(new Action<IMessage, NetConnection>(OnConnectQueryMessage));
            MessageCallbackInvoker.RegisterCallback<QueryConnectMessageRsp>(new Action<IMessage, NetConnection>(OnConnectQueryMessageRsp));
        }

        public void OnCommandMessage(IMessage msg, NetConnection conn)
        {
            var comm = (CommandMessage)msg;

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

        public void OnPluginLoadMessage(IMessage msg, NetConnection conn)
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

        private void OnConnectQueryMessage(IMessage msg, NetConnection conn)
        {
            var queryMsg = (QueryConnectMessage)msg;
            var rspMsg = new QueryConnectMessageRsp()
            {
                ConnectionType = QueryConnectMessageRsp.ClientType,
            };
            client.SendMessage(rspMsg);
        }

        private void OnConnectQueryMessageRsp(IMessage msg, NetConnection conn)
        {
            var rspMsg = (QueryConnectMessageRsp)msg;
            if (rspMsg.ConnectionType == QueryConnectMessageRsp.ClientType)
            {
                client.DisConnect();
            }
        }

        private void OnConnectMessage(IMessage msg, NetConnection conn)
        {
            var connMsg = (ConnectMessage)msg;
        }
    }
}