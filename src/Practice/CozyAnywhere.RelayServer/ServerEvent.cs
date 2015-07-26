using System;
using NetworkHelper.Event;
using NetworkHelper;
using CozyAnywhere.Protocol;
using CozyAnywhere.RelayServerCore.Events;
using CozyAnywhere.Protocol.Messages;

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
            var conn = msg.Input.SenderConnection;
            if (status == NetworkHelper.NetConnectionStatus.Connected)
            {
                if (server.ServerConn == null)
                {
                    server.ServerConn = conn;
                }
                else if (server.ClientConn == null)
                {
                    server.ClientConn = conn;
                }
                else
                {
                    return;
                }
                var queryMsg = new QueryConnectMessage();
                server.SendMessage(queryMsg, conn);
            }
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
                    OnQueryMessageRsp(baseMsg, msg.Input.SenderConnection);
                }
            }
            else
            {
                if(msg.Input.SenderConnection == server.ServerConn)
                {
                    server.SendMessageToClient(baseMsg);
                }
                else if(msg.Input.SenderConnection == server.ClientConn)
                {
                    server.SendMessageToServer(baseMsg);
                }
            }
        }

        private void OnInternalMessage(object sender, InternalMessageArgs msg)
        {
        }

        public event EventHandler<ClientConnectArgs> ClientConnectMessage;
        public event EventHandler<ServerConnectArgs> ServerConnectMessage;
    }
}
