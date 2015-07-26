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
    }
}
