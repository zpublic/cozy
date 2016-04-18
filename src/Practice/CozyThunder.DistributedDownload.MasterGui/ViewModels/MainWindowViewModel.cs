using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Net;
using CozyThunder.DistributedDownload.MasterGui.Models.Listener;

namespace CozyThunder.DistributedDownload.MasterGui.ViewModels
{
    public partial class MainWindowViewModel : BaseViewModel
    {
        public MainWindowViewModel()
        {
            master.Start(IPAddress.Any, 48360, new MainMasterListener());
            RegistMessage();
        }

        private void OnClear()
        {
            master.Stop();
            UnregistMessage();
        }
    }
}
