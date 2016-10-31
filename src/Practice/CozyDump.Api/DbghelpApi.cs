using System;
using System.Runtime.InteropServices;
using static CozyDump.Api.Model.DumpApiModel;

namespace CozyDump.Api
{
    public static class DbghelpApi
    {
        [DllImport("dbghelp.dll", SetLastError = true)]
        public static extern bool MiniDumpReadDumpStream(
                        IntPtr BaseOfDump,
                        MINIDUMP_STREAM_TYPE StreamNumber,
                        ref IntPtr DirPtr,
                        ref IntPtr StreamPointer,
                        ref uint StreamSize);
    }
}
