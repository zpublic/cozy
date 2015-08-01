using System;
using CozyAnywhere.RelayServerCore.Events;

namespace CozyAnywhere.RelayServerCore
{
    public partial class AnywhereRelayServer
    {
        public event EventHandler<ClientConnectArgs> ClientConnectMessage;

        public event EventHandler<ServerConnectArgs> ServerConnectMessage;

        public event EventHandler<MessageSendMessage> MessageSendMessage;
    }
}
