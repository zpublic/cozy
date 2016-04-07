using CozyThunder.Botnet.Common;
using CozyThunder.Botnet.Interface;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace CozyThunder.Botnet.Master
{
    public class MasterPeer : Peer, IMasterPeer
    {
        IMasterPeerListener listener_;
        PeerList peerList_;
        Socket c1_;

        public bool Start(string ip, int port, IMasterPeerListener listener)
        {
            listener_ = listener;
            EndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            peerList_ = new PeerList();
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
            c1_ = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            c1_.BeginConnect(peer.EndPoint.Address, peer.EndPoint.Port, ConnectCallback, c1_);
            return true;
        }

        public bool DisConnect(Peer peer)
        {
            c1_?.Shutdown(SocketShutdown.Both);
            c1_?.Close();
            return true;
        }

        public bool Send(Peer peer, string msg)
        {
            byte[] byteData = Encoding.ASCII.GetBytes(msg);
            c1_.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), c1_);
            return true;
        }

        void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;
                client.EndConnect(ar);
            }
            catch {}
        }

        void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;
                int bytesSent = client.EndSend(ar);
            }
            catch {}
        }
    }
}
