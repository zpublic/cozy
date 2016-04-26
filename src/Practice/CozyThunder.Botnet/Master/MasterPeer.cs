using CozyThunder.Botnet.Common;
using CozyThunder.Botnet.Interface;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Net;

namespace CozyThunder.Botnet.Master
{
    public class MasterPeer : Peer, IMasterPeer, IMasterConnectorListener
    {
        IMasterPeerListener listener_;
        PeerList peerList_;
        ConcurrentDictionary<string, MasterConnector> connectorList_;

        public bool Start(IPAddress ip, int port, IMasterPeerListener listener)
        {
            listener_ = listener;
            EndPoint = new IPEndPoint(ip, port);
            peerList_ = new PeerList();
            connectorList_ = new ConcurrentDictionary<string, MasterConnector>();
            return true;
        }

        public bool Stop()
        {
            return true;
        }

        public bool AddPeer(Peer peer)
        {
            return peerList_.AddPeer(peer);
        }

        public bool DelPeer(Peer peer)
        {
            return peerList_.DelPeer(peer);
        }

        public PeerList GetPeerList()
        {
            return peerList_;
        }

        public bool Connect(Peer peer)
        {
            var connector = new MasterConnector();
            if(connector.Connect(peer, this))
            {
                if(connectorList_.TryAdd(peer.EndPoint.ToString(), connector))
                {
                    return true;
                }
            }
            return false;
        }

        public bool DisConnect(Peer peer)
        {
            MasterConnector connector;
            if (connectorList_.TryRemove(peer.EndPoint.ToString(), out connector))
            {
                connector.DisConnect();
                return true;
            }
            return false;
        }

        public bool Send(Peer peer, string msg)
        {
            MasterConnector connector;
            if (connectorList_.TryGetValue(peer.EndPoint.ToString(), out connector))
            {
                connector.Send(msg);
                return true;
            }
            return false;
        }

        public bool Send(Peer peer, byte[] msg)
        {
            MasterConnector connector;
            if (connectorList_.TryGetValue(peer.EndPoint.ToString(), out connector))
            {
                connector.Send(msg);
                return true;
            }
            return false;
        }

        public void OnConnect(Peer peer)
        {
            listener_?.OnConnect(peer);
        }

        public void OnDisConnect(Peer peer)
        {
            listener_?.OnDisConnect(peer);
        }

        public void OnMessage(Peer peer, byte[] msg)
        {
            listener_?.OnMessage(peer, msg);
        }
    }
}
