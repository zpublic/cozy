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
