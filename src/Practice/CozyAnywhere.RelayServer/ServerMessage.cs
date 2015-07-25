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
            if (server.ServerConn == null && connMsg.ConnectionType == ConnectMessage.ServerType)
            {
                server.ServerConn = conn;
            }
            else if(server.ClientConn == null && connMsg.ConnectionType == ConnectMessage.ClientType)
            {
                server.ClientConn = conn;
                server.SendMessageToServer(msg);
            }
            else
            {
                var rspMsg = new ConnectMessageRsp()
                {
                    CanConnect = false,
                };
                server.SendMessage(rspMsg, conn);
            }
        }
    }
}
