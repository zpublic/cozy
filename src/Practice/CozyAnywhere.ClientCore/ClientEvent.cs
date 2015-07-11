using CozyAnywhere.Protocol;
using NetworkHelper;
using NetworkHelper.Event;
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
            if (msg.Status == NetConnectionStatus.Connected)
            {
                SendTestMessage();
            }
        }

        private void OnDataMessage(object sender, DataMessageArgs msg)
        {
            var baseMsg = MessageReader.GetTypeInstanceByStream(msg.Input);
            switch (baseMsg.Id)
            {
                case MessageId.FileEnumMessageRsp:
                    OnFileEnumMessageRsp(baseMsg);
                    break;
                case MessageId.CommandMessageRsp:
                    OnCommandMessageRsp(baseMsg);
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