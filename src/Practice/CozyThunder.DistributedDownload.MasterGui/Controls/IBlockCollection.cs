using CozyThunder.DistributedDownload.MasterGui.Controls.Block;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyThunder.DistributedDownload.MasterGui.Controls
{
    public interface IBlockCollection
    {
        IBlock GetBlockItem(int index);
    }
}
