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
        private SpiderSetting setting = null;

        private UrlAddressQueue urlQueue = new UrlAddressQueue();

        public void Init(SpiderSetting setting)
        {
            this.setting = setting;
        }

        public void Crawl()
        {
            SpiderProcess.Seed2Queue(urlQueue, setting);
        }

        public void Stop()
        {

        }
    }
}
