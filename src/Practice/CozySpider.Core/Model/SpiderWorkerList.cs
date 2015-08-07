using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using CozySpider.Core.Event;
using CozySpider.Core.Worker;

namespace CozySpider.Core.Model
{
    public class SpiderWorkerList
    {
        private List<SpiderWorker> Workers = new List<SpiderWorker>();

        public UrlAddressQueue AddressQueue { get; set; }

        public int AllWorkersCount { get; private set; }

        public int FreeWokersCount { get; private set; }

        public SpiderWorkerList(UrlAddressQueue queue)
        {
            AddressQueue = queue;
        }

        public void Add(SpiderWorker worker)
        {
            Workers.Add(worker);
            ++AllWorkersCount;
        }

        public void CreateWorker(int n)
        {
            for (int i = 0; i < n; ++i)
            {
                SpiderWorker worker = new SpiderThreadWorker(AddressQueue);
                Workers.Add(worker);
            }
        }

        public void SetWorkAction(Action action)
        {
            foreach (var worker in Workers)
            {
                worker.WrokAction = action;
            }
        }

        public void Start()
        {
            foreach(var worker in Workers)
            {
                worker.BeginWork();
            }
            AllWorkersCount = FreeWokersCount = Workers.Count;
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
