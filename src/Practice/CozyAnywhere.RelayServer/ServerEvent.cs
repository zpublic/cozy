using System;
using NetworkHelper.Event;
using NetworkHelper;
using CozyAnywhere.Protocol;
using CozyAnywhere.Protocol.Messages;

namespace CozyAnywhere.RelayServerCore
{
    public partial class AnywhereRelayServer
    {
        private void InitEvent()
        {
            server.RelayMessage += new EventHandler<DataMessageArgs>(OnRelayMessage);
            server.DataMessage += new EventHandler<DataMessageArgs>(OnDataMessage);
            server.InternalMessage += new EventHandler<InternalMessageArgs>(OnInternalMessage);
        }

        private void OnRelayMessage(object sender, DataMessageArgs msg)
        {
            NetConnectionStatus status = (NetConnectionStatus)msg.Input.ReadByte();
            string reason = msg.Input.ReadString();
            if (status == NetConnectionStatus.Connected)
            {
                var ms = new ConnectionTypeQueryMessage();
                server.SendMessage(ms, msg.Input.SenderConnection);
            }
        }

        private void OnDataMessage(object sender, DataMessageArgs msg)
        {
            var baseMsg = MessageReader.GetTypeInstanceByStream(msg.Input);
            if (baseMsg.Id == MessageId.ConnectionTypeQueryMessageRsp)
            {
                var type = ((ConnectionTypeQueryMessageRsp)baseMsg).ConnectionType;
                if (server.ClientConn == null && type == ConnectionTypeQueryMessageRsp.ClientType)
                {
                    server.ClientConn = msg.Input.SenderConnection;
                }
                else if (server.ServerConn == null && type == ConnectionTypeQueryMessageRsp.ServerType)
                {
                    server.ServerConn = msg.Input.SenderConnection;
                }
                else
                {
                    var ms = new CloseConnectionMessage();
                    server.SendMessage(ms, msg.Input.SenderConnection);
                }
            }
            else
            {
                if (msg.Input.SenderConnection == server.ServerConn)
                {
                    server.SendMessageToClient(baseMsg);
                }
                else if (msg.Input.SenderConnection == server.ServerConn)
                {
                    server.SendMessageToServer(baseMsg);
                }
            }
        }

        private void OnInternalMessage(object sender, InternalMessageArgs msg)
        {
        }
    }
}
