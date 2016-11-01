using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyDump.Core.Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var parser = new MiniDumpParser())
            {
                if (parser.Parse(@"c:\lbsymbol\2006.dmp"))
                {
                    if (parser.ExistModuleListStream())
                    {
                        Console.WriteLine(parser.ModuleNums);
                        for (var i = 0; i < 3; ++i)
                        {
                            var moduleName = parser.GetStringFromRva(parser.ModuleInfo(i).ModuleNameRva);
                            Console.WriteLine(moduleName);
                        }
                    }
                    if (parser.ExistThreadListStream())
                    {
                        Console.WriteLine(parser.ThreadNums);
                        for (var i = 0; i < 3; ++i)
                        {
                            Console.WriteLine(parser.ThreadInfo(i).ThreadId);
                        }
                    }
                    if (parser.ExistSystemInfoStream())
                    {
                        var si = parser.SystemInfo();
                        Console.WriteLine("ostarget = {0}.{1}.{2}", si.MajorVersion, si.MinorVersion, si.BuildNumber);
                    }
                    if (parser.ExistExceptionStream())
                    {
                        var errorCode = parser.ExceptionStream().ExceptionRecord.ExceptionCode;
                        string errorMessage = new Win32Exception((int)errorCode).Message;
                        Console.WriteLine(errorMessage);
                    }
                }
            }
        }
    }
}
