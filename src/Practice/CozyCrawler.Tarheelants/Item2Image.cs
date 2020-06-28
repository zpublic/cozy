using CozyCrawler.Base;
using CozyCrawler.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Newtonsoft.Json;

namespace CozyCrawler.Tarheelants
{
    class Item2Image : IUrl2Url
    {
        private IUrlIn _To { get; set; }

        private AsyncInvoker<string> InnerInvoker { get; set; }

        public Item2Image(int maxInvoker = 1)
        {
            InnerInvoker = new AsyncInvoker<string>(maxInvoker);
            InnerInvoker.InvokerAction = InvokerProc;
        }

        public void OnNewUrl(string url)
        {
            InnerInvoker.Add(url);
        }

        public void Start()
        {
            InnerInvoker.Start();
        }

        public void Stop()
        {
            InnerInvoker.Stop();
        }

        public void To(IUrlIn to)
        {
            _To = to;
        }

        private void InvokerProc(string url)
        {
            HtmlDocument doc = new HtmlDocument();
            try
            {
                doc.LoadHtml(Encoding.GetEncoding("GBK").GetString(HttpGet.Get(url).Content.ReadAsByteArrayAsync().Result));
            }
            catch (Exception)
            {
                // todo 
            }

            var urls = ReadAllXPathNode(doc, url);

            if (urls != null)
            {
                foreach (var u in urls)
                {
                    _To.OnNewUrl(u);
                }
            }
        }

        private List<string> ReadAllXPathNode(HtmlDocument doc, string ourl)
        {
            List<string> result = new List<string>();
            HtmlNodeCollection hrefs = doc.DocumentNode.SelectNodes("//*[@id=\"product-photo-container\"]/a");
            if (hrefs == null)
            {
                HtmlNodeCollection hrefs2 = doc.DocumentNode.SelectNodes("//*[@id=\"product-photo-container\"]/img");
                if (hrefs2 != null)
                {
                    foreach (HtmlNode nodeA in hrefs2)
                    {
                        var url = nodeA.GetAttributeValue("src", "");
                        if (url.Length > 0)
                        {
                            int i = ourl.LastIndexOf("/");
                            result.Add("https:" + url + "x|x" + ourl.Substring(i + 1));
                        }
                    }
                }
            }
            else
            {
                foreach (HtmlNode nodeA in hrefs)
                {
                    var url = nodeA.GetAttributeValue("href", "");
                    if (url.Length > 0)
                    {
                        int i = ourl.LastIndexOf("/");
                        result.Add("https:" + url + "x|x" + ourl.Substring(i + 1));
                    }
                }
            }
            lock (this)
            {
                Console.WriteLine("------Item2Image------");
                foreach (var s in result)
                {
                    Console.WriteLine(s);
                }
                Console.WriteLine("-------------------------");
            }
            return result;
        }
    }
}
