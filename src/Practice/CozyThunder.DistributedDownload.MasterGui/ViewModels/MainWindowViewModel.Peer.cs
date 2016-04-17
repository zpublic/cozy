using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyThunder.DistributedDownload.MasterGui.ViewModels
{
    public partial class MainWindowViewModel
    {
        private string _Address;
        public string Address
        {
            get { return _Address; }
            set { Set(ref _Address, value); }
        }

        private string _Port;
        public string Port
        {
            get { return _Port; }
            set { Set(ref _Port, value); }
        }
    }
}
