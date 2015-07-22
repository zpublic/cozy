using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozySpider.Core;

namespace CozySpider.ConsoleExe
{
    class Program
    {
        static void Main(string[] args)
        {
            SpiderSeeds seeds = new SpiderSeeds();
            seeds.AddSeed("http://www.javfee.com/cn/genre/w/currentPage/");

            SpiderMaster master = new SpiderMaster();
            master.Init(seeds);
            master.Crawl();
        }
    }
}
