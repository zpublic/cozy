using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CozyPublic.Win
{
    public class MonitorManager
    {
        public static int GetCountDirect()
        {
            return WindowsAPI.GetSystemMetrics(CommonConst.SM_CMONITORS);
        }

        public static Rect GetVirtualDesktopRect()
        {
            return new Rect(
                WindowsAPI.GetSystemMetrics(CommonConst.SM_XVIRTUALSCREEN),
                WindowsAPI.GetSystemMetrics(CommonConst.SM_YVIRTUALSCREEN),
                WindowsAPI.GetSystemMetrics(CommonConst.SM_CXVIRTUALSCREEN) - WindowsAPI.GetSystemMetrics(CommonConst.SM_XVIRTUALSCREEN),
                WindowsAPI.GetSystemMetrics(CommonConst.SM_CYVIRTUALSCREEN) - WindowsAPI.GetSystemMetrics(CommonConst.SM_YVIRTUALSCREEN));
        }

        public void UpdateMonitors()
        {
            FreeMonitors();
            WindowsAPI.EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, delegate(IntPtr hMonitor, IntPtr hdcMonitor, ref RECT lprcMonitor, IntPtr dwData)
            {
                MonitorDevice mi = new MonitorDevice();
                mi.handle = hMonitor;
                mi.index = listMonitors.Count;
                listMonitors.Add(mi);
                return true;
            }, IntPtr.Zero);
        }

        public MonitorDevice GetMonitor(int index)
        {
            return listMonitors[index];
        }

        public MonitorDevice GetPrimaryMonitor()
        {
            return listMonitors.Find(r => r.IsPrimaryMonitor());
        }

        public MonitorDevice GetNearestMonitor(Rect rect)
        {
            IntPtr hMonitor = WindowsAPI.MonitorFromRect(rect, MONITOR_DEFAULTTONEAREST);
            return FindMonitor(hMonitor);
        }

        public MonitorDevice GetNearestMonitor(Point pt)
        {
            IntPtr hMonitor = WindowsAPI.MonitorFromPoint(pt, MONITOR_DEFAULTTONEAREST);
            return FindMonitor(hMonitor);
        }

        public MonitorDevice GetNearestMonitor(IntPtr hWnd)
        {
            IntPtr hMonitor = WindowsAPI.MonitorFromWindow(hWnd, MONITOR_DEFAULTTONEAREST);
            return FindMonitor(hMonitor);
        }

        public bool FullScreenWindow(IntPtr hWnd, bool bFullScreen, int iMonitorIndex = 0)
        {
            return false;
        }
        public bool FullScreenWindow(IntPtr hWndFirst, IntPtr hWndSecond)
        {
            return false; 
        }
        public void CenterWindow(IntPtr hWnd, int iMonitorIndex = 0, bool bUseWorkArea = false)
        {

        }

        private void FreeMonitors()
        {
            listMonitors.Clear();
        }

        private MonitorDevice FindMonitor(IntPtr hMonitor)
        {
            return listMonitors.Find(r => r.handle == hMonitor);
        }

        private List<MonitorDevice> listMonitors = new List<MonitorDevice>();

        private const int MONITOR_DEFAULTTONEAREST = 2;
// 
// private:
//     vector<CMonitor*> m_vec_monitor;
//     struct WndInfo
//     {
//         WndInfo(long lStyle, long lExStyle, RECT rcWnd, bool bFull):
//         _lStyle(lStyle), _lExStyle(lExStyle), _rcWnd(rcWnd), _bFull(bFull){}
//         WndInfo(){};
//         long _lStyle;   ///< 普通样式
//         long _lExStyle; ///< 扩展样式
//         RECT _rcWnd;    ///< 矩形
//         bool _bFull;    ///< 是否已全屏:true是,false否
//     };
//     ///< 保存恢复窗口时的状态
//     map<HWND, WndInfo> m_map_wnd;  */
    }
}
