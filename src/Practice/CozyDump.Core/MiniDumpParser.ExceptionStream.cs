using static CozyDump.Api.Model.DumpApiModel;

namespace CozyDump.Core
{
    public partial class MiniDumpParser
    {
        MINIDUMP_EXCEPTION_STREAM _exception;
        bool _ExistExceptionStream = false;

        public bool ExistExceptionStream()
        {
            return (parseSuccessed && _ExistExceptionStream);
        }
        public MINIDUMP_EXCEPTION_STREAM ExceptionStream()
        {
            return _exception;
        }
    }
}
