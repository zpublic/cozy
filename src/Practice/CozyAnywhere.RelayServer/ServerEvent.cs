using System;
using NetworkHelper.Event;
using NetworkHelper;
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
            var queryMsg = new QueryConnectMessage();
            server.SendMessage(queryMsg, msg.Input.SenderConnection);
        }

        private void OnDataMessage(object sender, DataMessageArgs msg)
        {
            var baseMsg = MessageReader.GetTypeInstanceByStream(msg.Input);
            MessageCallbackInvoker.Invoke(baseMsg, msg.Input.SenderConnection);
        }

        private void OnInternalMessage(object sender, InternalMessageArgs msg)
        {
        }

        public event EventHandler<ClientConnectArgs> ClientConnectMessage;
        public event EventHandler<ServerConnectArgs> ServerConnectMessage;
        public event EventHandler<MessageSendMessage> MessageSendMessage;
    }
}
