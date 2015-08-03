using NetworkHelper;
using NetworkProtocol;
using Lidgren.Network;
using CozyAnywhere.Protocol.Messages;
using System;

namespace CozyAnywhere.RelayServerCore
{
    public partial class AnywhereRelayServer
    {
        private void InitMessage()
        {
            var asm = "CozyAnywhere.Protocol";
            var ns  = "CozyAnywhere.Protocol.Messages";
            MessageReader.RegisterTypeWithAssembly(asm, ns);
            MessageCallbackInvoker.LoadMessage(asm, ns);
            RegisterCallback();
        }

        private void RegisterCallback()
        {
            MessageCallbackInvoker.RegisterCallback<ConnectMessage>(new Action<IMessage, NetConnection>(OnConnectMessage));
            MessageCallbackInvoker.RegisterCallback<QueryConnectMessage>(new Action<IMessage, NetConnection>(OnConnectQueryMessage));
            MessageCallbackInvoker.RegisterCallback<QueryConnectMessageRsp>(new Action<IMessage, NetConnection>(OnConnectQueryMessageRsp));
            MessageCallbackInvoker.DefaultAction = new Action<IMessage, NetConnection>(DispatchMessage);
        }

        private void OnConnectMessage(IMessage msg, NetConnection conn)
        {
            var connMsg = (ConnectMessage)msg;
        }

        private void OnConnectQueryMessage(IMessage msg, NetConnection conn)
        {
            var rspMsg = new QueryConnectMessageRsp()
            {
                ConnectionType = QueryConnectMessageRsp.RelayServerType,
            };
            server.SendMessage(rspMsg, conn);
        }

        private void OnConnectQueryMessageRsp(IMessage msg, NetConnection conn)
        {
            var rspMsg = (QueryConnectMessageRsp)msg;
            if (rspMsg.ConnectionType == QueryConnectMessageRsp.ServerType)
            {
                server.ServerConn = conn;
                ServerConnectMessage(this, new Events.ServerConnectArgs());
            }
            else if(rspMsg.ConnectionType == QueryConnectMessageRsp.ClientType)
            {
                server.ClientConn = conn;
                ClientConnectMessage(this, new Events.ClientConnectArgs());
            }
            else
            {
                // TODO Disconnect the conn
            }
        }

        private void DispatchMessage(IMessage msg, NetConnection conn)
        {
            uint id     = msg.Id;
            string from = null;
            string to   = null;

            if (conn == server.ServerConn)
            {
                server.SendMessageToClient(msg);
                from    = "server";
                to      = "client";
            }
            else if (conn == server.ClientConn)
            {
                server.SendMessageToServer(msg);
                from    = "client";
                to      = "server";
            }

            MessageSendMessage(this, new Events.MessageSendMessage(from, to, id));
        }
    }
}
