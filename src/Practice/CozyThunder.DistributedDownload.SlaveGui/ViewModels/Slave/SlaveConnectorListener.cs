using CozyThunder.Botnet.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyThunder.HttpDownload;
using CozyThunder.Protocol.FileBlock;
using CozyThunder.Schedule;

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
            Console.WriteLine(host + "connect");
        }

        public void OnDisConnect()
        {
            Console.WriteLine("disconnect");
        }

        public void OnMessage(byte[] msg)
        {
            Console.WriteLine("message : {0}", msg.Length);

            var task = new FileBlockTask();
            task.Decode(msg, 0, msg.Length);

            var downloadTask = task.task_;
            var data = HttpDownloadRange.Download(downloadTask.RemotePath, downloadTask.from, downloadTask.to);

            if (data.Length != downloadTask.to - downloadTask.from)
            {
                throw new Exception("download error length is {0}" + (downloadTask.to - downloadTask.from));
            }

            var header  = new FileBlockBeginPacket();
            var body    = new FileBlockDataPacket(data);
            var foot    = new FileBlockEndPacket();

            var buffer      = new byte[header.ByteLength + body.ByteLength + foot.ByteLength];
            var offset      = 0;

            header.Encode(buffer, offset);
            offset += header.ByteLength;

            body.Encode(buffer, offset);
            offset += body.ByteLength;

            foot.Encode(buffer, offset);
            offset += foot.ByteLength;

            Peer.Send(buffer);
        }
    }
}
