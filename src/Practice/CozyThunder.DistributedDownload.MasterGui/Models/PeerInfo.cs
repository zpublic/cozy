using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyThunder.DistributedDownload.MasterGui.Models
{
    public class PeerInfo
    {
        public string Address { get; set; }
        public int Port { get; set; }
        public PeerStatus Status { get; set; }
        public PeerRange Range { get; set; }
    }
}
