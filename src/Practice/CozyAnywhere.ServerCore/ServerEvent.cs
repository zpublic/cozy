using NetworkHelper.Event;
using NetworkHelper;
using System;
using CozyAnywhere.Protocol;
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
        }

        private void OnDataMessage(object sender, DataMessageArgs msg)
        {
            var baseMsg = MessageReader.GetTypeInstanceByStream(msg.Input);
            switch (baseMsg.Id)
            {
                case MessageId.CommandMessage:
                    OnCommandMessage(baseMsg);
                    break;

                default:
                    break;
            }
        }

        private void OnInternalMessage(object sender, InternalMessageArgs msg)
        {
        }
    }
}