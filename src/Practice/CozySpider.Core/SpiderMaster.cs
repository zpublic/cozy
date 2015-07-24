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

        private List<SpiderWorker> workers = new List<SpiderWorker>();

        public void Init(SpiderSetting setting)
        {
            this.setting = setting;
            for (int i = 0; i < setting.WorkerCount; ++i)
            {
                workers.Add(new SpiderWorker());
            }
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
