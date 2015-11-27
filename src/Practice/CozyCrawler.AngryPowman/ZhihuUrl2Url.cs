using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyCrawler.Interface;
using CozyCrawler.Interface.Async;
using CozyCrawler.Model;
using System.IO;
using System.Net;
using HtmlAgilityPack;

namespace CozyCrawler.AngryPowman
{
    public class ZhihuUrl2Url : IAsyncUrl2Url
    {
        private ConcurrentBag<string> Urls { get; set; }
            = new ConcurrentBag<string>();

        private AsyncInvoker<KeyValuePair<string, int>> InnerInvoker { get; set; }

        private IUrlIn _To { get; set; }

        public int MaxTire { get; set; }

        public ZhihuUrl2Url(int maxInvoer = 1)
        {
            InnerInvoker = new AsyncInvoker<KeyValuePair<string, int>>(maxInvoer);
            InnerInvoker.InvokerAction = ParseUrl;
        }

        public void OnNewUrl(string url)
        {
            Urls.Add(url);
            OnNewUrl(url, 0);
        }

        public void OnNewUrl(string url, int tire)
        {
            InnerInvoker.Add(new KeyValuePair<string, int>(url, tire));
        }

        public void To(IUrlIn to)
        {
            if (InnerInvoker.IsRunning)
            {
                throw new Exception("Result is running");
            }

            _To = to;
        }

        public void Start()
        {
            InnerInvoker.Start();
        }

        public void Stop()
        {
            InnerInvoker.Stop();
        }

        private readonly Uri Zhihu = new Uri(@"http://www.zhihu.com/");
        private void ParseUrl(KeyValuePair<string, int> url)
        {
            HtmlDocument doc = new HtmlDocument();
            try
            {
                doc.LoadHtml(ReadData(url.Key));
            }
            catch(Exception)
            {

            }

            HtmlNodeCollection hrefs = doc.DocumentNode.SelectNodes(@"//a[@href and not(contains(@href, 'javascript'))]");
            if (hrefs == null)
            {
                return;
            }

            foreach (HtmlNode nodeA in hrefs)
            {
                var Ref = nodeA.Attributes["href"].Value.Trim();
                if (!Ref.StartsWith(Zhihu.ToString()))
                {
                    Uri newUri = null;
                    if(Uri.TryCreate(Zhihu, Ref, out newUri))
                    {
                        Ref = newUri.ToString();
                    }
                }

                if (!Urls.Contains(Ref))
                {
                    Urls.Add(Ref);
                    _To.OnNewUrl(Ref);

                    if (url.Value < MaxTire)
                    {
                        OnNewUrl(Ref, url.Value + 1);
                    }
                }
            }
        }

        private const string DefaultUA = @"Mozilla / 5.0(Windows NT 6.2; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/32.0.1667.0 Safari/537.36";
        private string ReadData(string url)
        {
            var req = (HttpWebRequest)WebRequest.Create(url);

            req.ServicePoint.Expect100Continue = false;
            req.Method      = "GET";
            req.KeepAlive   = true;
            req.UserAgent   = DefaultUA;
            req.ContentType = "text/html";

            using (var rsp = (HttpWebResponse)req.GetResponse())
            {
                using (var sr = new StreamReader(rsp.GetResponseStream()))
                {
                    return sr.ReadToEnd();
                }
            }
        }
    }
}
