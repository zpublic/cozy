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

        private UrlAddressQueue AddressQueue { get; set; }

        public int AllWorkersCount { get; private set; }

        public int FreeWokersCount { get; private set; }

        private SpiderSetting Setting { get; set; }

        public Action<object, AddUrlEventArgs> AddUrlEventAction;

        public Action<object, DataReceivedEventArgs> DataReceivedAction;

        public void Add(SpiderWorker worker)
        {
            Workers.Add(worker);
            ++AllWorkersCount;
        }

        public void CreateWorker(int n)
        {
            for (int i = 0; i < n; ++i)
            {
                var worker                      = new SpiderThreadWorker();
                worker.AddUrlEventHandler       += new EventHandler<AddUrlEventArgs>(AddUrlEventAction);
                worker.DataReceivedEventHandler += new EventHandler<DataReceivedEventArgs>(DataReceivedAction);
                Workers.Add(worker);
            }
            
        }

        public SpiderWorkerList(UrlAddressQueue addressQueue, SpiderSetting setting)
        {
            AddressQueue    = addressQueue;
            Setting         = setting;
        }

        public void Start()
        {
            foreach(var worker in Workers)
            {
                worker.BeginWaitWork(AddressQueue, Setting);
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
