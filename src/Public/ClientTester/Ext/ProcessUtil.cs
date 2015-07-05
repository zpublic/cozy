using System;
using System.Runtime.InteropServices;

namespace ClientTester.Ext
{
    public static class ProcessUtil
    {
        public delegate bool ProcessEnumFunc(uint pid);

        public delegate void GetProcessNameFunc(IntPtr ptr);

        [DllImport(@"../../ProcessUtilCpp/Debug/ProcessUtilCpp.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint ProcessEnum(ProcessEnumFunc func);

        [DllImport(@"../../ProcessUtilCpp/Debug/ProcessUtilCpp.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool GetProcessName(uint Pid, GetProcessNameFunc func);
    }
}