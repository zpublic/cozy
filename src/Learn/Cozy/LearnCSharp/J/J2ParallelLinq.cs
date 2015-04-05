using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cozy.LearnCSharp.J
{
    class J2ParallelLinq
    {
        static IEnumerable<int> SampleData()
        {
            const int s = 10000000;
            var r = new Random();
            return Enumerable.Range(0, s).Select(XmlReaderSettings => r.Next(140)).ToList();
        }

        static void IntroParallel()
        {
            var data = SampleData();
            var watch = new Stopwatch();

            watch.Start();
            var q1 = (from x in data
                      where Math.Log(x) < 4
                      select x).Average();
            watch.Stop();
            Console.WriteLine("sync {0}, result: {1}", watch.ElapsedMilliseconds, q1);

            watch.Reset();
            watch.Start();
            var q2 = (from x in Partitioner.Create(data).AsParallel()
                      where Math.Log(x) < 4
                      select x).Average();
            watch.Stop();
            Console.WriteLine("async {0}, result: {1}", watch.ElapsedMilliseconds, q2);
        }

        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            IntroParallel();
        }
    }
}
