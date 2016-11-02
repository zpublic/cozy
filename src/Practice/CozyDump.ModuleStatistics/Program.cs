using CozyDump.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CozyDump.ModuleStatistics
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, int> moduleCount = new Dictionary<string, int>();
            var files = Directory.GetFiles(@"c:\lbsymbol\").Where(x => x.EndsWith(".dmp"));
            foreach (var f in files)
            {
                using (var parser = new MiniDumpParser())
                {
                    parser.Parse(f);
                    if (parser.ExistModuleListStream())
                    {
                        for (var i = 0; i < parser.ModuleNum; ++i)
                        {
                            var m = parser.GetStringFromRva(parser.ModuleInfo(i).ModuleNameRva);
                            var filename = Path.GetFileName(m).ToLower();
                            if (moduleCount.ContainsKey(filename))
                                moduleCount[filename]++;
                            else
                                moduleCount[filename] = 1;
                        }
                    }
                }
            }
            moduleCount
                .OrderByDescending(i => i.Value)
                .Take(30)
                .ToList()
                .ForEach(x => Console.WriteLine(x));
        }
    }
}
