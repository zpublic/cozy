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
using CozyThunder.DistributedDownload.MasterGui.Controls.Styles;

namespace CozyThunder.DistributedDownload.MasterGui.Controls
{
    /// <summary>
    /// BlockControl.xaml 的交互逻辑
    /// </summary>
    public partial class BlockControl : UserControl
    {
        public static readonly DependencyProperty ItemCountProperty = DependencyProperty.Register("ItemCount", typeof(int), typeof(BlockControl), 
            new PropertyMetadata(0, new PropertyChangedCallback(OnItemCountChanged)));

        public static readonly DependencyProperty BlockStyleProperty = DependencyProperty.Register("BlockStyle", typeof(BlockVisualStyle), typeof(BlockControl),
           new PropertyMetadata(null, new PropertyChangedCallback(OnBlockStyleChanged)));

        static void OnItemCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = d as BlockControl;
            if(obj != null)
            {
                obj.OnItemCountChanged((int)e.NewValue);
            }
        }

        static void OnBlockStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = d as BlockControl;
            if (obj != null)
            {
                obj.OnBlockStyleChanged((BlockVisualStyle)e.NewValue);
            }
        }

        void OnItemCountChanged(int value)
        {
            int v = this.BlockGrid.Children.Count;
            if (value > v)
            {
                BlockVisualStyle style = BlockStyle ?? BlockVisualStyle.DefaultStyle;
                for (int i = 0; i < value - v; ++i)
                {
                    var ctrl = new Block();
                    ctrl.Margin = new Thickness(2);
                    style.UseStyle(ctrl);
                    this.BlockGrid.Children.Add(ctrl);
                }
            }
            else if(value < v)
            {
                var length = v - value;
                this.BlockGrid.Children.RemoveRange(this.BlockGrid.Children.Count - length, length);
            }
        }

        public void OnBlockStyleChanged(BlockVisualStyle style)
        {
            foreach(Block obj in this.BlockGrid.Children)
            {
                style.UseStyle(obj);
            }
        }

        public int ItemCount
        {
            get
            {
                return (int)this.GetValue(ItemCountProperty);
            }
            set
            {
                this.SetValue(ItemCountProperty, value);
            }
        }

        public BlockVisualStyle BlockStyle
        {
            get
            {
                return (BlockVisualStyle)this.GetValue(BlockStyleProperty);
            }
            set
            {
                this.SetValue(BlockStyleProperty, value);
            }
        }

        public BlockControl()
        {
            InitializeComponent();
        }
    }
}
