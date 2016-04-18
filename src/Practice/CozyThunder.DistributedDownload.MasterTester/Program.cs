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
        object lock_ = new object();

        public void OnConnect(Peer peer)
        {
            Console.WriteLine("OnConnect - " + peer.EndPoint.ToString());
            lock (lock_)
            {
                subTask_.Add(peer.EndPoint.ToString(), Program.task.GetSubTask());
                i_.Add(peer.EndPoint.ToString(), 0);
                buff_.Add(peer.EndPoint.ToString(), new byte[1024 * 1024 * 3]);
                sbuff_.Add(peer.EndPoint.ToString(), new byte[1024 * 1024 * 4]);
                sbufflen_.Add(peer.EndPoint.ToString(), 0);
            }
            if (subTask_[peer.EndPoint.ToString()] != null)
            {
                FileBlockTask f = new FileBlockTask(subTask_[peer.EndPoint.ToString()]);
                Program.master.Send(peer, f.Encode());
            }
        }

        public void OnDisConnect(Peer peer)
        {
            Console.WriteLine("OnDisConnect - " + peer.EndPoint.ToString());
        }

        Dictionary<string, int> i_ = new Dictionary<string, int>();
        Dictionary<string, byte[]> buff_ = new Dictionary<string, byte[]>();
        Dictionary<string, DownloadSubTask> subTask_ = new Dictionary<string, DownloadSubTask>();
        Dictionary<string, byte[]> sbuff_ = new Dictionary<string, byte[]>();
        Dictionary<string, int> sbufflen_ = new Dictionary<string, int>();

        public void OnMessage(Peer peer, byte[] msg)
        {
            Array.Copy(msg, 0,
                sbuff_[peer.EndPoint.ToString()],
                sbufflen_[peer.EndPoint.ToString()], msg.Length);
            sbufflen_[peer.EndPoint.ToString()] += msg.Length;
            PacketTest t = new PacketTest(sbuff_[peer.EndPoint.ToString()], 0);
            if (t.PacketLength > sbufflen_[peer.EndPoint.ToString()])
            {
                // string s = "拆包"; ok
            }
            else if (t.PacketLength < sbufflen_[peer.EndPoint.ToString()])
            {
                string s = "粘包";
            }
            else if (t.PacketLength == sbufflen_[peer.EndPoint.ToString()])
            {
                switch (t.PacketId)
                {
                    case 1:
                        StringPacket packet = new StringPacket();
                        packet.Decode(sbuff_[peer.EndPoint.ToString()], 0, sbufflen_[peer.EndPoint.ToString()]);
                        Console.WriteLine("OnMessage - " + packet.data);
                        break;
                    case 10001:
                        Console.WriteLine("OnMessage - FileBlockBeginPacket");
                        FileBlockBeginPacket begin = new FileBlockBeginPacket();
                        Program.master.Send(peer, begin.Encode());
                        break;
                    case 10002:
                        FileBlockDataPacket d = new FileBlockDataPacket();
                        d.Decode(sbuff_[peer.EndPoint.ToString()], 0, sbufflen_[peer.EndPoint.ToString()]);
                        Array.Copy(d.data3k, 0, buff_[peer.EndPoint.ToString()], i_[peer.EndPoint.ToString()] * 1024 * 3, d.data3k.Length);
                        i_[peer.EndPoint.ToString()]++;
                        FileBlockBeginPacket begin2 = new FileBlockBeginPacket();
                        Program.master.Send(peer, begin2.Encode());
                        break;
                    case 10003:
                        i_[peer.EndPoint.ToString()] = 0;
                        Console.WriteLine("OnMessage - FileBlockEndPacket");
                        subTask_[peer.EndPoint.ToString()].data = buff_[peer.EndPoint.ToString()];
                        var len = subTask_[peer.EndPoint.ToString()].to - subTask_[peer.EndPoint.ToString()].from + 1;
                        if (len != Program.task.BlockSize)
                        {
                            subTask_[peer.EndPoint.ToString()].data = new byte[len];
                            Array.Copy(buff_[peer.EndPoint.ToString()], 0, subTask_[peer.EndPoint.ToString()].data, 0, len);
                        }
                        if (!Program.task.UpdateTaskState(subTask_[peer.EndPoint.ToString()], 2))
                        {
                            subTask_[peer.EndPoint.ToString()] = Program.task.GetSubTask();
                            if (subTask_[peer.EndPoint.ToString()] != null)
                            {
                                FileBlockTask f = new FileBlockTask(subTask_[peer.EndPoint.ToString()]);
                                Program.master.Send(peer, f.Encode());
                            }
                        }
                        break;
                }
                sbufflen_[peer.EndPoint.ToString()] = 0;
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
            task.RemotePath = @"http://speed.myzone.cn/pc_elive_1.1.rar"; //60M
            //task.RemotePath = @"http://cd002.www.duba.net/duba/install/2011/ever/duba160406_100_50.exe"; //17M
            task.LocalPath = @"d:\hehe.rar";
            task.BlockSize = 1024 * 1024 * 3;
            if (task.InitTask())
            {
                Console.WriteLine("begin download");
            }

            Console.ReadKey();

            master = new MasterPeer();
            master.Start(IPAddress.Any, 48360, new MasterPeerListener());
            List<KeyValuePair<string, int>> peerList = new List<KeyValuePair<string, int>>()
            {
                new KeyValuePair<string, int>("127.0.0.1", 48360),
                //new KeyValuePair<string, int>("10.20.208.27", 48235),
                //new KeyValuePair<string, int>("10.20.208.27", 48362),
                //new KeyValuePair<string, int>("10.20.221.119", 48360),
                //new KeyValuePair<string, int>("10.20.208.30", 48360), //wyf
                //new KeyValuePair<string, int>("10.20.208.38", 48390), //hym
                //new KeyValuePair<string, int>("10.20.208.55", 48390), //hsj
            };
            foreach (var i in peerList)
            {
                var peer = new Peer() { EndPoint = new IPEndPoint(IPAddress.Parse(i.Key), i.Value) };
                master.Connect(peer);
            }
            Console.ReadKey();
        }
    }
}
