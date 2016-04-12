using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyThunder.DistributedDownload.SlaveGui.Commands;
using System.Windows.Input;
using CozyThunder.Botnet.Slave;
using CozyThunder.DistributedDownload.SlaveGui.ViewModels.Slave;
using CozyThunder.DistributedDownload.SlaveGui.Log;

namespace CozyThunder.DistributedDownload.SlaveGui.ViewModels
{
    public partial class MainWindowViewModel : BaseViewModel
    {
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

        public MainWindowViewModel()
        {
            InitLog();
        }

        private SlavePeer peer = new SlavePeer();

        private void OnSwitchStatus()
        {
            if (ClientState)
            {
                ClientState = false;
                SwitchButtonText = "开启";
                OnStopClient();
                LogManager.Instalce.TaskEndLog();
            }
            else
            {
                ClientState = true;
                SwitchButtonText = "关闭";
                OnStartClient();
                LogManager.Instalce.TaskBeginLog();
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
