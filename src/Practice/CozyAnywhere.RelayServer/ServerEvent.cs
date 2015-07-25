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
        }

        private void OnDataMessage(object sender, DataMessageArgs msg)
        {
            var baseMsg = MessageReader.GetTypeInstanceByStream(msg.Input);
        }

        private void OnInternalMessage(object sender, InternalMessageArgs msg)
        {
        }
    }
}
