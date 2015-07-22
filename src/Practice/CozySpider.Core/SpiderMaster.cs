using CozySpider.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozySpider.Core
{
    public class SpiderMaster
    {
        private SpiderSeeds seeds = null;
        private UrlAddressQueue urlQueue = new UrlAddressQueue();

        public void Init(SpiderSeeds seeds)
        {
            this.seeds = seeds;
        }

        public void Crawl()
        {
            SpiderProcess.Seed2Queue(seeds, urlQueue);
        }
    }
}
