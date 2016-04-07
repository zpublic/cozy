using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace CozyThunder.Botnet.Common
{
    public class PeerList
    {
        Dictionary<string, Peer> peers_ = new Dictionary<string, Peer>();

        public bool AddPeer(Peer peer, bool force = false)
        {
            if (force || !peers_.ContainsKey(peer.EndPoint.ToString()))
            {
                peers_.Add(peer.EndPoint.ToString(), peer);
                return true;
            }
            return false;
        }

        public bool DelPeer(Peer peer)
        {
            return peers_.Remove(peer.EndPoint.ToString());
        }

        public bool DelPeer(IPAddress ip, int port)
        {
            return peers_.Remove(new IPEndPoint(ip, port).ToString());
        }

        public IEnumerable<KeyValuePair<string, Peer>> AsEnumerable()
        {
            return peers_.AsEnumerable();
        }
    }
}
