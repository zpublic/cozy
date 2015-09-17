using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace CozyServer.Core
{
    public class ThreadSafeQueue<T>
        where T : class
    {
        private Queue<T> InnerQueue { get; set; } = new Queue<T>();
        private object Locker { get; set; } = new object();

        public void Clear()
        {
            lock(Locker)
            {
                InnerQueue.Clear();
            }
        }

        public void Enqueue(T t)
        {
            lock(Locker)
            {
                InnerQueue.Enqueue(t);
            }
        }

        public T Dequeue()
        {
            lock(Locker)
            {
                if(InnerQueue.Count == 0)
                {
                    return null;
                }
                return InnerQueue.Dequeue();
            }
        }

        public void Dequeue(out T t)
        {
            lock (Locker)
            {
                if (InnerQueue.Count == 0)
                {
                    t = null;
                }
                t = InnerQueue.Dequeue();
            }
        }

        public int Count
        {
            get
            {
                lock(Locker)
                {
                    return InnerQueue.Count;
                }
            }
        }
    }
}
