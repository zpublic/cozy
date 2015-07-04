using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace ProcessTester.Ext
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
