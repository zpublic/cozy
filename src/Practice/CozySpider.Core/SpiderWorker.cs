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
    public abstract partial class SpiderWorker
    {
        protected UrlAddressQueue AddressQueue { get; set; }

        private SpiderSetting Setting { get; set; }

        private IUrlReader Reader { get; set; }

        private const string pattern = @"http://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";

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

            AddressQueue    = queue;
            Setting         = setting;
            DoWork(new Action(() =>
            {
                if(AddressQueue.HasValue)
                {
                    var result = this.AddressQueue.DeQueue();

                    if (result.Depth < Setting.Depth)
                    {
                        try
                        {
                            var pageData = Reader.Read(result.Url);
                            if(DataReceivedEventHandler != null)
                            {
                                DataReceivedEventHandler(this, new Event.DataReceivedEventArgs(result.Url));
                            }

                            Regex r             = new Regex(pattern, RegexOptions.IgnoreCase);
                            MatchCollection m   = r.Matches(pageData);

                            foreach (var url in m)
                            {
                                var U = url.ToString();
                                if (SpiderProcess.UrlMatch(U, setting))
                                {
                                    if (AddUrlEventHandler != null)
                                    {
                                        AddUrlEventHandler(this, new Event.AddUrlEventArgs(U, result.Depth));
                                    }
                                    
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            if(ErrorEventHandler != null)
                            {
                                ErrorEventHandler(this, new Event.ErrorEventArgs(result.Url, e.Message));
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
