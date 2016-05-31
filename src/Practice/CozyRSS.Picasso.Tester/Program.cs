using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyRSS.Picasso.Tester
{
    class Program
    {
        static List<string> Urls = new List<string>()
        {
            "http://www.infoq.com/cn/feed/architecture-design/articles",
            "http://36kr.com/feed",
            "http://www.huxiu.com/rss/0.xml"
        };

        static void Main(string[] args)
        {
            PicassoEngine p = new PicassoEngine();
            p.Init("./Picasso", Urls);
            var f1 = p.Get(Urls[0]);
            f1 = p.Flush(Urls[0]);
            f1 = p.Get(Urls[0]);
            f1 = p.Flush(Urls[0]);
            f1 = p.Get(Urls[0]);
            var b = p.IsReaded(f1.items[0]);
            p.SetReaded(f1.items[0]);
            b = p.IsReaded(f1.items[0]);
            p.SetReaded(f1.items[0], false);
            b = p.IsReaded(f1.items[0]);
        }
    }
}
