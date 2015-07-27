using CozyAnywhere.Protocol;
using NetworkHelper;
using NetworkHelper.Event;
using CozyAnywhere.Protocol.Messages;
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
                case MessageId.QueryConnectMessage:
                    OnConnectQueryMessage(baseMsg, msg.Input.SenderConnection);
                    break;
                case MessageId.QueryConnectMessageRsp:
                    OnConnectQueryMessageRsp(baseMsg, msg.Input.SenderConnection);
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