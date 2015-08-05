using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozySpider.Core.Model;
using System.Threading;

namespace CozySpider.Core
{
    public abstract class SpiderWorker
    {
        protected UrlAddressQueue AddressQueue { get; set; }

        public void BeginWaitWork(UrlAddressQueue queue)
        {
            if (queue == null)
            {
                throw new ArgumentNullException("UrlAddressQueue Must Not Null");
            }

            AddressQueue = queue;
            DoWork(new Action(() =>
            {
                if(this.AddressQueue.HasValue)
                {
                    var result = this.AddressQueue.DeQueue();
                    System.Windows.Forms.MessageBox.Show("hello " + result.Url + " " + result.Depth);
                }
            }));

        }

        protected abstract void DoWork(Action action);

        public abstract void StopWaitWork();
    }
}
