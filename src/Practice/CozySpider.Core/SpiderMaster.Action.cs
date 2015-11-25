using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

namespace CozySpider.Core
{
    public partial class SpiderMaster
    {
        private const string pattern = @"http://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";

        public void WorkerAction()
        {
            var result = urlQueue.DeQueue();
            if (result != null)
            {
                if (result.Depth < Setting.Depth)
                {
                    try
                    {
                        string pageData = null;
                        using (var dataStream = SpiderProcess.UrlReadData(result.Url, Setting))
                        {
                            const int buffSize = 40960;

                            var buff = new byte[buffSize];
                            int bytes = 0;
                            List<byte> recv = new List<byte>();
                            while ((bytes = dataStream.Read(buff, 0, buffSize)) != 0)
                            {
                                if(bytes != buffSize)
                                {
                                    var ToBuff = new byte[bytes];
                                    Array.Copy(buff, ToBuff, bytes);
                                    recv.AddRange(ToBuff);
                                }
                                else
                                {
                                    recv.AddRange(buff);
                                }
                            }

                            var data = recv.ToArray();
                            OnDataReceivedEventHandler(this, new Event.DataReceivedEventArgs(result.Url, data));
                            pageData = Encoding.UTF8.GetString(data);
                        }

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
