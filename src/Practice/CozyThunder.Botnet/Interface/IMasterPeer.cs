using CozyThunder.Botnet.Common;
using System.Net;

namespace CozyThunder.Botnet.Interface
{
    public interface IMasterPeer
    {
        bool Start(IPAddress ip, int port, IMasterPeerListener listener);
        bool Stop();

        bool AddPeer(Peer peer);
        bool DelPeer(Peer peer);
        PeerList GetPeerList();

        bool Connect(Peer peer);
        bool DisConnect(Peer peer);
        bool Send(Peer peer, string msg);
        bool Send(Peer peer, byte[] msg);
    }
}
