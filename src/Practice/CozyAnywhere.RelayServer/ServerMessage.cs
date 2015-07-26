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
            if(conn == server.ServerConn)
            {
                ServerConnectMessage(this, new Events.ServerConnectArgs());
            }
            else if(conn == server.ClientConn)
            {
                ClientConnectMessage(this, new Events.ClientConnectArgs());
            }
        }

        private void OnQueryMessageRsp(IMessage msg, NetConnection conn)
        {
            var rspMsg = (QueryConnectMessageRsp)msg;
            var disMsg = new ConnectMessageRsp();
            if ((conn == server.ServerConn && rspMsg.ConnectionType == QueryConnectMessageRsp.ServerType)
                || (conn == server.ClientConn && rspMsg.ConnectionType == QueryConnectMessageRsp.ClientType))
            {
                disMsg.CanConnect = true;
            }
            else
            {
                disMsg.CanConnect = false;
            }
            server.SendMessage(disMsg, conn);
        }

    }
}
