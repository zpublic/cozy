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

        private AutoResetEvent BeginEvent { get; set; }

        private Action WorkAction { get; set; }

        public bool ShouldStop { get; set; }

        public bool IsWaiting { get; set; }

        public SpiderThreadWorker()
        {
            BeginEvent  = new AutoResetEvent(false);
            InnerThread = new Thread(new ThreadStart(ThreadProc));
            InnerThread.Start();
        }

        public override void StopWork()
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
            BeginEvent.Set();
        }

        private void ThreadProc()
        {
            while(!ShouldStop)
            {
                BeginEvent.WaitOne();
                IsWaiting = false;
                WorkAction();
                IsWaiting = true;
            }
        }
    }
}
