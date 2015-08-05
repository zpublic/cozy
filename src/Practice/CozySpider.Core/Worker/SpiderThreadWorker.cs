using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozySpider.Core.Model;
using System.Threading;

namespace CozySpider.Core.Worker
{
    public class SpiderThreadWorker : SpiderWorker
    {
        private Thread InnerThread { get; set; }

        private Action WorkAction { get; set; }

        public bool ShouldStop { get; set; }

        public bool IsWaiting { get; set; }

        public SpiderThreadWorker()
        {
            InnerThread = new Thread(new ThreadStart(ThreadProc));
            InnerThread.Start();
        }

        public override void StopWaitWork()
        {
            ShouldStop = true;
            if(IsWaiting)
            {
                InnerThread.Abort();
            }
        }

        protected override void DoWork(Action action)
        {
            WorkAction = action;
        }

        private void ThreadProc()
        {
            while(!ShouldStop)
            {
                if (AddressQueue != null)
                {
                    AddressQueue.AutoResetEvent.WaitOne();
                    IsWaiting = false;
                    WorkAction();
                    IsWaiting = true;
                    Thread.Sleep(0);
                }
            }
        }
    }
}
