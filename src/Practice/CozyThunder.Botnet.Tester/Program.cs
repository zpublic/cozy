using CozyThunder.Botnet.Interface;
using CozyThunder.Botnet.Slave;
using System;
using CozyThunder.Botnet.Common;
using CozyThunder.Botnet.Master;
using System.Net;
using System.Text;

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

            public void OnMessage(byte[] msg)
            {
                Console.WriteLine("OnMessage - " + Encoding.ASCII.GetString(msg, 0, msg.Length));
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

            public void OnMessage(Peer peer, byte[] msg)
            {
                Console.WriteLine("OnMessage - " + peer.EndPoint.ToString() + " - " + Encoding.ASCII.GetString(msg, 0, msg.Length));
            }
        }

        static void Main(string[] args)
        {
            var r = Console.ReadLine();
            if (r == "master")
            {
                MasterPeer master = new MasterPeer();
                master.Start(IPAddress.Any, 48360, new MasterPeerListener());
                var peer = new Peer() { EndPoint = new IPEndPoint(IPAddress.Parse("10.20.208.27"), 48361) };
                master.Connect(peer);
                Console.ReadKey();
                master.Send(peer, "kingwl");
                Console.ReadKey();
                master.Stop();
            }
            else if (r == "master2")
            {
                MasterPeer master = new MasterPeer();
                master.Start(IPAddress.Any, 48360, new MasterPeerListener());
                var peer1 = new Peer() { EndPoint = new IPEndPoint(IPAddress.Parse("10.20.208.27"), 48361) };
                var peer2 = new Peer() { EndPoint = new IPEndPoint(IPAddress.Parse("10.20.208.27"), 48362) };
                var peer3 = new Peer() { EndPoint = new IPEndPoint(IPAddress.Parse("10.20.208.27"), 48363) };
                master.Connect(peer1);
                master.Connect(peer2);
                master.Connect(peer3);
                Console.ReadKey();
                master.Send(peer1, "kingwl");
                master.Send(peer2, "kingwlkingwl");
                master.Send(peer3, "kingwlkingwlkingwl");
                Console.ReadKey();
                master.Stop();
            }
            else if (r == "slave")
            {
                SlavePeer slave = new SlavePeer();
                slave.Start(IPAddress.Any, 48361, new SlavePeerListener());
                Console.ReadKey();
                slave.Send("hehe");
                Console.ReadKey();
                slave.Stop();
            }
            else if (r == "slave2")
            {
                SlavePeer slave = new SlavePeer();
                slave.Start(IPAddress.Any, 48362, new SlavePeerListener());
                SlavePeer slave2 = new SlavePeer();
                slave2.Start(IPAddress.Any, 48363, new SlavePeerListener());
                Console.ReadKey();
                slave.Stop();
                slave2.Stop();
            }
        }
    }
}
