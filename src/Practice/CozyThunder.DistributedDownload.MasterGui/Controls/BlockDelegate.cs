using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyThunder.DistributedDownload.MasterGui.Controls.Block
{
    public class BlockDelegate
    {
        public IBlockCollection Target { get; set; }

        public IBlock this[int index]
        {
            get
            {
                if (Target == null) return null;

                return Target?.GetBlockItem(index);
            }
        }
    }
}
