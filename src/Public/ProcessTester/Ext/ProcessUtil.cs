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
        public delegate void ProcessEnumFunc(uint pid);

        [DllImport(@"../../ProcessUtilCpp/Debug/ProcessUtilCpp.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ProcessEnum(ProcessEnumFunc func);
    }
}
