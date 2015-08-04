using NetworkHelper;
using NetworkHelper.Event;
using CozyAnywhere.Protocol.Messages;
using System;

namespace CozyAnywhere.ClientCore
{
    public partial class AnywhereClient
    {
        private void InitClientEvent()
        {
            if (server != null)
            {
                server.StatusMessage    += new EventHandler<StatusMessageArgs>(OnStatusMessage);
                server.DataMessage      += new EventHandler<DataMessageArgs>(OnDataMessage);
                server.InternalMessage  += new EventHandler<InternalMessageArgs>(OnInternalMessage);
            }
        }

        private void OnStatusMessage(object sender, StatusMessageArgs msg)
        {
            if (msg.Status == NetworkHelper.NetConnectionStatus.Connected)
            {
                var rspMsg = new QueryConnectMessage();
                server.SendMessage(rspMsg, msg.Connection);
            }
        }

        private void OnDataMessage(object sender, DataMessageArgs msg)
        {
            var baseMsg = MessageReader.GetTypeInstance(msg.MessageId, msg.Input);
            MessageCallbackInvoker.Invoke(baseMsg, msg.Connection);
        }

        private void OnInternalMessage(object sender, InternalMessageArgs msg)
        {
        }
    }
}