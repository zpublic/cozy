using CozyCrawler.Runner;
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
            IUrlGeneraterRunner p1   = new MultiUrlGeneraterRunner();
            IUrl2UrlRunner p2       = new BlockedUrl2UrlRunner();
            IUrl2ResultRunner p3    = new AsyncUrl2ResultRunner();

            p1.From(new TestUrlGenerater());
            p1.To(p2);
            p2.To(p3);
            p2.SetProcessor(new TestUrl2Url());
            p3.To(new TestUrl2Result());

            p3.Start();
            p2.Start();
            p1.Start();
        }
    }
}
