using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CozyPublic.Win
{
    public class MonitorDevice
    {
        private IntPtr handle_;
        public string name { get; set; }
        public int index { get; set; }
        public IntPtr handle
        {
            get
            {
                return this.handle_;
            }
            set
            {
                this.handle_ = value;
                MONITORINFOEX mi = new MONITORINFOEX();
                mi.cbSize = Marshal.SizeOf(mi);
                if (WindowsAPI.GetMonitorInfo(handle, ref mi))
                {
                    this.name = mi.szDevice;
                }
            }
        }

        public Rect GetMonitorRect()
        {
            MONITORINFO mi = new MONITORINFO();
            mi.cbSize = Marshal.SizeOf(mi);
            if (WindowsAPI.GetMonitorInfo(handle, ref mi))
            {
                Rect rc = new Rect(
                    mi.rcMonitor.left,
                    mi.rcMonitor.top,
                    mi.rcMonitor.right - mi.rcMonitor.left,
                    mi.rcMonitor.bottom - mi.rcMonitor.top);
                return rc;
            }
            return new Rect();
        }

        public Rect GetWorkAreaRect()
        {
            MONITORINFO mi = new MONITORINFO();
            mi.cbSize = Marshal.SizeOf(mi);
            if (WindowsAPI.GetMonitorInfo(handle, ref mi))
            {
                Rect rc = new Rect(
                    mi.rcWork.left,
                    mi.rcWork.top,
                    mi.rcWork.right - mi.rcWork.left,
                    mi.rcWork.bottom - mi.rcWork.top);
                return rc;
            }
            return new Rect();
        }

        public int GetPixelWidth()
        {
            DEVMODE devmode = new DEVMODE();
            devmode.dmSize = Marshal.SizeOf(devmode);
            WindowsAPI.EnumDisplaySettings(
                name,
                CommonConst.ENUM_CURRENT_SETTINGS,
                ref devmode);
            return devmode.dmPelsWidth;
        }

        public int GetPixelHeight()
        {
            DEVMODE devmode = new DEVMODE();
            devmode.dmSize = Marshal.SizeOf(devmode);
            WindowsAPI.EnumDisplaySettings(
                name,
                CommonConst.ENUM_CURRENT_SETTINGS,
                ref devmode);
            return devmode.dmPelsHeight;
        }

        public int GetBitsPerPixel()
        {
            DEVMODE devmode = new DEVMODE();
            devmode.dmSize = Marshal.SizeOf(devmode);
            WindowsAPI.EnumDisplaySettings(
                name,
                CommonConst.ENUM_CURRENT_SETTINGS,
                ref devmode);
            return devmode.dmBitsPerPel;
        }

        public int GetMonitorLeft()
        {
            DEVMODE devmode = new DEVMODE();
            devmode.dmSize = Marshal.SizeOf(devmode);
            WindowsAPI.EnumDisplaySettings(
                name,
                CommonConst.ENUM_CURRENT_SETTINGS,
                ref devmode);
            return (int)devmode.dmPosition.X;
        }

        public int GetMonitorTop()
        {
            DEVMODE devmode = new DEVMODE();
            devmode.dmSize = Marshal.SizeOf(devmode);
            WindowsAPI.EnumDisplaySettings(
                name,
                CommonConst.ENUM_CURRENT_SETTINGS,
                ref devmode);
            return (int)devmode.dmPosition.Y;
        }

        public int GetDisplayFrequency()
        {
            DEVMODE devmode = new DEVMODE();
            devmode.dmSize = Marshal.SizeOf(devmode);
            WindowsAPI.EnumDisplaySettings(
                name,
                CommonConst.ENUM_CURRENT_SETTINGS,
                ref devmode);
            return devmode.dmDisplayFrequency;
        }

        public bool IsPrimaryMonitor()
        {
            MONITORINFO mi = new MONITORINFO();
            mi.cbSize = Marshal.SizeOf(mi);
            if (WindowsAPI.GetMonitorInfo(handle, ref mi))
            {
                return mi.dwFlags == MONITORINFOF_PRIMARY;
            }
            return false;
        }

        protected Rect CenterRectToMonitor(Rect lprc, bool bWorkArea = false)
        {
            double w = lprc.Right - lprc.Left;
            double h = lprc.Bottom - lprc.Top;

            Rect rect;
            if (bWorkArea)
            {
                rect = GetWorkAreaRect();
            }
            else
            {
                rect = GetMonitorRect();
            }
            return new Rect(
                rect.Left + (rect.Right - rect.Left - w) / 2,
                rect.Top + (rect.Bottom - rect.Top - h) / 2,
                w,
                h);
        }

        private const int MONITORINFOF_PRIMARY = 1;
    }
}
