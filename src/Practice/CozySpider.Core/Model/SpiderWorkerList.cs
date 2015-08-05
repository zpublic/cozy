using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace CozySpider.Core.Model
{
    public class SpiderWorkerList
    {
        private List<SpiderWorker> Workers = new List<SpiderWorker>();

        private UrlAddressQueue AddressQueue { get; set; }

        public int AllWorkersCount { get; private set; }

        private SpiderSetting Setting { get; set; }

        public void Add(SpiderWorker worker)
        {
            Workers.Add(worker);
            ++AllWorkersCount;
        }

        public SpiderWorkerList(UrlAddressQueue addressQueue, SpiderSetting setting)
        {
            AddressQueue = addressQueue;
            Setting = setting;
        }

        public void Start()
        {
            foreach(var worker in Workers)
            {
                worker.BeginWaitWork(AddressQueue, Setting);
            }
        }

        public void Stop()
        {
            foreach(var worker in Workers)
            {
                worker.StopWaitWork();
            }
        }
    }
}
