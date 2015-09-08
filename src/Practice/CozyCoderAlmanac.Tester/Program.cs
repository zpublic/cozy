using CozyCoderAlmanac.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCoderAlmanac.Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            CoderAlmanac ca = new CoderAlmanac();
            ca.Init();
            var g = ca.GetGoodActivity();
            foreach (var i in g)
            {
                Console.WriteLine(i.Name + "  " + i.Desc);
            }
            var b = ca.GetBadActivity();
            foreach (var i in b)
            {
                Console.WriteLine(i.Name + "  " + i.Desc);
            }
        }
    }
}
