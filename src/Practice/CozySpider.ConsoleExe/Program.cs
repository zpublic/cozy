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
            seeds.AddSeed("http://www.javfee.com/cn/genre/w/currentPage/");

            IUrlMatch match = new FindStringMatch()
            {
                StringFind  = "javfee.com",
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
            master.Test();
            Thread.Sleep(3000);

            master.Stop();
        }
    }
}
