using System;
using System.Runtime.InteropServices;
using static CozyDump.Api.Model.DumpApiModel;

namespace CozyDump.Core
{
    public partial class MiniDumpParser : IDisposable
    {
        MiniDumpReader rdr_;
        public void Dispose()
        {
            rdr_?.Dispose();
        }

        #region Parse
        void ParseThreadListStream()
        {
            var strmDir = rdr_.ReadStreamType(MINIDUMP_STREAM_TYPE.ThreadListStream);
            var locStream = strmDir.location;
            if (locStream.Rva != 0)
            {
                var addrStream = rdr_.MapStream(locStream);
                var moduleStream = rdr_.MapStream(strmDir.location);
                var NumberOfThreads = Marshal.ReadInt32(moduleStream);
                var ndescSize = (uint)(Marshal.SizeOf<MINIDUMP_THREAD>() - 4);
                var locRva = new MINIDUMP_LOCATION_DESCRIPTOR()
                {
                    Rva = (uint)(locStream.Rva + Marshal.SizeOf<uint>()),
                    DataSize = (uint)(ndescSize + 4)
                };
                _threads.NumberOfThreads = (uint)NumberOfThreads;
                _threads.Threads = new MINIDUMP_THREAD[NumberOfThreads];
                for (int i = 0; i < NumberOfThreads; i++)
                {
                    var ptr = rdr_.MapStream(locRva);
                    _threads.Threads[i] = Marshal.PtrToStructure<MINIDUMP_THREAD>(ptr);
                    locRva.Rva += ndescSize;
                }
            }
        }
        void ParseModuleListStream()
        {
            var strmDir = rdr_.ReadStreamType(MINIDUMP_STREAM_TYPE.ModuleListStream);
            var locStream = strmDir.location;
            if (locStream.Rva != 0)
            {
                var addrStream = rdr_.MapStream(locStream);
                var moduleStream = rdr_.MapStream(strmDir.location);
                var NumberOfModules = Marshal.ReadInt32(moduleStream);
                var ndescSize = (uint)(Marshal.SizeOf<MINIDUMP_MODULE>() - 4);
                var locRva = new MINIDUMP_LOCATION_DESCRIPTOR()
                {
                    Rva = (uint)(locStream.Rva + Marshal.SizeOf<uint>()),
                    DataSize = (uint)(ndescSize + 4)
                };
                _modules.NumberOfModules = (uint)NumberOfModules;
                _modules.Modules = new MINIDUMP_MODULE[NumberOfModules];
                for (int i = 0; i < NumberOfModules; i++)
                {
                    var ptr = rdr_.MapStream(locRva);
                    _modules.Modules[i] = Marshal.PtrToStructure<MINIDUMP_MODULE>(ptr);
                    locRva.Rva += ndescSize;
                }
            }
        }
        #endregion
        bool parseSuccessed = false;
        public bool ParseSuccessed { get { return parseSuccessed; } }
        public bool Parse(string filepath)
        {
            parseSuccessed = true;
            try
            {
                rdr_ = new MiniDumpReader(filepath);
                ParseThreadListStream();
                ParseModuleListStream();
            }
            catch(Exception)
            {
                parseSuccessed = false;
            }
            return parseSuccessed;
        }

        public string GetStringFromRva(uint rva)
        {
            return rdr_.GetStringFromRva(rva);
        }
    }
}
