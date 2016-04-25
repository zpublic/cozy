using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Net;
using CozyThunder.DistributedDownload.MasterGui.Models.Listener;
using System.Threading;

namespace CozyThunder.DistributedDownload.MasterGui.ViewModels
{
    public partial class MainWindowViewModel : BaseViewModel
    {
        public MainWindowViewModel()
        {
            master.Start(IPAddress.Any, 48234, new MainMasterListener());
            RegistMessage();
        }

        private void OnClear()
        {
            master.Stop();
            UnregistMessage();
        }

        private int _ProgressValue;
        public int ProgressValue
        {
            get { return _ProgressValue; }
            set { Set(ref _ProgressValue, value); }
        }

        private void AddProgress()
        {
            ProgressValue++;
        }

        private void ClearProgress()
        {
            ProgressValue = 0;
        }
    }
}
