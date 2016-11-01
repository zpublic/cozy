using static CozyDump.Api.Model.DumpApiModel;

namespace CozyDump.Core
{
    public partial class MiniDumpParser
    {
        MINIDUMP_SYSTEM_INFO _systemInfo;
        bool _ExistSystemInfoStream = false;

        public bool ExistSystemInfoStream()
        {
            return (parseSuccessed && _ExistSystemInfoStream);
        }
        public MINIDUMP_SYSTEM_INFO SystemInfo()
        {
            return _systemInfo;
        }
    }
}
