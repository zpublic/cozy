using CozyThunder.Botnet.Common;
using CozyThunder.DistributedDownload.MasterGui.Common;
using CozyThunder.DistributedDownload.MasterGui.MessageCenter;
using CozyThunder.DistributedDownload.MasterGui.Models;
using CozyThunder.Protocol;
using CozyThunder.Protocol.FileBlock;
using CozyThunder.Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CozyThunder.DistributedDownload.MasterGui.ViewModels
{
    public partial class MainWindowViewModel
    {
        
        private Dictionary<string, MessageBuffer> msgbuff = new Dictionary<string, MessageBuffer>();
        private Dictionary<string, DownloadSubTask> subTask_ = new Dictionary<string, DownloadSubTask>();


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
            sbuff_[peer.EndPoint.ToString()].Clear();
        }

        private void OnStringPacket(Peer peer, byte[] data)
        {
            StringPacket packet = new StringPacket();
            packet.Decode(sbuff_[peer.EndPoint.ToString()].RawData, 0, sbuff_[peer.EndPoint.ToString()].Length);
        }

        private void OnFileBlockBeginPacket(Peer peer, byte[] data)
        {
            FileBlockBeginPacket begin = new FileBlockBeginPacket();
            master.Send(peer, begin.Encode());

            var taskid = subTask_[peer.EndPoint.ToString()].Id;
            Action act = () =>
            {
                this.Blocks[taskid].Status = Controls.Block.BlockStatus.Downloading;
            };
            GlobalMessageCenter.Instance.Send("MainWindow.UIThreadInvoke", act);
        }

        private void OnFileBlockDataPacket(Peer peer, byte[] data)
        {
            FileBlockDataPacket d = new FileBlockDataPacket();
            d.Decode(sbuff_[peer.EndPoint.ToString()].RawData, 0, sbuff_[peer.EndPoint.ToString()].Length);

            msgbuff[peer.EndPoint.ToString()].Append(d.data3k, d.len);

            FileBlockBeginPacket begin2 = new FileBlockBeginPacket();
            master.Send(peer, begin2.Encode());
        }

        private void OnFileBlockEndPacket(Peer peer, byte[] data)
        {
            subTask_[peer.EndPoint.ToString()].data = msgbuff[peer.EndPoint.ToString()].RawData;

            var len = subTask_[peer.EndPoint.ToString()].to - subTask_[peer.EndPoint.ToString()].from + 1;
            if (len != CurrentTask.BlockSize)
            {
                subTask_[peer.EndPoint.ToString()].data = new byte[len];
                Array.Copy(msgbuff[peer.EndPoint.ToString()].RawData, 0, subTask_[peer.EndPoint.ToString()].data, 0, len);
            }

            SetPeerInfoStatus(peer, PeerStatus.Free);
            AddProgress();

            var taskid = subTask_[peer.EndPoint.ToString()].Id;
            Action act = () =>
            {
                this.Blocks[taskid].Status = Controls.Block.BlockStatus.Complete;
            };
            GlobalMessageCenter.Instance.Send("MainWindow.UIThreadInvoke", act);

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
