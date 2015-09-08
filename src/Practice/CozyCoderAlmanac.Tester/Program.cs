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
            Console.WriteLine(@"今天是 {0}", DateTime.Now.ToString("yyyy年MM月dd日 dddd"));

            Console.WriteLine("宜");
            var g = ca.GetGoodActivity();
            foreach (var i in g)
            {
                Console.WriteLine(i.Name + "  " + i.Desc);
            }
            Console.WriteLine("不宜");
            var b = ca.GetBadActivity();
            foreach (var i in b)
            {
                Console.WriteLine(i.Name + "  " + i.Desc);
            }
            Console.WriteLine(ca.GetDire());
            Console.WriteLine(ca.GetDrink());
            Console.WriteLine("女神亲近指数：" + ca.GetStar());
            Console.ReadKey();
        }
    }
}
