using CozyCrawler.Interface;
using CozyCrawler.Runner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCrawler.SampleMultSourceMultTarget
{
    class Program
    {
        static void Main(string[] args)
        {
            IUrlGeneraterRunner p1 = new SingleThreadMultSourceMultTargetUrlGeneraterRunner();
            IUrl2ResultRunner p3 = new AsyncUrl2ResultRunner();

            IUrlGenerater gen1 = new TestUrlGenerater();
            IUrlGenerater gen2 = new TestUrlGenerater2();
            IUrl2Result to1 = new TestUrl2Result();
            IUrl2Result to2 = new TestUrl2Result2();

            p1.From(gen1);
            p1.From(gen2);
            p1.To(to1);
            p1.To(p3);
            p3.To(to2);

            p3.Start();
            p1.Start();
        }
    }
}
