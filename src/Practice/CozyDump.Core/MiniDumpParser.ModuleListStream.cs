using static CozyDump.Api.Model.DumpApiModel;

namespace CozyDump.Core
{
    public partial class MiniDumpParser
    {
        MINIDUMP_MODULE_LIST _modules;

        public bool ExistModuleListStream()
        {
            return (parseSuccessed && _modules.NumberOfModules > 0);
        }
        public uint ModuleNum { get { return _modules.NumberOfModules; } }
        public MINIDUMP_MODULE ModuleInfo(int index)
        {
            return _modules.Modules[index];
        }
    }
}
