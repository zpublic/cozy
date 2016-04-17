using CozyThunder.DistributedDownload.MasterGui.Controls.Block;
using CozyThunder.DistributedDownload.MasterGui.MessageCenter;
using CozyThunder.DistributedDownload.MasterGui.Models;
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

        public void OnCreateTask()
        {
            var task = new DownloadTaskInfo();
            GlobalMessageCenter.Instance.Send("CreateDownloadTask", task);
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

        public void OnEnableDistributedCommand()
        {

        }

        public void OnDisableDistributedCommand()
        {

        }

        public void OnGlobalSetting()
        {
            GlobalMessageCenter.Instance.Send("GlobalSetting");
        }
    }
}
