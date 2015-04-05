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
            WindowsAPI.EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, delegate(
                IntPtr hMonitor,
                IntPtr hdcMonitor,
                ref RECT lprcMonitor,
                IntPtr dwData)
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

        public bool FullScreenWindow(IntPtr hWnd, int iMonitorIndex = 0)
        {
            if (hWnd == IntPtr.Zero || !WindowsAPI.IsWindow(hWnd))
            {
                return false;
            }
            MonitorDevice md = GetMonitor(iMonitorIndex);
            if (md.handle != IntPtr.Zero)
            {
                Rect rc = md.GetMonitorRect();
                WindowsAPI.SetWindowPos(
                    hWnd,
                    IntPtr.Zero,
                    (int)rc.Left,
                    (int)rc.Top,
                    (int)rc.Width,
                    (int)rc.Height,
                    CommonConst.SWP_SHOWWINDOW | CommonConst.SWP_FRAMECHANGED | CommonConst.SWP_NOZORDER);
                return true;
            }
            return false;
        }

        public bool CenterWindow(IntPtr hWnd, int iMonitorIndex = 0, bool bUseWorkArea = false)
        {
            RECT srect = new RECT();
            WindowsAPI.GetWindowRect(hWnd, ref srect);
            Rect rect = new Rect(srect.left, srect.top, srect.right - srect.left, srect.bottom - srect.top);

            if (0 > iMonitorIndex)
            {
                rect = CenterWindowToAll(rect, bUseWorkArea);
            }
            else if (0 == iMonitorIndex)
            {
                MonitorDevice pMonitor = GetNearestMonitor(hWnd);
                if (pMonitor.handle == IntPtr.Zero)
                {
                    return false;
                }
                rect = pMonitor.CenterRectToMonitor(rect, bUseWorkArea);
            }
            else
            {
                MonitorDevice pMonitor = GetMonitor(iMonitorIndex);
                if (pMonitor.handle == IntPtr.Zero)
                {
                    return false;
                }
                rect = pMonitor.CenterRectToMonitor(rect, bUseWorkArea);
            }
            return WindowsAPI.SetWindowPos(
                hWnd,
                IntPtr.Zero,
                (int)rect.Left,
                (int)rect.Top,
                0,
                0,
                CommonConst.SWP_NOSIZE | CommonConst.SWP_NOZORDER);
        }

        private Rect CenterWindowToAll(Rect rect, bool bUseWorkArea = false)
        {
            if (bUseWorkArea)
            {
                Rect rcWork = GetPrimaryMonitor().GetWorkAreaRect();
                Rect rcAll = GetVirtualDesktopRect();
                double Width = rect.Right - rect.Left;
                double Height = rect.Bottom - rect.Top;
                return new Rect(
                    rcWork.Left + (rcAll.Right - rcAll.Left - Width) / 2,
                    rcWork.Top + (rcAll.Bottom - rcAll.Top - Height) / 2,
                    Width,
                    Height);
            }
            else
            {
                Rect rcAll = GetVirtualDesktopRect();
                double Width = rect.Right - rect.Left;
                double Height = rect.Bottom - rect.Top;
                return new Rect(
                    rcAll.Left + (rcAll.Right - rcAll.Left - Width) / 2,
                    rcAll.Top + (rcAll.Bottom - rcAll.Top - Height) / 2,
                    Width,
                    Height);
            }
        }

        private void FreeMonitors()
        {
            listMonitors.Clear();
        }

        private MonitorDevice FindMonitor(IntPtr hMonitor)
        {
            return listMonitors.Find(r => r.handle == hMonitor);
        }

        ///> 以后可能做全屏之后的恢复
        //         private class WndInfo
        //         {
        //             public WndInfo(bool f, int s, int s2, RECT rc)
        //             {
        //                 bfull = f;
        //                 style = s;
        //                 exstyle = s2;
        //                 rcWnd = rc;
        //             }
        //             public bool bfull { get; set; }
        //             public int style { get; set; }
        //             public int exstyle { get; set; }
        //             RECT rcWnd { get; set; }
        //         }
        //         private Dictionary<IntPtr, WndInfo> mapWnd = new Dictionary<IntPtr, WndInfo>();
        private List<MonitorDevice> listMonitors = new List<MonitorDevice>();

        private const int MONITOR_DEFAULTTONEAREST = 2;
    }
}
