using CozyThunder.DistributedDownload.MasterGui.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CozyThunder.DistributedDownload.MasterGui.Models
{
    public class PeerInfo : NotifyObject
    {
        private string _Address;
        public string Address
        {
            get { return _Address; }
            set { Set(ref _Address, value); }
        }
        private int _Port;
        public int Port
        {
            get { return _Port; }
            set { Set(ref _Port, value); }
        }
        private PeerStatus _Status;
        public PeerStatus Status
        {
            get { return _Status; }
            set { Set(ref _Status, value); }
        }
        private PeerRange _Range;
        public PeerRange Range
        {
            get { return _Range; }
            set { Set(ref _Range, value); }
        }
    }
}
