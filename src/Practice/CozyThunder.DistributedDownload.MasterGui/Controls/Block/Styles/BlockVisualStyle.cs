using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CozyThunder.DistributedDownload.MasterGui.Controls.Block.Styles
{
    public class BlockVisualStyle
    {
        public static BlockVisualStyle DefaultStyle = new BlockVisualStyle()
        {
            StatusColors        = new BlockStatusColor()
            {
                FreeColor           = Colors.WhiteSmoke,
                FailedColor         = Colors.Red,
                CompleteColor       = Colors.Green,
                DownloadingColor    = Colors.Blue,
            },

            Margin              = new Thickness(5),
        };

        public BlockStatusColor StatusColors { get; set; }
        public Thickness Margin { get; set; }

        public void UseStyle(BlockEntry target)
        {
            target.StatusColors = this.StatusColors;
            target.Margin       = this.Margin;
        }
    }
}
