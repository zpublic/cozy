using NetworkHelper.Event;
using NetworkHelper;
using System;
using CozyAnywhere.Protocol.Messages;
using CozyAnywhere.Protocol;

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
            var baseMsg = MessageReader.GetTypeInstanceByStream(msg.Input);
            switch (baseMsg.Id)
            {
                case MessageId.CommandMessage:
                    OnCommandMessage(baseMsg);
                    break;
                case MessageId.PluginLoadMessage:
                    OnPluginLoadMessage(baseMsg);
                    break;
                case MessageId.QueryConnectMessage:
                    OnConnectQueryMessage(baseMsg);
                    break;
                case MessageId.QueryConnectMessageRsp:
                    OnConnectQueryMessageRsp(baseMsg);
                    break;
                case MessageId.ConnectMessage:
                    OnConnectMessage(baseMsg);
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