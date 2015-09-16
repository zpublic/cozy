using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;

namespace CozyServer.Core
{
    public class CozyWorkerSet
    {
        private List<CozyWorker> Workers { get; set; } = new List<CozyWorker>();
        private readonly object Locker = new object();

        public Action<NetIncomingMessage> MsgCallback { get; set; }

        public CozyWorkerSet(Action<NetIncomingMessage> action, int WorkerCount = 0)
        {
            MsgCallback = action;
            for(int i = 0; i < WorkerCount; ++i)
            {
                AddWorker(new CozyWorker());
            }
        }

        public void AddWorker(CozyWorker worker)
        {
            worker.MessageCallback = MsgCallback;
            lock(Locker)
            {
                Workers.Add(worker);
            }
        }

        public void WorkStart()
        {
            lock (Locker)
            {
                foreach(var obj in Workers)
                {
                    obj.Start();
                }
            }
        }

        public void WorkStop()
        {
            lock (Locker)
            {
                foreach (var obj in Workers)
                {
                    obj.Stop();
                }
            }
        }

        public void PushMessage(NetIncomingMessage msg)
        {
            lock (Locker)
            {
                var worker = FindWorker(msg.SenderConnection.RemoteUniqueIdentifier);
                worker.AddMessage(msg);
            }
        }

        private CozyWorker FindWorker(long id)
        {
            CozyWorker MinWorker = null;
            foreach (var worker in Workers)
            {
                if (worker.ClientId == id)
                {
                    return worker;
                }

                if (MinWorker == null)
                {
                    MinWorker = worker;
                }
                else
                {
                    MinWorker = MinWorker.MessageCount < worker.MessageCount ? MinWorker : worker;
                }
            }

            if (MinWorker != null)
            {
                return MinWorker;
            }
            return null;
        }
    }
}
