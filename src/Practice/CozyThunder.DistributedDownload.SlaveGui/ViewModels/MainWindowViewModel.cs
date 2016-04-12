using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using CozyThunder.DistributedDownload.SlaveGui.Commands;
using System.Windows.Input;
using CozyThunder.Botnet.Slave;
using CozyThunder.DistributedDownload.SlaveGui.ViewModels.Slave;

namespace CozyThunder.DistributedDownload.SlaveGui.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        public ObservableCollection<string> LogCollection { get; set; } = new ObservableCollection<string>();

        private bool _ClientState = false;
        public bool ClientState
        {
            get { return _ClientState; }
            set { Set(ref _ClientState, value); }
        }

        private ICommand _SwitchButtonCommand;
        public ICommand SwitchButtonCommand
        {
            get
            {
                return _SwitchButtonCommand = _SwitchButtonCommand ?? new DelegateCommand(x => 
                {
                    OnSwitchStatus();
                });
            }
        }

        private string _SwitchButtonText = "开启";
        public string SwitchButtonText
        {
            get { return _SwitchButtonText; }
            set { Set(ref _SwitchButtonText, value); }
        }

        private int _Port = 48234;
        public int Port
        {
            get { return _Port; }
            set { Set(ref _Port, value); }
        }

        private SlavePeer peer = new SlavePeer();

        private void OnSwitchStatus()
        {
            if (ClientState)
            {
                ClientState = false;
                SwitchButtonText = "开启";
                OnStopClient();
            }
            else
            {
                ClientState = true;
                SwitchButtonText = "关闭";
                OnStartClient();
            }
        }

        private void OnStartClient()
        {
            peer.Start(System.Net.IPAddress.Any, Port, new SlaveConnectorListener(peer));
        }

        private void OnStopClient()
        {
            peer.Stop();
        }
    }
}
