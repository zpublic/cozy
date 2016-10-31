using System.Runtime.InteropServices;

namespace CozyDump.Api.Model
{
    public static partial class DumpApiModel
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct MINIDUMP_MODULE
        {
            public ulong BaseOfImage;
            public uint SizeOfImage;
            public uint CheckSum;
            public uint TimeDateStamp;
            public uint ModuleNameRva;
            public VS_FIXEDFILEINFO VersionInfo;
            public MINIDUMP_LOCATION_DESCRIPTOR CvRecord;
            public MINIDUMP_LOCATION_DESCRIPTOR MiscRecord;
            public ulong Reserved0;
            public ulong Reserved1;
        }
    }
}
