using CozyAnywhere.Protocol;
using NetworkHelper;
using NetworkHelper.Event;
using System;
using Lidgren.Network;

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
            }
        }

        private void OnDataMessage(object sender, DataMessageArgs msg)
        {
            var baseMsg = MessageReader.GetTypeInstanceByStream(msg.Input);
            switch (baseMsg.Id)
            {
                case MessageId.CommandMessageRsp:
                    OnCommandMessageRsp(baseMsg);
                    break;
                case MessageId.PluginQueryMessage:
                    OnPluginQueryMessage(baseMsg);
                    break;
                case MessageId.BinaryPacketMessage:
                    OnBinaryPacketMessage(baseMsg);
                    break;
                case MessageId.ConnectMessage:
                    OnConnectMessage(baseMsg, msg.Input.SenderConnection);
                    break;
                case MessageId.QueryConnectMessageRsp:
                    OnQueryMessageRsp(baseMsg, msg.Input.SenderConnection);
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