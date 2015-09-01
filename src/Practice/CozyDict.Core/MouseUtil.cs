using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace CozyDict.Core
{
    public class MouseUtil
    {
        public delegate IntPtr MouseHookCallback(int code, UIntPtr wParam, IntPtr lParam);

        public delegate bool TextOutACallback(IntPtr hdc, int x, int y, IntPtr lpString, int c);

        public delegate bool TextOutWCallback(IntPtr hdc, int x, int y, IntPtr lpString, int c);

        public delegate bool ExtTextOutACallback(IntPtr hdc, int x, int y, uint optionsm, IntPtr rect, IntPtr lpString, uint c, IntPtr dx);

        public delegate bool ExtTextOutWCallback(IntPtr hdc, int x, int y, uint optionsm, IntPtr rect, IntPtr lpString, uint c, IntPtr dx);

        [DllImport(@"CozyDict.Base.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SetMouseHook(MouseHookCallback callback);

        [DllImport(@"CozyDict.Base.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool UnSetMouseHook();

        [DllImport(@"CozyDict.Base.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool InvalidateMouseWindow(int x, int y);

        [DllImport(@"CozyDict.Base.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ResetAPIHookCallback();

        [DllImport(@"CozyDict.Base.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void InitHookEnv();

        [DllImport(@"CozyDict.Base.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SetTextOutAHook(TextOutACallback callback);

        [DllImport(@"CozyDict.Base.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SetTextOutWHook(TextOutWCallback callback);

        [DllImport(@"CozyDict.Base.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SetExtTextOutAHook(ExtTextOutACallback callback);

        [DllImport(@"CozyDict.Base.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SetExtTextOutWHook(ExtTextOutWCallback callback);

        [DllImport(@"CozyDict.Base.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool UnsetAllHook();
    }
}
