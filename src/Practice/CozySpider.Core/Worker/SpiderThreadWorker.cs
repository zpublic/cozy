using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using CozySpider.Core.Model;

namespace CozySpider.Core.Worker
{
    public class SpiderThreadWorker : SpiderWorker
    {
        private Thread InnerThread { get; set; }

        public bool ShouldStop { get; set; }

        public bool IsWaiting { get; set; }

        public SpiderThreadWorker(UrlAddressQueue queue)
            :base(queue)
        {
            InnerThread = new Thread(new ThreadStart(ThreadProc));
        }

        public override void BeginWork()
        {
            if(InnerThread.ThreadState != ThreadState.Running)
            {
                IsWaiting = true;
                InnerThread.Start();
            }
        }

        public override void StopWaitWork()
        {
            ShouldStop = true;
            if(IsWaiting)
            {
                InnerThread.Abort();
            }
        }

        private void ThreadProc()
        {
            while (!ShouldStop && AddressQueue != null)
            {
                if (!AddressQueue.HasValue)
                {
                    AddressQueue.AutoResetEvent.WaitOne();
                }

                IsWaiting = false;
                WrokAction();
                IsWaiting = true;
                Thread.Sleep(0);
            }
        }
    }
}
