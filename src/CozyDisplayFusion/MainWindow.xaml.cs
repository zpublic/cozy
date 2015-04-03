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
        private const int HOTKEY_ID_A = 1;
        private const int HOTKEY_ID_B = 2;
        private const int HOTKEY_ID_C = 3;
        private const int WM_HOTKEY = 0x0312;

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
                int i = wParam.ToInt32();
                if (i == HOTKEY_ID_A)
                {
                    IntPtr hWnd = WindowsAPI.GetForegroundWindow();
                    monitorMgr.CenterWindow(hWnd);
                }
                else if (i == HOTKEY_ID_B)
                {
                    IntPtr hWnd = WindowsAPI.GetForegroundWindow();
                    monitorMgr.FullScreenWindow(hWnd);
                }
                else if (i == HOTKEY_ID_C)
                {
                    IntPtr hWnd = WindowsAPI.GetForegroundWindow();
                    monitorMgr.FullScreenWindow(hWnd, 1);
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
            HotKey.RegisterHotKey(
                handle,
                HOTKEY_ID_B,
                ModifierKeys.Control | ModifierKeys.Alt | ModifierKeys.Shift,
                KeyInterop.VirtualKeyFromKey(Key.X));
            HotKey.RegisterHotKey(
               handle,
               HOTKEY_ID_C,
               ModifierKeys.Control | ModifierKeys.Alt | ModifierKeys.Shift,
               KeyInterop.VirtualKeyFromKey(Key.Z));
            HwndSource source = PresentationSource.FromVisual(this) as HwndSource;
            source.AddHook(WndProc);
        }
    }
}
