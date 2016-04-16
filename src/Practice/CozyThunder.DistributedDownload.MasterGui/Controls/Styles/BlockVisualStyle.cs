using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CozyThunder.DistributedDownload.MasterGui.Controls.Styles
{
    public class BlockVisualStyle
    {
        public static BlockVisualStyle DefaultStyle = new BlockVisualStyle()
        {
            BlockColor  = Colors.WhiteSmoke,
            Margin      = new Thickness(5),
        };

        public Color BlockColor { get; set; }

        public Thickness Margin { get; set; }

        public void UseStyle(Block target)
        {
            target.SetVisualStyle(this);
        }
    }
}
