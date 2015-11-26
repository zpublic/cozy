using CozyCrawler.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;

namespace CozyCrawler.Core
{
    public class AsyncUrl2ResultRunner : IUrl2ResultRunner
    {
        private ConcurrentQueue<string> UrlMsgQueue { get; set; } = new ConcurrentQueue<string>();

        private CancellationTokenSource CancelSource { get; set; }
        private Semaphore MsgSem { get; set; } = new Semaphore(0, int.MaxValue);

        private IUrl2Result ToResult { get; set; }
        private Task[] InnerTasks { get; set; }

        public int RunnerCount { get; private set; }

        private bool _IsRunning;
        public bool IsRunning
        {
            get { return _IsRunning; }
            set { _IsRunning = false; }
        }

        public AsyncUrl2ResultRunner(int maxRunner = 1)
        {
            if(maxRunner <= 0)
            {
                throw new ArgumentException("maxRunner must bigger than 0");
            }

            RunnerCount = maxRunner;
        }

        public void OnNewUrl(string url)
        {
            UrlMsgQueue.Enqueue(url);
            MsgSem.Release();
        }

        public void Start()
        {
            IsRunning       = true;
            CancelSource    = new CancellationTokenSource();
            InnerTasks      = new Task[RunnerCount];

            for(int i = 0; i < RunnerCount; ++i)
            {
                InnerTasks[i] = Task.Factory.StartNew(TaskProc, CancelSource.Token);
            }
        }

        public void Stop()
        {
            IsRunning = false;
            CancelSource.Cancel();
            MsgSem.Release(RunnerCount);
            Task.WaitAll(InnerTasks);
        }

        public void To(IUrl2Result to)
        {
            if(IsRunning)
            {
                throw new Exception("Result is running");
            }
            ToResult = to;
        }

        private void TaskProc()
        {
            while(!CancelSource.IsCancellationRequested)
            {
                MsgSem.WaitOne();

                if(CancelSource.IsCancellationRequested)
                {
                    break;
                }

                string str = null;
                if(UrlMsgQueue.TryDequeue(out str))
                {
                    ToResult?.OnNewUrl(str);
                }
            }
        }
    }
}
