using CozyThunder.Botnet.Interface;
using CozyThunder.Botnet.Slave;
using System;
using CozyThunder.Botnet.Common;
using CozyThunder.Botnet.Master;
using System.Net;

namespace CozyThunder.Botnet.Tester
{
    class Program
    {
        class SlavePeerListener : ISlavePeerListener
        {
            public void OnConnect(string host)
            {
                Console.WriteLine("OnConnect - " + host);
            }

            public void OnDisConnect()
            {
                Console.WriteLine("OnDisConnect");
            }

            public void OnMessage(string msg)
            {
                Console.WriteLine("OnMessage - " + msg.Length.ToString());
            }
        }

        class MasterPeerListener : IMasterPeerListener
        {
            public void OnConnect(Peer peer)
            {
                Console.WriteLine("OnConnect - " + peer.EndPoint.ToString());
            }

            public void OnDisConnect(Peer peer)
            {
                Console.WriteLine("OnDisConnect - " + peer.EndPoint.ToString());
            }

            public void OnMessage(Peer peer, string msg)
            {
                Console.WriteLine("OnMessage - " + peer.EndPoint.ToString() + " - " + msg.Length.ToString());
            }
        }

        static void Main(string[] args)
        {
            var r = Console.ReadLine();
            if (r == "master")
            {
                MasterPeer slave = new MasterPeer();
                slave.Start("127.0.0.1", 48360, new MasterPeerListener());
                var peer = new Peer() { EndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 48361) };
                slave.Connect(peer);
                Console.ReadKey();
                slave.Send(peer, "kingwl");
                Console.ReadKey();
                slave.Stop();
            }
            else if (r == "slave")
            {
                SlavePeer slave = new SlavePeer();
                slave.Start("127.0.0.1", 48361, new SlavePeerListener());
                Console.ReadKey();
                slave.Stop();
            }
        }
    }
}
