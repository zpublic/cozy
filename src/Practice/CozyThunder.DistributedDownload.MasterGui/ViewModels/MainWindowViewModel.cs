using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using CozyThunder.DistributedDownload.MasterGui.Models;
using CozyThunder.DistributedDownload.MasterGui.Commands;
using System.Windows.Input;

namespace CozyThunder.DistributedDownload.MasterGui.ViewModels
{
    public partial class MainWindowViewModel : BaseViewModel
    {
        public ObservableCollection<PeerInfo> PeerInfoList { get; set; } = new ObservableCollection<PeerInfo>();

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
