using System;
using NetworkHelper.Event;
using NetworkHelper;
using CozyAnywhere.Protocol;
using CozyAnywhere.RelayServerCore.Events;
using CozyAnywhere.Protocol.Messages;
using NetworkProtocol;
using Lidgren.Network;

namespace CozyAnywhere.RelayServerCore
{
    public partial class AnywhereRelayServer
    {
        private void InitEvent()
        {
            server.StatusMessage += new EventHandler<DataMessageArgs>(OnStatusMessage);
            server.DataMessage += new EventHandler<DataMessageArgs>(OnDataMessage);
            server.InternalMessage += new EventHandler<InternalMessageArgs>(OnInternalMessage);
        }

        private void OnStatusMessage(object sender, DataMessageArgs msg)
        {
            var status = (NetworkHelper.NetConnectionStatus)msg.Input.ReadByte();
            string reason = msg.Input.ReadString();
            var queryMsg = new QueryConnectMessage();
            server.SendMessage(queryMsg, msg.Input.SenderConnection);
        }

        private void OnDataMessage(object sender, DataMessageArgs msg)
        {
            var baseMsg = MessageReader.GetTypeInstanceByStream(msg.Input);
            if (server.ServerConn == null || server.ClientConn == null)
            {
                if (baseMsg.Id == MessageId.ConnectMessage)
                {
                    OnConnectMessage(baseMsg, msg.Input.SenderConnection);
                }
                else if (baseMsg.Id == MessageId.QueryConnectMessageRsp)
                {
                    OnConnectQueryMessageRsp(baseMsg, msg.Input.SenderConnection);
                }
                else if (baseMsg.Id == MessageId.QueryConnectMessageRsp)
                {
                    OnConnectQueryMessage(baseMsg, msg.Input.SenderConnection);
                }
            }
            else
            {
                DispatchMessage(baseMsg, msg.Input.SenderConnection);
            }
        }

        private void OnInternalMessage(object sender, InternalMessageArgs msg)
        {
        }

        private void DispatchMessage(IMessage msg, NetConnection conn)
        {
            uint id = msg.Id;
            string from = null;
            string to = null;

            if (conn == server.ServerConn)
            {
                server.SendMessageToClient(msg);
                from = "server";
                to = "client";
            }
            else if (conn == server.ClientConn)
            {
                server.SendMessageToServer(msg);
                from = "client";
                to = "server";
            }

            MessageSendMessage(this, new Events.MessageSendMessage(from, to, id));
        }

        public event EventHandler<ClientConnectArgs> ClientConnectMessage;
        public event EventHandler<ServerConnectArgs> ServerConnectMessage;
        public event EventHandler<MessageSendMessage> MessageSendMessage;
    }
}
