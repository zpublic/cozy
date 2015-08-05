using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CozySpider.Core.Model
{
    public class UrlAddressQueue
    {
        protected readonly Queue<UrlInfo> InnerQueue = new Queue<UrlInfo>();
        protected readonly object SyncObject = new object();

        private readonly AutoResetEvent autoResetEvent = new AutoResetEvent(false);
        public AutoResetEvent AutoResetEvent
        {
            get
            {
                return this.autoResetEvent;
            }
        }

        public int Count
        {
            get
            {
                lock (this.SyncObject)
                {
                    return this.InnerQueue.Count;
                }
            }
        }

        public bool HasValue
        {
            get
            {
                return this.Count > 0;
            }
        }

        public UrlInfo DeQueue()
        {
            lock (this.SyncObject)
            {
                if (this.InnerQueue.Count > 0)
                {
                    return this.InnerQueue.Dequeue();
                }
                return null;
            }
        }

        public void EnQueue(UrlInfo target)
        {
            lock (this.SyncObject)
            {
                this.InnerQueue.Enqueue(target);
                this.AutoResetEvent.Set();
            }
        }
    }
}
