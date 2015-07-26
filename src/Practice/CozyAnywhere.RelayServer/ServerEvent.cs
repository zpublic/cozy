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
            server.StatusMessage += new EventHandler<StatusMessageArgs>(OnStatusMessage);
            server.DataMessage += new EventHandler<DataMessageArgs>(OnDataMessage);
            server.InternalMessage += new EventHandler<InternalMessageArgs>(OnInternalMessage);
        }

        private void OnStatusMessage(object sender, StatusMessageArgs msg)
        {
            if(msg.Status == NetworkHelper.NetConnectionStatus.Connected)
            {
            }
        }

        private void OnDataMessage(object sender, DataMessageArgs msg)
        {
            var baseMsg = MessageReader.GetTypeInstanceByStream(msg.Input);
            if (baseMsg.Id == MessageId.ConnectMessage)
            {
                OnConnectMessage(baseMsg, msg.Input.SenderConnection);
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
