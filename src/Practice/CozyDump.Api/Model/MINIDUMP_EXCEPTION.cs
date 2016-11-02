using System.Runtime.InteropServices;

namespace CozyDump.Api.Model
{
    public static partial class DumpApiModel
    {
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct MINIDUMP_EXCEPTION
        {
            public uint ExceptionCode;
            public uint ExceptionFlags;
            public ulong ExceptionRecord;
            public ulong ExceptionAddress;
            public uint NumberParameters;
            public uint __unusedAlignment;
            public ulong ExceptionInformation1;
            public ulong ExceptionInformation2;
            public ulong ExceptionInformation3;
            public ulong ExceptionInformation4;
            public ulong ExceptionInformation5;
            public ulong ExceptionInformation6;
            public ulong ExceptionInformation7;
            public ulong ExceptionInformation8;
            public ulong ExceptionInformation9;
            public ulong ExceptionInformation10;
            public ulong ExceptionInformation11;
            public ulong ExceptionInformation12;
            public ulong ExceptionInformation13;
            public ulong ExceptionInformation14;
            public ulong ExceptionInformation15;
        }
    }
}
