using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyThunder.DistributedDownload.MasterGui.Models
{
    public class PeerInfo : ObservableCollection<PeerInfo> 
    {
        public string Address { get; set; }
        public int Port { get; set; }
        public PeerStatus Status { get; set; }
        public PeerRange Range { get; set; }
    }
}
