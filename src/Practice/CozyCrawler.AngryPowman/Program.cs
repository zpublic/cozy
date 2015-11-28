using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyCrawler.Core;
using CozyCrawler.Interface;

namespace CozyCrawler.AngryPowman
{
    class Program
    {
        static void Main(string[] args)
        {
            IUrlGeneraterRunner p1 = new SingleThreadUrlGeneraterRunner();
            IUrl2UrlRunner p2 = new BlockedUrl2UrlRunner();
            IUrl2ResultRunner p3 = new AsyncUrl2ResultRunner();

            p1.From(new ZhihuUrlGenerater("kingwl"));
            p1.To(p2);
            p2.To(p3);
            var url = new GenericAsyncUrl2Url("http://www.zhihu.com/", 4)
            {
                MaxTire = 1,
            };

            url.Start();
            p2.SetProcessor(url);
            p3.To(new ZhihuUrl2Result());

            p3.Start();
            p2.Start();
            p1.Start();

            Console.ReadKey();
        }
    }
}
