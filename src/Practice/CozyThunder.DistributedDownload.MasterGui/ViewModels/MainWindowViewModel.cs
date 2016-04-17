using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using CozyThunder.DistributedDownload.MasterGui.Models;
using CozyThunder.DistributedDownload.MasterGui.Commands;
using System.Windows.Input;
using CozyThunder.DistributedDownload.MasterGui.Controls.Block;

namespace CozyThunder.DistributedDownload.MasterGui.ViewModels
{
    public partial class MainWindowViewModel : BaseViewModel
    {
        public ObservableCollection<PeerInfo> PeerInfoList { get; set; } = new ObservableCollection<PeerInfo>();

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

        public MainWindowViewModel()
        {
            PeerInfoList.Add(new PeerInfo()
            {
                Address = "127.0.0.1",
                Port = 8000,
                Status = PeerStatus.Downloading,
                Range = new PeerRange() {From = 0, To = 1024 * 3, }
            });
        }
    }
}
