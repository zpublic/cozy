using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace CozySpider.Core
{
    public partial class SpiderMaster
    {
        private const string pattern = @"http://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";

        public void WorkerAction()
        {
            if(urlQueue.HasValue)
            {
                var result = urlQueue.DeQueue();
                if (result.Depth < Setting.Depth)
                {
                    try
                    {
                        var pageData = SpiderProcess.UrlRead(result.Url, Setting);
                        OnDataReceivedEventHandler(this, new Event.DataReceivedEventArgs(result.Url));

                        Regex r             = new Regex(pattern, RegexOptions.IgnoreCase);
                        MatchCollection m   = r.Matches(pageData);

                        foreach (var url in m)
                        {
                            var U = url.ToString();
                            if (SpiderProcess.UrlFilter(U, Setting))
                            {
                                continue;
                            }
                            if (SpiderProcess.UrlMatch(U, Setting))
                            {
                                OnAddUrlEventHandler(this, new Event.AddUrlEventArgs(U, result.Depth));
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        if (ErrorEventHandler != null)
                        {
                            OnErrorEventHandler(this, new Event.ErrorEventArgs(result.Url, e.Message));
                        }
                    }
                }
            }
        }
    }
}
