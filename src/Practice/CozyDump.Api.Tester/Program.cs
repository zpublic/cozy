using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static CozyDump.Api.DumpApi;

namespace CozyDump.Api.Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var dumpFileName = @"c:\lbsymbol\1335.dmp";
                using (var rdr = new MiniDumpReader(dumpFileName))
                {
                    foreach (MINIDUMP_STREAM_TYPE strmType in Enum.GetValues(typeof(MINIDUMP_STREAM_TYPE)))
                    {
                        var strmDir = rdr.ReadStreamType(strmType);
                        var locStream = strmDir.location;
                        if (locStream.Rva != 0)
                        {
                            var addrStream = rdr.MapStream(locStream);
                            switch (strmType)
                            {
                                case MINIDUMP_STREAM_TYPE.ModuleListStream:
                                    var moduleStream = rdr.MapStream(strmDir.location);
                                    var moduleList = Marshal.PtrToStructure<MINIDUMP_MODULE_LIST>(moduleStream);
                                    var ndescSize = (uint)(Marshal.SizeOf<MINIDUMP_MODULE>() - 4);
                                    var locRva = new MINIDUMP_LOCATION_DESCRIPTOR()
                                    {
                                        Rva = (uint)(locStream.Rva + Marshal.SizeOf<MINIDUMP_MODULE_LIST>()),
                                        DataSize = (uint)(ndescSize + 4)
                                    };
                                    for (int i = 0; i < moduleList.NumberOfModules; i++)
                                    {
                                        var ptr = rdr.MapStream(locRva);
                                        var moduleinfo = Marshal.PtrToStructure<MINIDUMP_MODULE>(ptr);
                                        var moduleName = rdr.GetNameFromRva(moduleinfo.ModuleNameRva);
                                        Console.WriteLine(string.Format("  {0}", moduleName));

                                        locRva.Rva += ndescSize;
                                    }
                                    break;
                                default:
                                    Console.WriteLine(
                                            string.Format(
                                            "got {0} {1} Sz={2} Addr={3:x8}",
                                            strmType,
                                            locStream.Rva,
                                            locStream.DataSize,
                                            addrStream.ToInt32()));
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine(
                                        string.Format(
                                            "zero {0}",
                                            strmType
                                            ));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
