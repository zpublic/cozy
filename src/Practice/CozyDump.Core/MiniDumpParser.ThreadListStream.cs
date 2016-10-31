using static CozyDump.Api.Model.DumpApiModel;

namespace CozyDump.Core
{
    public partial class MiniDumpParser
    {
        MINIDUMP_THREAD_LIST _threads;

        public bool ExistThreadListStream()
        {
            return (parseSuccessed && _threads.NumberOfThreads > 0);
        }
        public uint ThreadNums { get { return _threads.NumberOfThreads; } }
        public MINIDUMP_THREAD ThreadInfo(int index)
        {
            return _threads.Threads[index];
        }
    }
}
