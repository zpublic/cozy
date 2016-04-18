using CozyThunder.Botnet.Interface;
using System;
using CozyThunder.HttpDownload;
using CozyThunder.Protocol.FileBlock;
using CozyThunder.Schedule;
using CozyThunder.DistributedDownload.SlaveGui.Log;
using CozyThunder.Protocol;
using System.Threading;

namespace CozyThunder.DistributedDownload.SlaveGui.ViewModels.Slave
{
    public class SlaveConnectorListener : ISlavePeerListener
    {
        private ISlavePeer Peer { get; set; }

        public SlaveConnectorListener(ISlavePeer peer)
        {
            if(peer == null)
            {
                throw new ArgumentNullException("peer cannot be null");
            }
            Peer = peer;
        }

        public void OnConnect(string host)
        {
            LogManager.Instalce.ConnectLog(host);
        }

        public void OnDisConnect()
        {
            LogManager.Instalce.DisconnectLog();
        }

        byte[] sbuff_ = new byte[1024 * 4];
        int sbufflen_ = 0;
        public void OnMessage(byte[] msg)
        {
            Array.Copy(msg, 0,
                sbuff_,
                sbufflen_, msg.Length);
            sbufflen_ += msg.Length;
            PacketTest t = new PacketTest(sbuff_, 0);
            if (t.PacketLength > sbufflen_)
            {
                // string s = "拆包"; ok
            }
            else if (t.PacketLength < sbufflen_)
            {
                string s = "粘包";
            }
            else if (t.PacketLength == sbufflen_)
            {
                switch (t.PacketId)
                {
                    case 1:
                        StringPacket packet = new StringPacket();
                        packet.Decode(msg, 0, msg.Length);
                        break;
                    case 10004:
                        FileBlockTask task = new FileBlockTask();
                        task.Decode(msg, 0, msg.Length);
                        task_ = task.task_;
                        autoResetEvent_.Set();
                        new Thread(new ThreadStart(Download)).Start();
                        break;
                    case 10001:
                        autoResetEvent_.Set();
                        break;
                }
                sbufflen_ = 0;
            }
        }

        AutoResetEvent autoResetEvent_ = new AutoResetEvent(false);
        DownloadSubTask task_;
        void Download()
        {
            if (task_ != null)
            {
                LogManager.Instalce.DownloadTaskBeginLog(task_.RemotePath, task_.from, task_.to);
                byte[] data = HttpDownloadRange.Download(task_.RemotePath, task_.from, task_.to);
                LogManager.Instalce.DownloadTaskEndLog(task_.RemotePath, task_.from, task_.to);

                LogManager.Instalce.TransferBegin(task_.RemotePath, task_.from, task_.to);
                FileBlockBeginPacket begin = new FileBlockBeginPacket();
                autoResetEvent_.WaitOne();
                Peer.Send(begin.Encode());
                byte[] b3k = new byte[1024 * 3];
                int c = (data.Length + 3071) / 3072;
                for (int i = 0; i < c - 1; ++i)
                {
                    Array.Copy(data, 1024 * 3 * i, b3k, 0, 1024 * 3);
                    FileBlockDataPacket d = new FileBlockDataPacket(b3k);
                    autoResetEvent_.WaitOne();
                    Peer.Send(d.Encode());
                }
                int lastBlockSize = data.Length % 3072;
                if (lastBlockSize == 0)
                    lastBlockSize = 3072;
                {
                    byte[] bxk = new byte[lastBlockSize];
                    Array.Copy(data, 1024 * 3 * (c - 1), bxk, 0, lastBlockSize);
                    FileBlockDataPacket d = new FileBlockDataPacket(bxk);
                    autoResetEvent_.WaitOne();
                    Peer.Send(d.Encode());
                }
                FileBlockEndPacket end = new FileBlockEndPacket();
                autoResetEvent_.WaitOne();
                Peer.Send(end.Encode());
                LogManager.Instalce.TransferEnd(task_.RemotePath, task_.from, task_.to);
            }
        }
    }
}
