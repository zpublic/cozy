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

        public void Add(SpiderWorker worker)
        {
            Workers.Add(worker);
            ++AllWorkersCount;
        }

        public SpiderWorkerList(UrlAddressQueue addressQueue)
        {
            AddressQueue = addressQueue;
        }

        public void Start()
        {
            foreach(var worker in Workers)
            {
                worker.RecvWork(AddressQueue);
            }
        }

        public void Stop()
        {
            foreach(var worker in Workers)
            {
                worker.StopWork();
            }
        }
    }
}
