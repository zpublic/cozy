using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozySpider.Core.Model;
using CozySpider.Core.Reader;
using System.Text.RegularExpressions;
using System.Threading;

namespace CozySpider.Core
{
    public abstract class SpiderWorker
    {
        protected UrlAddressQueue AddressQueue { get; set; }

        private SpiderSetting Setting { get; set; }

        private IUrlReader Reader { get; set; }

        public SpiderWorker()
        {
            Reader = new DefaultReader();
        }

        public void BeginWaitWork(UrlAddressQueue queue, SpiderSetting setting)
        {
            if (queue == null || setting == null)
            {
                throw new ArgumentNullException("UrlAddressQueue and SpiderSetting must not null");
            }

            AddressQueue = queue;
            Setting = setting;
            DoWork(new Action(() =>
            {
                if(this.AddressQueue.HasValue)
                {
                    var result = this.AddressQueue.DeQueue();
                    if (result.Depth < Setting.Depth)
                    {
                        var pageData = Reader.Read(result.Url);
                        // TODO GetWebPage
                        const string pattern = @"http://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";
                        Regex r = new Regex(pattern, RegexOptions.IgnoreCase);
                        MatchCollection m = r.Matches(pageData);

                        foreach(var url in m)
                        {
                            var U = url.ToString();
                            if (SpiderProcess.UrlMatch(U, setting))
                            {
                                AddressQueue.EnQueue(new UrlInfo(U, result.Depth + 1));
                            }
                        }
                    }
                }
            }));
        }

        protected abstract void DoWork(Action action);

        public abstract void StopWaitWork();
    }
}
