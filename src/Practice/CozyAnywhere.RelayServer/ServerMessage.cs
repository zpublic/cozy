using NetworkHelper;
using NetworkProtocol;
using Lidgren.Network;
using CozyAnywhere.Protocol.Messages;

namespace CozyAnywhere.RelayServerCore
{
    public partial class AnywhereRelayServer
    {
        private void InitMessage()
        {
            MessageReader.RegisterTypeWithAssembly("CozyAnywhere.Protocol", "CozyAnywhere.Protocol.Messages");
        }

        private void OnConnectMessage(IMessage msg, NetConnection conn)
        {
            var connMsg = (ConnectMessage)msg;
        }

        private void OnConnectQueryMessage(IMessage msg, NetConnection conn)
        {
            var rspMsg = new QueryConnectMessageRsp()
            {
                ConnectionType = QueryConnectMessageRsp.RelayServerType,
            };
            server.SendMessage(rspMsg, conn);
        }

        private void OnConnectQueryMessageRsp(IMessage msg, NetConnection conn)
        {
            var rspMsg = (QueryConnectMessageRsp)msg;
            if (rspMsg.ConnectionType == QueryConnectMessageRsp.ServerType)
            {
                server.ServerConn = conn;
            }
            else if(rspMsg.ConnectionType == QueryConnectMessageRsp.ClientType)
            {
                server.ClientConn = conn;
            }
            else
            {
                // TODO Disconnect the conn
            }
        }
    }
}
