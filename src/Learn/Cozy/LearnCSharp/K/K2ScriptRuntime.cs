using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronPython.Hosting;

namespace Cozy.LearnCSharp.K
{
    class K2ScriptRuntime
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            var scrptRuntime = Python.CreateRuntime();
            dynamic script = scrptRuntime.UseFile(@"LearnCSharp\K\Details\Script.py");
            var list = script.GetPaths();
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
        }
    }
}
