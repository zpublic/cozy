using CozyCrawler.Core;
using CozyCrawler.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCrawler.SampleMuitUrlGenerater
{
    class Program
    {
        static void Main(string[] args)
        {
            IGenerateUrlRunner p1 = new SingleThreadMultSourceGenerateUrlRunner();
            IUrl2ResultRunner p3 = new AsyncUrl2ResultRunner();

            IUrlGenerater gen1 = new TestUrlGenerater();
            IUrlGenerater gen2 = new TestUrlGenerater2();

            p1.From(gen1);
            p1.From(gen2);
            p1.To(p3);
            p3.To(new TestUrl2Result());

            p3.Start();
            p1.Start();
        }
    }
}
