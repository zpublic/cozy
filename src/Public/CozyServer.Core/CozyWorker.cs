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

        private long clientId;
        public long ClientId
        {
            get { return Interlocked.Read(ref clientId); }
            set { Interlocked.Exchange(ref clientId, value); }
        }

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
            while(IsRunning)
            {
                var msg = MsgQueue.Dequeue();
                if (msg != null)
                {
                    ClientId = msg.SenderConnection.RemoteUniqueIdentifier;
                    if (MessageCallback != null)
                    {
                        MessageCallback(msg);
                    }

                    if (MsgQueue.Count == 0)
                    {
                        ClientId = 0;
                    }
                    Thread.Sleep(0);
                }
            }
        }

        public void AddMessage(NetIncomingMessage msg)
        {
            MsgQueue.Enqueue(msg);
        }
    }
}
