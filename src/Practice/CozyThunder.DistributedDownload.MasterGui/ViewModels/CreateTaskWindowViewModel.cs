using CozyThunder.DistributedDownload.MasterGui.Commands;
using CozyThunder.DistributedDownload.MasterGui.MessageCenter;
using CozyThunder.DistributedDownload.MasterGui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CozyThunder.DistributedDownload.MasterGui.ViewModels
{
    public class CreateTaskWindowViewModel : BaseViewModel
    {
        private string _RemotePath = @"http://speed.myzone.cn/pc_elive_1.1.rar";
        public string RemotePath
        {
            get { return _RemotePath; }
            set { Set(ref _RemotePath, value); }
        }

        private string _LocalPath;
        public string LocalPath
        {
            get { return _LocalPath; }
            set { Set(ref _LocalPath, value); }
        }

        private bool _IsEnableDistributed;
        public bool IsEnableDistributed
        {
            get { return _IsEnableDistributed; }
            set { Set(ref _IsEnableDistributed, value); }
        }

        private ICommand _SubmitCommand;
        public ICommand SubmitCommand
        {
            get
            {
                return _SubmitCommand = _SubmitCommand ?? new DelegateCommand(x => 
                {
                    var task = new DownloadTaskInfo()
                    {
                        RemotePath = RemotePath,
                        LocalPath = LocalPath,
                        IsEnableDistributed = IsEnableDistributed,
                    };
                    GlobalMessageCenter.Instance.Send("CreateTaskWindw.Submit", task);
                });
            }
        }
    }
}
