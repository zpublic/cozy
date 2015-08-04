using NetworkHelper.Event;
using NetworkHelper;
using System;
using CozyAnywhere.Protocol.Messages;

namespace CozyAnywhere.ServerCore
{
    public partial class AnywhereServer
    {
        public void InitServerEvent()
        {
            if (client != null)
            {
                client.StatusMessage    += new EventHandler<StatusMessageArgs>(OnStatusMessage);
                client.DataMessage      += new EventHandler<DataMessageArgs>(OnDataMessage);
                client.InternalMessage  += new EventHandler<InternalMessageArgs>(OnInternalMessage);
            }
        }

        private void OnStatusMessage(object sender, StatusMessageArgs msg)
        {
            if (msg.Status == NetConnectionStatus.Connected)
            {
                var rspMsg = new QueryConnectMessage();
                client.SendMessage(rspMsg);
            }
        }

        private void OnDataMessage(object sender, DataMessageArgs msg)
        {
            var baseMsg = MessageReader.GetTypeInstance(msg.MessageId, msg.Input);
            MessageCallbackInvoker.Invoke(baseMsg, msg.Input.SenderConnection);
        }

        private void OnInternalMessage(object sender, InternalMessageArgs msg)
        {
        }
    }
}