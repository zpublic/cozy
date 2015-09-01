using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace CozyDict.Core
{
    public static class OutputUtil
    {
        public delegate bool TextOutACallback(IntPtr hdc, int x, int y, IntPtr lpString, int c);

        public delegate bool TextOutWCallback(IntPtr hdc, int x, int y, IntPtr lpString, int c);

        public delegate bool ExtTextOutACallback(IntPtr hdc, int x, int y, uint optionsm, IntPtr rect, IntPtr lpString, uint c, IntPtr dx);

        public delegate bool ExtTextOutWCallback(IntPtr hdc, int x, int y, uint optionsm, IntPtr rect, IntPtr lpString, uint c, IntPtr dx);

        [DllImport(@"CozyDict.Hook.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ResetAPIHookCallback();

        [DllImport(@"CozyDict.Hook.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void InitHookEnv();

        [DllImport(@"CozyDict.Hook.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SetTextOutAHook(TextOutACallback callback);

        [DllImport(@"CozyDict.Hook.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SetTextOutWHook(TextOutWCallback callback);

        [DllImport(@"CozyDict.Hook.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SetExtTextOutAHook(ExtTextOutACallback callback);

        [DllImport(@"CozyDict.Hook.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SetExtTextOutWHook(ExtTextOutWCallback callback);

        [DllImport(@"CozyDict.Hook.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool UnsetAllHook();
    }
}
