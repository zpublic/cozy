using CozyThunder.Botnet.Common;
using CozyThunder.DistributedDownload.MasterGui.Controls.Block;
using CozyThunder.DistributedDownload.MasterGui.MessageCenter;
using CozyThunder.DistributedDownload.MasterGui.Models;
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
        private string _CurrentRemotePath;
        public string CurrentRemotePath
        {
            get { return _CurrentRemotePath; }
            set { Set(ref _CurrentRemotePath, value); }
        }

        private BlockDelegate _Blocks = new BlockDelegate();
        public BlockDelegate Blocks
        {
            get { return _Blocks; }
            set { Set(ref _Blocks, value); }
        }

        public DownloadTask CurrentTask { get; set; }

        private object lockObject = new object();
        public void OnCreateTask()
        {
            var task = new DownloadTaskInfo();
            GlobalMessageCenter.Instance.Send("CreateDownloadTask", task);

            if(task.IsValid)
            {
                var filesize = HttpDownload.HttpFileSplit.TryGetContentLength(task.RemotePath);
                if (filesize <= 0)
                {
                    GlobalMessageCenter.Instance.Send("TaskError.Size");
                    return;
                }

                var blockSize = (filesize + 224) / 225;
                var downloadTask = new DownloadTask()
                {
                    RemotePath = task.RemotePath,
                    LocalPath = task.LocalPath,
                    CfgPath = task.LocalPath + ".cfg",
                    BlockSize = blockSize,
                };

                if (downloadTask.InitTask())
                {
                    CurrentTask = downloadTask;
                    CurrentRemotePath = CurrentTask.RemotePath;
                }

                ClearProgress();

                if(task.IsEnableDistributed)
                {
                    foreach (var info in PeerInfoList)
                    {
                        var peer = PeerInfo2Peer(info);
                        var subtask = CurrentTask.GetSubTask();
                        InstPeer(peer, subtask, blockSize);

                        if (subTask_[peer.EndPoint.ToString()] != null)
                        {
                            FileBlockTask f = new FileBlockTask(subTask_[peer.EndPoint.ToString()]);
                            master.Send(peer, f.Encode());
                        }
                    }
                }
            }
        }

        private void InstPeer(Peer peer, DownloadSubTask task, long blockSize)
        {
            var temp = peer.EndPoint.ToString();
            if (!subTask_.ContainsKey(temp))
            {
                subTask_.Add(temp, task);
            }

            if (!msgbuff.ContainsKey(temp))
            {
                msgbuff.Add(temp, new Common.MessageBuffer());
            }

            if (!sbuff_.ContainsKey(temp))
            {
                sbuff_.Add(temp, new Common.MessageBuffer());
            }
        }

        private void UninstPeer(Peer peer)
        {
            var temp = peer.EndPoint.ToString();
            if (subTask_.ContainsKey(temp))
            {
                subTask_.Remove(temp);
            }

            if (msgbuff.ContainsKey(temp))
            {
                msgbuff.Remove(temp);
            }

            if (sbuff_.ContainsKey(temp))
            {
                sbuff_.Remove(temp);
            }
        }

        public void OnPauseTask()
        {

        }

        public void OnResumeTask()
        {

        }

        public void OnCalcleTask()
        {

        }

        public void OnGlobalSetting()
        {
            GlobalMessageCenter.Instance.Send("GlobalSetting");
        }
    }
}
