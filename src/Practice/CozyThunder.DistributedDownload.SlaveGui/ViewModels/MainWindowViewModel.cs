using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

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

    }
}
