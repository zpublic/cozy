using CozyPublic.Win;
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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CozyDisplayFusion
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int HOTKEY_ID_A       = 1;
        private const int WM_HOTKEY         = 0x0312;

        private MonitorManager monitorMgr = new MonitorManager();

        public MainWindow()
        {
            InitializeComponent();
            monitorMgr.UpdateMonitors();
        }

        IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handle)
        {
            if (msg == WM_HOTKEY)
            {
                if (wParam.ToInt32() == HOTKEY_ID_A)
                {
                    IntPtr hWnd = WindowsAPI.GetForegroundWindow();
                    WindowsAPI.MoveWindow(hWnd, 0, 0, 100, 100, true);
                }
            }
            return IntPtr.Zero;
        }

        private void OnSourceInitialized(object sender, EventArgs e)
        {
            IntPtr handle = new WindowInteropHelper(this).Handle;
            HotKey.RegisterHotKey(
                handle,
                HOTKEY_ID_A,
                ModifierKeys.Control | ModifierKeys.Alt | ModifierKeys.Shift,
                KeyInterop.VirtualKeyFromKey(Key.C));
            HwndSource source = PresentationSource.FromVisual(this) as HwndSource;
            source.AddHook(WndProc);
        }
    }
}
