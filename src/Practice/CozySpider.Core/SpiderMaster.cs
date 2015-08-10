using CozySpider.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CozySpider.Core
{
    public partial class SpiderMaster
    {
        private SpiderSetting Setting { get; set; }

        private UrlAddressQueue urlQueue { get; set; }

        private UrlAddressPool urlPool { get; set; }

        private SpiderWorkerList Workers { get; set; }

        public SpiderMaster()
        {
            urlQueue    = new UrlAddressQueue();
            urlPool     = new UrlAddressPool();
        }

        public void Init(SpiderSetting setting)
        {
            Setting = setting;
            Workers = new SpiderWorkerList(urlQueue);
            Workers.CreateWorker(setting.WorkerCount);
            Workers.SetWorkAction(WorkerAction);
            Workers.Start();
        }

        public void Crawl()
        {
            SpiderProcess.Seed2Queue(urlQueue, Setting);

            while(true)
            {
                Workers.WorkersFreeEvent.WaitOne();
                if (!urlQueue.HasValue) break;
            }
        }

        public void Stop()
        {
            Workers.Stop();
        }
    }
}
