using CozyThunder.DistributedDownload.MasterGui.Controls.Block.Styles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CozyThunder.DistributedDownload.MasterGui.Controls.Block
{
    /// <summary>
    /// Block.xaml 的交互逻辑
    /// </summary>
    public partial class BlockEntry : UserControl, IBlock
    {
        private BlockStatusColor _StatusColors;
        public BlockStatusColor StatusColors
        {
            get { return _StatusColors; }
            set
            {
                _StatusColors = value;
                UpdateBlock();
            }
        }

        private BlockStatus _Status = BlockStatus.Free;
        public BlockStatus Status
        {
            get { return _Status; }
            set
            {
                _Status = value;
                UpdateBlock();
            }
        }

        private void UpdateBlock()
        {
            Color color;
            switch(Status)
            {
                case BlockStatus.Complete:
                    color = StatusColors.CompleteColor;
                    break;
                case BlockStatus.Downloading:
                    color = StatusColors.DownloadingColor;
                    break;
                case BlockStatus.Failed:
                    color = StatusColors.FailedColor;
                    break;
                case BlockStatus.Free:
                    color = StatusColors.FreeColor;
                    break;
                default:
                    color = Colors.Black;
                    break;
            }

            this.InnerRect.Fill = new SolidColorBrush(color);
        }

        public BlockEntry()
        {
            InitializeComponent();
        }
    }
}
