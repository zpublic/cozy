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
        [DllImport(@"CozyDict.Base.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool StartPipe();

        [DllImport(@"CozyDict.Base.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool StopPipe();

        [DllImport(@"CozyDict.Hook.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void InitHookEnv();

        [DllImport(@"CozyDict.Hook.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SetTextOutAHook();

        [DllImport(@"CozyDict.Hook.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SetTextOutWHook();

        [DllImport(@"CozyDict.Hook.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SetExtTextOutAHook();

        [DllImport(@"CozyDict.Hook.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SetExtTextOutWHook();

        [DllImport(@"CozyDict.Hook.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool UnsetAllHook();
    }
}
