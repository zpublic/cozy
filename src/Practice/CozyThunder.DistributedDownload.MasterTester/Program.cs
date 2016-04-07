using CozyThunder.Botnet.Common;
using CozyThunder.Botnet.Interface;
using CozyThunder.Botnet.Master;
using CozyThunder.Protocol;
using CozyThunder.Protocol.FileBlock;
using CozyThunder.Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CozyThunder.DistributedDownload.MasterTester
{
    class MasterPeerListener : IMasterPeerListener
    {
        public void OnConnect(Peer peer)
        {
            Console.WriteLine("OnConnect - " + peer.EndPoint.ToString());
            subTask_[peer] = Program.task.GetSubTask();
            i_[peer] = 0;
            buff_[peer] = new byte[1024 * 1024 * 3];
            if (subTask_[peer] != null)
            {
                FileBlockTask f = new FileBlockTask(subTask_[peer]);
                Program.master.Send(peer, f.Encode());
            }
        }

        public void OnDisConnect(Peer peer)
        {
            Console.WriteLine("OnDisConnect - " + peer.EndPoint.ToString());
        }

        Dictionary<Peer, int> i_ = new Dictionary<Peer, int>();
        Dictionary<Peer, byte[]> buff_ = new Dictionary<Peer, byte[]>();
        Dictionary<Peer, DownloadSubTask> subTask_ = new Dictionary<Peer, DownloadSubTask>();

        public void OnMessage(Peer peer, byte[] msg)
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
                    Console.WriteLine("OnMessage - FileBlockBeginPacket");
                    FileBlockBeginPacket begin = new FileBlockBeginPacket();
                    Program.master.Send(peer, begin.Encode());
                    break;
                case 10002:
                    FileBlockDataPacket d = new FileBlockDataPacket();
                    d.Decode(msg, 0, msg.Length);
                    Array.Copy(d.data3k, 0, buff_[peer], i_[peer] * 1024 * 3, d.data3k.Length);
                    i_[peer]++;
                    FileBlockBeginPacket begin2 = new FileBlockBeginPacket();
                    Program.master.Send(peer, begin2.Encode());
                    break;
                case 10003:
                    i_[peer] = 0;
                    Console.WriteLine("OnMessage - FileBlockEndPacket");
                    subTask_[peer].data = buff_[peer];
                    var len = subTask_[peer].to - subTask_[peer].from + 1;
                    if (len != Program.task.BlockSize)
                    {
                        subTask_[peer].data = new byte[len];
                        Array.Copy(buff_[peer], 0, subTask_[peer].data, 0, len);
                    }
                    if (!Program.task.UpdateTaskState(subTask_[peer], 2))
                    {
                        subTask_[peer] = Program.task.GetSubTask();
                        if (subTask_[peer] != null)
                        {
                            FileBlockTask f = new FileBlockTask(subTask_[peer]);
                            Program.master.Send(peer, f.Encode());
                        }
                    }
                    break;
            }
        }
    }

    class Program
    {
        public static MasterPeer master;
        public static DownloadTask task;
        static void Main(string[] args)
        {
            task = new DownloadTask();
            task.RemotePath = @"http://speed.myzone.cn/pc_elive_1.1.rar";
            task.LocalPath = @"d:\hehe.rar";
            task.BlockSize = 1024 * 1024 * 3;
            if (task.InitTask())
            {
                Console.WriteLine("begin download");
            }

            Console.ReadKey();

            master = new MasterPeer();
            master.Start(IPAddress.Any, 48360, new MasterPeerListener());
            var peer = new Peer() { EndPoint = new IPEndPoint(IPAddress.Parse("10.20.208.38"), 48360) }; //10.20.208.27 38
            var peer2 = new Peer() { EndPoint = new IPEndPoint(IPAddress.Parse("10.20.208.30"), 48360) };
            var peer3 = new Peer() { EndPoint = new IPEndPoint(IPAddress.Parse("10.20.208.27"), 48360) };
            master.Connect(peer);
            master.Connect(peer2);
            master.Connect(peer3);

            Console.ReadKey();
        }
    }
}
