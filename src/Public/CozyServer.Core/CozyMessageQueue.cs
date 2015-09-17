using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Lidgren.Network;

namespace CozyServer.Core
{
    public class CozyMessageQueue
    {
        ThreadSafeQueue<NetIncomingMessage> MessageQueue { get; set; } = new ThreadSafeQueue<NetIncomingMessage>();

        Semaphore InnerSemaphore { get; set; }

        public CozyMessageQueue(int MaxClientNum = int.MaxValue)
        {
            InnerSemaphore = new Semaphore(0, MaxClientNum);
        }

        public void Clear()
        {
            MessageQueue.Clear();
            InnerSemaphore.Release(0);
        }

        public void Enqueue(NetIncomingMessage t)
        {
            MessageQueue.Enqueue(t);
            InnerSemaphore.Release();
        }

        public NetIncomingMessage Dequeue()
        {
            InnerSemaphore.WaitOne();
            return MessageQueue.Dequeue();
        }

        public void Dequeue(out NetIncomingMessage t)
        {
            InnerSemaphore.WaitOne();
            MessageQueue.Dequeue(out t);
        }

        public int Count
        {
            get
            {
                return MessageQueue.Count;
            }
        }
    }
}
