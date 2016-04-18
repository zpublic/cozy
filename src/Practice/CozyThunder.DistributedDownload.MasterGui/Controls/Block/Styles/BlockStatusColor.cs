using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CozyThunder.DistributedDownload.MasterGui.Controls.Block.Styles
{
    public class BlockStatusColor
    {
        public Color DownloadingColor { get; set; }
        public Color FreeColor { get; set; }
        public Color FailedColor { get; set; }
        public Color CompleteColor { get; set; }
    }
}
