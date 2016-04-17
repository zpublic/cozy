using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyThunder.DistributedDownload.MasterGui.Models
{
    public class PeerRange
    {
        public static PeerRange Empty { get; } = new PeerRange() { From = 0, To = 0, };

        public int From { get; set; }

        public int To { get; set; }

        public override string ToString()
        {
            return string.Format("[{0} : {1}]", From, To);
        }
    }
}
