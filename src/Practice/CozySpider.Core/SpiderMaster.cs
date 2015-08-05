using CozySpider.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CozySpider.Core.Worker;

namespace CozySpider.Core
{
    public class SpiderMaster
    {
        private SpiderSetting setting = null;

        private UrlAddressQueue urlQueue { get; set; }

        private SpiderWorkerList Workers { get; set; }

        public SpiderMaster()
        {
            urlQueue = new UrlAddressQueue();
            Workers  = new SpiderWorkerList(urlQueue);
        }

        public void Init(SpiderSetting setting)
        {
            this.setting = setting;
            for (int i = 0; i < setting.WorkerCount; ++i)
            {
                 Workers.Add(new SpiderThreadWorker());
            }
            Workers.Start();
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
