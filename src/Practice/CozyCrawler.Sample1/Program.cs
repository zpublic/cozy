using CozyCrawler.Core;
using CozyCrawler.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CozyCrawler.Sample1
{
    class Program
    {
        static void Main(string[] args)
        {
            TestGenerater gen = new TestGenerater();
            TestResultGetter getter = new TestResultGetter();

            SimpleProcessGenerateUrlSet p1 = new SimpleProcessGenerateUrlSet();
            SimpleProcessUrlSetToResult p2 = new SimpleProcessUrlSetToResult();

            p1.From(gen);
            p1.To(p2);
            p2.To(getter);

            p2.Start();
            p1.Start();
        }
    }
}
