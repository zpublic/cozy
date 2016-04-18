using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyThunder.DistributedDownload.MasterGui.Models.Comparer
{
    public class PeerInfoComparer : IEqualityComparer<PeerInfo>
    {
        public static PeerInfoComparer Default { get; set; } = new PeerInfoComparer();

        public bool Equals(PeerInfo x, PeerInfo y)
        {
            return x.Address == y.Address && x.Port == y.Port;
        }

        public int GetHashCode(PeerInfo obj)
        {
            return obj.GetHashCode();
        }
    }
}
