using System.Runtime.InteropServices;

namespace CozyDump.Api.Model
{
    public static partial class DumpApiModel
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct MINIDUMP_THREAD
        {
            public uint ThreadId;
            public uint SuspendCount;
            public uint PriorityClass;
            public uint Priority;
            public ulong Teb;
            public MINIDUMP_MEMORY_DESCRIPTOR Stack;
            public MINIDUMP_LOCATION_DESCRIPTOR ThreadContext;
        }
    }
}
