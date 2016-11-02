using static CozyDump.Api.Model.DumpApiModel;

namespace CozyDump.Core
{
    public partial class MiniDumpParser
    {
        MINIDUMP_MEMORY_LIST _memories;

        public bool ExistMemoryListStream()
        {
            return (parseSuccessed && _memories.NumberOfMemoryRanges > 0);
        }
        public uint MemoryRangeNum { get { return _memories.NumberOfMemoryRanges; } }
        public MINIDUMP_MEMORY_DESCRIPTOR MemoryRange(int index)
        {
            return _memories.MemoryRanges[index];
        }
    }
}
