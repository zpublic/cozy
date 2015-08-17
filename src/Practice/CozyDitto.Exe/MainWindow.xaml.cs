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
using CozyDitto.Utils;
using System.Windows.Interop;

namespace CozyDitto.Exe
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += (sender, m) =>
            {
                var handle = new WindowInteropHelper(this).Handle;
                Util.RegisterShowWindowHotKey(handle, Util.KeyModifiers.Ctrl, VirtualKey.VK_F1);

                var source = PresentationSource.FromVisual(this) as HwndSource;
                if (source != null)
                {
                    source.AddHook(WndProc);
                }
            };
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch(msg)
            {
                case Util.WM_HOTKEY:
                    if(wParam.ToInt32() == Util.GetShowWindowHotKeyId())
                    {
                        var handle = new WindowInteropHelper(this).Handle;
                        Util.SetClipboardText(handle, "cozy");
                    }
                    break;
                default:
                    break;
            }
            return IntPtr.Zero;
        }
    }
}
