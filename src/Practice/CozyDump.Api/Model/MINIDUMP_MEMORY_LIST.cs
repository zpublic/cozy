using System.Runtime.InteropServices;

namespace CozyDump.Api.Model
{
    public static partial class DumpApiModel
    {
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct MINIDUMP_MEMORY_LIST
        {
            public uint NumberOfMemoryRanges;
            public MINIDUMP_MEMORY_DESCRIPTOR[] MemoryRanges;
        }
    }
}
