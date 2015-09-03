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
        public delegate int IPCCallback(IntPtr lpString, uint dwPid);

        [DllImport(@"CozyDict.Base.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint GetMouseWindowPid(int x, int y);

        [DllImport(@"CozyDict.Base.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SetIPCCallback(IPCCallback callback);

        [DllImport(@"CozyDict.Hook.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SetCBTHook();

        [DllImport(@"CozyDict.Hook.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool UnSetCBTHook();

        [DllImport(@"CozyDict.Base.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool StartPipe();

        [DllImport(@"CozyDict.Base.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool StopPipe();

        [DllImport(@"CozyDict.Hook.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool UnsetAllHook();
    }
}
