using CozyThunder.Botnet.Common;
using CozyThunder.Protocol;
using CozyThunder.Protocol.FileBlock;
using CozyThunder.Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyThunder.DistributedDownload.MasterGui.ViewModels
{
    public partial class MainWindowViewModel
    {
        Dictionary<string, int> i_ = new Dictionary<string, int>();
        Dictionary<string, byte[]> buff_ = new Dictionary<string, byte[]>();
        Dictionary<string, DownloadSubTask> subTask_ = new Dictionary<string, DownloadSubTask>();

        public void MessageDispatch(Peer peer, byte[] data, int id)
        {
            switch (id)
            {
                case 1:
                    OnStringPacket(peer, data);
                    break;
                case 10001:
                    OnFileBlockBeginPacket(peer, data);
                    break;
                case 10002:
                    OnFileBlockDataPacket(peer, data);
                    break;
                case 10003:
                    OnFileBlockEndPacket(peer, data);
                    break;
            }
            sbufflen_[peer.EndPoint.ToString()] = 0;
        }

        private void OnStringPacket(Peer peer, byte[] data)
        {
            StringPacket packet = new StringPacket();
            packet.Decode(sbuff_[peer.EndPoint.ToString()], 0, sbufflen_[peer.EndPoint.ToString()]);
        }

        private void OnFileBlockBeginPacket(Peer peer, byte[] data)
        {
            FileBlockBeginPacket begin = new FileBlockBeginPacket();
            master.Send(peer, begin.Encode());
        }

        private void OnFileBlockDataPacket(Peer peer, byte[] data)
        {
            FileBlockDataPacket d = new FileBlockDataPacket();
            d.Decode(sbuff_[peer.EndPoint.ToString()], 0, sbufflen_[peer.EndPoint.ToString()]);

            Array.Copy(d.data3k, 0, buff_[peer.EndPoint.ToString()], i_[peer.EndPoint.ToString()] * 1024 * 3, d.data3k.Length);
            i_[peer.EndPoint.ToString()]++;

            FileBlockBeginPacket begin2 = new FileBlockBeginPacket();
            master.Send(peer, begin2.Encode());
        }

        private void OnFileBlockEndPacket(Peer peer, byte[] data)
        {
            i_[peer.EndPoint.ToString()] = 0;
            subTask_[peer.EndPoint.ToString()].data = buff_[peer.EndPoint.ToString()];

            var len = subTask_[peer.EndPoint.ToString()].to - subTask_[peer.EndPoint.ToString()].from + 1;
            if (len != CurrentTask.BlockSize)
            {
                subTask_[peer.EndPoint.ToString()].data = new byte[len];
                Array.Copy(buff_[peer.EndPoint.ToString()], 0, subTask_[peer.EndPoint.ToString()].data, 0, len);
            }

            if (!CurrentTask.UpdateTaskState(subTask_[peer.EndPoint.ToString()], 2))
            {
                subTask_[peer.EndPoint.ToString()] = CurrentTask.GetSubTask();
                if (subTask_[peer.EndPoint.ToString()] != null)
                {
                    FileBlockTask f = new FileBlockTask(subTask_[peer.EndPoint.ToString()]);
                    master.Send(peer, f.Encode());
                }
            }
        }
    }
}
