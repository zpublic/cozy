using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;

namespace CozyCrawler.Base
{
    public class AsyncInvoker<T>
    {
        private ConcurrentQueue<T> MsgQueue { get; set; } = new ConcurrentQueue<T>();
        private CancellationTokenSource CancelSource { get; set; }
        private Semaphore MsgSem { get; set; } = new Semaphore(0, int.MaxValue);
        private Task[] InnerTasks { get; set; }
        private bool _IsRunning;

        /// <summary>
        /// Running线程数
        /// </summary>
        public readonly int InvokerCount;

        /// <summary>
        /// 获取消息后的回调
        /// </summary>
        private Action<T> _InvokerAction;
        public Action<T> InvokerAction
        {
            get { return _InvokerAction; }
            set
            {
                if(_IsRunning)
                {
                    throw new Exception("Result is running");
                }
                _InvokerAction = value;
            }
        }

        /// <summary>
        /// 是否在运行
        /// </summary>
        public bool IsRunning
        {
            get { return _IsRunning; }
            set { _IsRunning = false; }
        }

        /// <summary>
        /// 初始化并设置线程数
        /// </summary>
        /// <param name="maxInvoker">最大的线程数</param>
        public AsyncInvoker(int maxInvoker)
        {
            if (maxInvoker <= 0)
            {
                throw new ArgumentException("maxInvoker must bigger than 0");
            }

            InvokerCount = maxInvoker;
        }

        /// <summary>
        /// 添加消息
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
            if(IsRunning)
            {
                throw new Exception("invoker is already start");
            }

            IsRunning = true;
            CancelSource = new CancellationTokenSource();
            InnerTasks = new Task[InvokerCount];

            for (int i = 0; i < InvokerCount; ++i)
            {
                InnerTasks[i] = Task.Factory.StartNew(TaskProc, CancelSource.Token);
            }
        }

        /// <summary>
        /// 停止接受消息并清空队列
        /// </summary>
        public void Stop()
        {
            if (!IsRunning)
            {
                throw new Exception("invoker is not start");
            }

            IsRunning = false;
            CancelSource.Cancel();
            MsgSem.Release(InvokerCount);
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
                    InvokerAction?.Invoke(value);
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
