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
using System.Threading;

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

            var hidewindowthread = new Thread(new ThreadStart(() => { Util.EnterMessageLoop(); }));
            this.Loaded += (sender, m) =>
            {
                Util.CreateHideMessageWindow();
                Util.RegisterHotKeyWithName("SetClipboard", Util.KeyModifiers.Ctrl, VirtualKey.VK_F1);
                Util.SetHotKeyCallback((x)=>
                {
                    if(x == Util.GetHotKeyIdWithName("SetClipboard"))
                    {
                        Util.SetClipboardText("cozy zui diao");
                        return true;
                    }
                    return false;
                });

                hidewindowthread.Start();
            };

            this.Closed += (sender, m) =>
            {
                Util.UnregisterHotKeyWithName("SetClipboard");
                hidewindowthread.Abort();
            };
        }
    }
}
