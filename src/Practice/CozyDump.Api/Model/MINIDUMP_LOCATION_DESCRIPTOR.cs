using System.Runtime.InteropServices;

namespace CozyDump.Api.Model
{
    public static partial class DumpApiModel
    {
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct MINIDUMP_LOCATION_DESCRIPTOR
        {
            public uint DataSize;
            public uint Rva; /// relative byte offset
            public override string ToString()
            {
                return string.Format(
                    "Off={0:x8} Sz={1:x8}",
                    Rva,
                    DataSize);
            }
        }
    }
}
