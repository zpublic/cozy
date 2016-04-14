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

        private string _AddressText;
        public string AddressText
        {
            get { return _AddressText; }
            set { Set(ref _AddressText, value); }
        }

        private int _Port;
        public int Port
        {
            get { return _Port; }
            set { Set(ref _Port, value); }
        }

        private PeerInfo _SelectedInfo;
        public PeerInfo SelectedInfo
        {
            get { return _SelectedInfo; }
            set { Set(ref _SelectedInfo, value); }
        }

        private ICommand _AddPeerCommand;
        public ICommand AddPeerCommand
        {
            get
            {
                return _AddPeerCommand = _AddPeerCommand ?? new DelegateCommand(x => 
                {
                    if(CheckAddress(AddressText) && CheckPort(Port))
                    {
                        PeerInfoList.Add(new PeerInfo() { Address = AddressText, Port = Port, });
                    }
                    
                    AddressText = string.Empty;
                    Port        = 0;
                });
            }
        }

        private ICommand _RemovePeerCommand;
        public ICommand RemovePeerCommand
        {
            get
            {
                return _RemovePeerCommand = _RemovePeerCommand ?? new DelegateCommand(x => 
                {
                    if(SelectedInfo != null)
                    {
                        PeerInfoList.Remove(SelectedInfo);
                    }
                });
            }
        }

        public MainWindowViewModel()
        {
            PeerInfoList.Add(new PeerInfo() { Address = "127.0.0.1", Port = 8000, });
        }

        private static bool CheckAddress(string str)
        {
            if (string.IsNullOrEmpty(str)) return false;

            var p = str.Split('.').ToList();
            if (p.Count != 4) return false;

            for(int i = 0; i < 4; ++i)
            {
                int n = 0;
                if(!int.TryParse(p[i], out n))
                {
                    return false;
                }
                if (n < 0 || n > 255) return false;
            }

            return true;
        }

        private static bool CheckPort(int port)
        {
            return !(port < 1024 || port > 65535);
        }
    }
}
