using CozyThunder.Botnet.Interface;
using CozyThunder.Botnet.Slave;
using CozyThunder.HttpDownload;
using CozyThunder.Protocol;
using CozyThunder.Protocol.FileBlock;
using CozyThunder.Schedule;
using System;
using System.Net;
using System.Threading;

namespace CozyThunder.DistributedDownload.SlaveTester
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
                case 10004:
                    FileBlockTask task = new FileBlockTask();
                    task.Decode(msg, 0, msg.Length);
                    task_ = task.task_;
                    Console.WriteLine("OnMessage - FileBlockTask");
                    autoResetEvent_.Set();
                    new Thread(new ThreadStart(Download)).Start();
                    break;
                case 10001:
                    autoResetEvent_.Set();
                    break;
            }
        }

        AutoResetEvent autoResetEvent_ = new AutoResetEvent(false);
        DownloadSubTask task_;
        void Download()
        {
            if (task_ != null)
            {
                Console.WriteLine("Download begin");
                byte[] data = HttpDownloadRange.Download(task_.RemotePath, task_.from, task_.to);
                Console.WriteLine("Download end");

                Console.WriteLine("trans begin");
                FileBlockBeginPacket begin = new FileBlockBeginPacket();
                autoResetEvent_.WaitOne();
                Program.slave.Send(begin.Encode());
                byte[] b3k = new byte[1024 * 3];
                int c = (data.Length + 3071) / 3072;
                for (int i = 0; i < c - 1; ++i)
                {
                    Array.Copy(data, 1024 * 3 * i, b3k, 0, 1024 * 3);
                    FileBlockDataPacket d = new FileBlockDataPacket(b3k);
                    autoResetEvent_.WaitOne();
                    Program.slave.Send(d.Encode());
                }
                int lastBlockSize = data.Length % 3072;
                if (lastBlockSize == 0)
                    lastBlockSize = 3072;
                {
                    Array.Copy(data, 1024 * 3 * (c - 1), b3k, 0, lastBlockSize);
                    FileBlockDataPacket d = new FileBlockDataPacket(b3k);
                    autoResetEvent_.WaitOne();
                    Program.slave.Send(d.Encode());
                }
                FileBlockEndPacket end = new FileBlockEndPacket();
                autoResetEvent_.WaitOne();
                Program.slave.Send(end.Encode());
                Console.WriteLine("trans end");
            }
        }
    }
    
    class Program
    {
        public static SlavePeer slave;
        static void Main(string[] args)
        {
            slave = new SlavePeer();
            slave.Start(IPAddress.Any, 48360, new SlavePeerListener());
            Console.ReadKey();
        }
    }
}
