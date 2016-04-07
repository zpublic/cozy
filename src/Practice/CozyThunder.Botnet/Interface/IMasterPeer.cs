using CozyThunder.Botnet.Common;

namespace CozyThunder.Botnet.Interface
{
    public interface IMasterPeer
    {
        bool Start(string ip, int port, IMasterPeerListener listener);
        bool Stop();

        bool AddPeer(Peer peer);
        bool DelPeer(Peer peer);
        PeerList GetPeerList();

        bool Connect(Peer peer);
        bool DisConnect(Peer peer);
        bool Send(Peer peer, string msg);
    }
}
