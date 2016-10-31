using System.Runtime.InteropServices;

namespace CozyDump.Api.Model
{
    public static partial class DumpApiModel
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct MINIDUMP_MODULE_LIST
        {
            public uint NumberOfModules;
            public MINIDUMP_MODULE[] Modules;
        }
    }
}
