using System;
using NetworkHelper.Event;
using NetworkHelper;
using CozyAnywhere.Protocol;

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
        }

        private void OnDataMessage(object sender, DataMessageArgs msg)
        {
            var baseMsg = MessageReader.GetTypeInstanceByStream(msg.Input);
            if(baseMsg.Id == MessageId.ConnectMessage)
            {
                OnConnectMessage(baseMsg, msg.Input.SenderConnection);
            }
        }

        private void OnInternalMessage(object sender, InternalMessageArgs msg)
        {
        }
    }
}
