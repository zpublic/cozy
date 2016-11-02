using System.Runtime.InteropServices;

namespace CozyDump.Api.Model
{
    public static partial class DumpApiModel
    {
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct MINIDUMP_MEMORY_DESCRIPTOR
        {
            public ulong StartOfMemoryRange;
            public MINIDUMP_LOCATION_DESCRIPTOR Memory;
        }
    }
}
