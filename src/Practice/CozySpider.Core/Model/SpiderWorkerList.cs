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
    public partial class SpiderWorkerList
    {
        private List<SpiderWorker> Workers = new List<SpiderWorker>();

        public UrlAddressQueue AddressQueue { get; set; }

        private readonly AutoResetEvent workersFreeEvent = new AutoResetEvent(true);
        public AutoResetEvent WorkersFreeEvent
        {
            get
            {
                return workersFreeEvent;
            }
        }

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
                SpiderWorker worker     = new SpiderThreadWorker(AddressQueue);
                worker.BeginWorkAction  = new Action(OnBeginWork);
                worker.FinishWorkAction = new Action(OnFinishWork);
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
                worker.StartWork();
            }
            AllWorkersCount = freeWokersCount = Workers.Count;
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
