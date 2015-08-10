using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozySpider.Core.Model;

namespace CozySpider.Core
{
    public abstract partial class SpiderWorker
    {
        protected UrlAddressQueue AddressQueue { get; set; }

        private SpiderSetting Setting { get; set; }

        private Action workAction;
        public Action WrokAction
        {
            get
            {
                return workAction;
            }

            set
            {
                if(value == null)
                {
                    throw new ArgumentNullException("action is null");
                }

                workAction = value;
            }
        }

        public Action BeginWorkAction { get; set; }

        public Action FinishWorkAction { get; set; }

        public SpiderWorker(UrlAddressQueue queue)
        {
            AddressQueue = queue;
        }

        public abstract void StartWork();

        public abstract void StopWork();
    }
}
