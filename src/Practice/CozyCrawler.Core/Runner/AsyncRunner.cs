using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;

namespace CozyCrawler.Core.Runner
{
    public class AsyncRunner<T>
    {
        private ConcurrentQueue<T> MsgQueue { get; set; } = new ConcurrentQueue<T>();
        private CancellationTokenSource CancelSource { get; set; }
        private Semaphore MsgSem { get; set; } = new Semaphore(0, int.MaxValue);
        private Task[] InnerTasks { get; set; }
        private bool _IsRunning;

        /// <summary>
        /// Running线程数
        /// </summary>
        public readonly int RunnerCount;

        /// <summary>
        /// 获取消息后的回调
        /// </summary>
        private Action<T> _RunnerAction;
        public Action<T> RunnerAction
        {
            get { return _RunnerAction; }
            set
            {
                if(_IsRunning)
                {
                    throw new Exception("Result is running");
                }
                _RunnerAction = value;
            }
        }

        /// <summary>
        /// Runner是否在运行
        /// </summary>
        public bool IsRunning
        {
            get { return _IsRunning; }
            set { _IsRunning = false; }
        }

        /// <summary>
        /// 初始化并设置线程数
        /// </summary>
        /// <param name="maxRunner">最大的线程数</param>
        public AsyncRunner(int maxRunner)
        {
            if (maxRunner <= 0)
            {
                throw new ArgumentException("maxRunner must bigger than 0");
            }

            RunnerCount = maxRunner;
        }

        /// <summary>
        /// 添加消息到Runner
        /// </summary>
        /// <param name="value">消息</param>
        public void Add(T value)
        {
            MsgQueue.Enqueue(value);
            MsgSem.Release();
        }

        /// <summary>
        /// 开始接受消息
        /// </summary>
        public void Start()
        {
            IsRunning = true;
            CancelSource = new CancellationTokenSource();
            InnerTasks = new Task[RunnerCount];

            for (int i = 0; i < RunnerCount; ++i)
            {
                InnerTasks[i] = Task.Factory.StartNew(TaskProc, CancelSource.Token);
            }
        }

        /// <summary>
        /// 停止接受消息并清空队列
        /// </summary>
        public void Stop()
        {
            IsRunning = false;
            CancelSource.Cancel();
            MsgSem.Release(RunnerCount);
            Task.WaitAll(InnerTasks);
            Clear();
        }

        protected void TaskProc()
        {
            while (!CancelSource.IsCancellationRequested)
            {
                MsgSem.WaitOne();

                if (CancelSource.IsCancellationRequested)
                {
                    break;
                }

                T value = default(T);
                if (MsgQueue.TryDequeue(out value))
                {
                    RunnerAction?.Invoke(value);
                }
            }
        }

        /// <summary>
        /// 清空队列
        /// </summary>
        private void Clear()
        {
            T item;
            while (MsgQueue.TryDequeue(out item))
            {
                // do nothing
            }
        }
    }
}
