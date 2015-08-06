using CozySpider.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CozySpider.Core.Reader;

namespace CozySpider.Core
{
    public partial class SpiderMaster
    {
        private SpiderSetting Setting { get; set; }

        private UrlAddressQueue urlQueue { get; set; }

        private UrlAddressPool urlPool { get; set; }

        private SpiderWorkerList Workers { get; set; }

        private IUrlReader Reader { get; set; }

        public SpiderMaster()
        {
            urlQueue    = new UrlAddressQueue();
            urlPool     = new UrlAddressPool();
            Reader      = new DefaultReader();
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
        }

        public void Stop()
        {
            Workers.Stop();
        }
    }
}
