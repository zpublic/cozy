using System.Runtime.InteropServices;

namespace CozyDump.Api.Model
{
    public static partial class DumpApiModel
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct MINIDUMP_EXCEPTION_STREAM
        {
            public uint ThreadId;
            public uint __alignment;
            public MINIDUMP_EXCEPTION ExceptionRecord;
            public MINIDUMP_LOCATION_DESCRIPTOR ThreadContext;
        }
    }
}
