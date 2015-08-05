using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozySpider.Core.Model;

namespace CozySpider.Core
{
    public abstract class SpiderWorker
    {
        public void RecvWork(UrlAddressQueue queue)
        {
            if (queue != null)
            {
                queue.AutoResetEvent.WaitOne();
                var result = queue.DeQueue();
                DoWork(new Action(()=> 
                {

                    // TODO Parser the result
                }));
            }
        }

        protected abstract void DoWork(Action action);

        public abstract void StopWork();
    }
}
