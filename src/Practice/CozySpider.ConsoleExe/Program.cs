using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozySpider.Core;
using CozySpider.Core.UrlMatch;
using System.Threading;

namespace CozySpider.ConsoleExe
{
    class Program
    {
        static void Main(string[] args)
        {
            SpiderSeeds seeds = new SpiderSeeds();
            //seeds.AddSeed("http://www.javfee.com/cn/genre/w/currentPage/");
            seeds.AddSeed(@"http://www.cnblogs.com/Jiajun/archive/2012/06/17/2552458.html");
            IUrlMatch match = new FindStringMatch()
            {
                StringFind  = "html",
                NoCase      = true
            };

            SpiderSetting setting = new SpiderSetting();
            setting.Depth           = 2;
            setting.WorkerCount     = 2;
            setting.Seeds           = seeds;
            setting.Match           = match;

            SpiderMaster master = new SpiderMaster();
            master.Init(setting);
            master.Crawl();

            Thread.Sleep(10000);
            master.Stop();
        }
    }
}
