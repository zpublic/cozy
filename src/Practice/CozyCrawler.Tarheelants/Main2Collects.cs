using CozyCrawler.Base;
using CozyCrawler.Interface;
using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCrawler.Tarheelants
{
    class Main2Collects : IUrl2Url
    {
        private IUrlIn _To { get; set; }

        private AsyncInvoker<string> InnerInvoker { get; set; }

        public Main2Collects(int maxInvoker = 1)
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
        private readonly string XPath = "//*[@id=\"collection-list\"]/li/div/a";

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

            var urls = ReadAllXPathNode(doc, XPath);

            if (urls != null)
            {
                foreach (var u in urls)
                {
                    _To.OnNewUrl(u);
                }
            }
        }

        private List<string> ReadAllXPathNode(HtmlDocument doc, string xpath)
        {
            List<string> result = new List<string>();

            HtmlNodeCollection hrefs = doc.DocumentNode.SelectNodes(xpath);
            if (hrefs == null)
            {
                return null;
            }

            foreach (HtmlNode nodeA in hrefs)
            {
                var url = nodeA.GetAttributeValue("href", "");
                if (url.Length > 0)
                    result.Add("https://tarheelants.com" + url);
            }

            lock (this)
            {
                Console.WriteLine("------Main2Collects------");
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
