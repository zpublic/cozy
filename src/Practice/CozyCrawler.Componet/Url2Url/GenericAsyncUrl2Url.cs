using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyCrawler.Interface;
using CozyCrawler.Interface.Async;
using HtmlAgilityPack;
using CozyCrawler.Base;
using CozyCrawler.Componet.UrlReader;

namespace CozyCrawler.Componet.Url2Url
{
    public class GenericAsyncUrl2Url : IAsyncUrl2Url
    {
        private ConcurrentBag<string> Urls { get; set; }
            = new ConcurrentBag<string>();

        private AsyncInvoker<KeyValuePair<string, int>> InnerInvoker { get; set; }

        private IUrlIn _To { get; set; }

        private IUrlReader InnerReader { get; set; } = new DefaultUrlReader();

        public int MaxTire { get; set; }

        public Uri Url { get; private set; }

        public GenericAsyncUrl2Url(string url, int maxInvoer = 1)
        {
            Url             = new Uri(url);
            InnerInvoker    = new AsyncInvoker<KeyValuePair<string, int>>(maxInvoer);

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

        private void ParseUrl(KeyValuePair<string, int> url)
        {
            HtmlDocument doc = new HtmlDocument();
            try
            {
                doc.LoadHtml(InnerReader.ReadHtml(url.Key));
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
                if (!Ref.StartsWith(Url.ToString()))
                {
                    Uri newUri = null;
                    if(Uri.TryCreate(Url, Ref, out newUri))
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
    }
}
