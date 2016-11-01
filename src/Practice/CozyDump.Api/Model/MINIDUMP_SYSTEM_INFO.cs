using System.Runtime.InteropServices;

namespace CozyDump.Api.Model
{
    public static partial class DumpApiModel
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct MINIDUMP_SYSTEM_INFO
        {
            public ushort ProcessorArchitecture;
            public ushort ProcessorLevel;
            public ushort ProcessorRevision;
            public byte NumberOfProcessors;
            public byte ProductType;
            public uint MajorVersion;
            public uint MinorVersion;
            public uint BuildNumber;
            public uint PlatformId;
            public uint CSDVersionRva;
            public ushort SuiteMask;
            public ushort Reserved2;
            public uint VendorId0;
            public uint VendorId1;
            public uint VendorId2;
            public uint VersionInformation;
            public uint FeatureInformation;
            public uint AMDExtendedCpuFeatures;
        }
    }
}