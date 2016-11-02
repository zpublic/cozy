using System.Runtime.InteropServices;

namespace CozyDump.Api.Model
{
    public static partial class DumpApiModel
    {
        public enum MINIDUMP_STREAM_TYPE
        {
            UnusedStream = 0,
            ReservedStream0 = 1,
            ReservedStream1 = 2,
            ThreadListStream = 3,
            ModuleListStream = 4,
            MemoryListStream = 5,
            ExceptionStream = 6,
            SystemInfoStream = 7,
            ThreadExListStream = 8,
            Memory64ListStream = 9,
            CommentStreamA = 10,
            CommentStreamW = 11,
            HandleDataStream = 12,
            FunctionTableStream = 13,
            UnloadedModuleListStream = 14,
            MiscInfoStream = 15,
            MemoryInfoListStream = 16, //   like VirtualQuery
            ThreadInfoListStream = 17,
            HandleOperationListStream = 18,
            TokenStream = 19,
            LastReservedStream = 0xFFFF
        }

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct MINIDUMP_DIRECTORY
        {
            public MINIDUMP_STREAM_TYPE streamType;
            public MINIDUMP_LOCATION_DESCRIPTOR location;
            public override string ToString()
            {
                return string.Format(
                    "{0} {1}",
                    streamType,
                    location
                    );
            }
        }
    }
}
