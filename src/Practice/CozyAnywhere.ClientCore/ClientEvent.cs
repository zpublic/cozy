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
                server.StatusMessage    += new EventHandler<DataMessageArgs>(OnStatusMessage);
                server.DataMessage      += new EventHandler<DataMessageArgs>(OnDataMessage);
                server.InternalMessage  += new EventHandler<InternalMessageArgs>(OnInternalMessage);
            }
        }

        private void OnStatusMessage(object sender, DataMessageArgs msg)
        {
            var status = (NetworkHelper.NetConnectionStatus)msg.Input.ReadByte();
            string reason = msg.Input.ReadString();
            if (status == NetworkHelper.NetConnectionStatus.Connected)
            {
                var rspMsg = new QueryConnectMessage();
                server.SendMessage(rspMsg, msg.Input.SenderConnection);
            }
        }

        private void OnDataMessage(object sender, DataMessageArgs msg)
        {
            var baseMsg = MessageReader.GetTypeInstanceByStream(msg.Input);
            MessageCallbackInvoker.Invoke(baseMsg, msg.Input.SenderConnection);
        }

        private void OnInternalMessage(object sender, InternalMessageArgs msg)
        {
        }
    }
}