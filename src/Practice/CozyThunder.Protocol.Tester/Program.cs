using CozyThunder.Botnet.Common;
using CozyThunder.Botnet.Interface;
using CozyThunder.Botnet.Master;
using CozyThunder.Botnet.Slave;
using CozyThunder.Protocol.FileBlock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CozyThunder.Protocol.Tester
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
            PacketTest t = new PacketTest(msg, 0);
            switch (t.PacketId)
            {
                case 1:
                    StringPacket packet = new StringPacket();
                    packet.Decode(msg, 0, msg.Length);
                    Console.WriteLine("OnMessage - " + packet.data);
                    break;
                case 10001:
                    FileBlockBeginPacket begin = new FileBlockBeginPacket();
                    Console.WriteLine("OnMessage - FileBlockBeginPacket");
                    break;
                case 10002:
                    //Console.WriteLine("OnMessage - FileBlockDataPacket");
                    break;
                case 10003:
                    Console.WriteLine("OnMessage - FileBlockEndPacket");
                    break;
            }
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

    class Program
    {
        static void Main(string[] args)
        {
            SlavePeer slave = new SlavePeer();
            slave.Start(IPAddress.Any, 48361, new SlavePeerListener());

            MasterPeer master = new MasterPeer();
            master.Start(IPAddress.Any, 48360, new MasterPeerListener());
            var peer = new Peer() { EndPoint = new IPEndPoint(IPAddress.Parse("10.20.208.27"), 48361) };
            master.Connect(peer);

            Console.ReadKey();
            StringPacket sp = new StringPacket("hehe");
            master.Send(peer, sp.Encode());

            Console.ReadKey();
            // 传输3M的数据
            FileBlockBeginPacket begin = new FileBlockBeginPacket();
            master.Send(peer, begin.Encode());
            byte[] b3k = new byte[1024 * 3];
            for (int i = 0; i < 1024; ++i)
            {
                FileBlockDataPacket d = new FileBlockDataPacket(b3k);
                master.Send(peer, d.Encode());
            }
            FileBlockEndPacket end = new FileBlockEndPacket();
            master.Send(peer, end.Encode());

            Console.ReadKey();
        }
    }
}
