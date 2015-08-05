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

        private SpiderWorkerList Workers { get; set; }

        public SpiderMaster()
        {
            urlQueue = new UrlAddressQueue();
        }

        public void Init(SpiderSetting setting)
        {
            this.Setting = setting;

            Workers                     = new SpiderWorkerList(urlQueue, Setting);
            Workers.AddUrlEventAction   = new Action<object, Event.AddUrlEventArgs>(OnAddUrlEventHandler);
            Workers.DataReceivedAction  = new Action<object, Event.DataReceivedEventArgs>(OnDataReceivedEventHandler);

            Workers.CreateWorker(setting.WorkerCount);
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
