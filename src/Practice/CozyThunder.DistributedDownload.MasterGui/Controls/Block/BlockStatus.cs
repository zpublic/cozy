using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyThunder.DistributedDownload.MasterGui.Controls.Block
{
    public enum BlockStatus
    {
        Unknow = 0,
        Free = 1,
        Downloading = 2,
        Failed = 3,
        Complete = 4,
    }
}
