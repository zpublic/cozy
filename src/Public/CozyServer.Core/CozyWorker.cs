using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;
using System.Threading;

namespace CozyServer.Core
{
    public class CozyWorker
    {
        private CozyMessageQueue MsgQueue { get; set; } = new CozyMessageQueue();

        private Thread InnerThread { get; set; }

        private Dictionary<long, int> ClientIdSet { get; set; } = new Dictionary<long, int>();
        private object Locker { get; set; } = new object();

        private bool IsRunning { get; set; } = true;

        public Action<NetIncomingMessage> MessageCallback { get; set; }

        public CozyWorker()
        {
            InnerThread                 = new Thread(new ThreadStart(ThreadProc));
            InnerThread.IsBackground    = true;
        }

        public int MessageCount
        {
            get
            {
                return MsgQueue.Count;
            }
        }

        public bool ContainsId(long id)
        {
            lock (Locker)
            {
                return ClientIdSet.ContainsKey(id);
            }
        }

        public void Start()
        {
            InnerThread.Start();
        }

        public void Stop()
        {
            InnerThread.Abort();
        }

        private void ThreadProc()
        {
            while (IsRunning)
            {
                var msg = MsgQueue.Dequeue();
                if (msg != null)
                {
                    var id = msg.SenderConnection.RemoteUniqueIdentifier;
                    if (MessageCallback != null)
                    {
                        MessageCallback(msg);
                    }

                    lock (Locker)
                    {
                        ClientIdSet[id]--;
                        if (ClientIdSet[id] <= 0)
                        {
                            ClientIdSet.Remove(id);
                        }
                    }
                }
                Thread.Sleep(0);
            }
        }

        public void AddMessage(NetIncomingMessage msg)
        {
            lock (Locker)
            {
                MsgQueue.Enqueue(msg);
                if (ClientIdSet.ContainsKey(msg.SenderConnection.RemoteUniqueIdentifier))
                {
                    ClientIdSet[msg.SenderConnection.RemoteUniqueIdentifier]++;
                }
                ClientIdSet[msg.SenderConnection.RemoteUniqueIdentifier] = 1;
            }
        }
    }
}
