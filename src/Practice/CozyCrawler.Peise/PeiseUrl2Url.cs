using CozyCrawler.Base;
using CozyCrawler.Interface;
using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCrawler.Peise
{
    public class PeiseUrl2Url : IUrl2Url
    {
        private IUrlIn _To { get; set; }

        private AsyncInvoker<string> InnerInvoker { get; set; }

        public PeiseUrl2Url(int maxInvoker = 1)
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

        private readonly string PeisePath = @"//li[@class='listpalette']";

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

            var paletteSet = ReadAllXPathNode(doc, PeisePath);

            if (paletteSet != null)
            {
                foreach (var palette in paletteSet)
                {
                    _To.OnNewUrl(JsonConvert.SerializeObject(palette));
                }
            }
        }

        private List<Palette> ReadAllXPathNode(HtmlDocument doc, string xpath)
        {
            List<Palette> result = new List<Palette>();

            HtmlNodeCollection hrefs = doc.DocumentNode.SelectNodes(xpath);
            if (hrefs == null)
            {
                return null;
            }

            const string namePath   = @"h3/a[@target]";
            const string colorPath  = @"a[@class='palette']/span[@style]";
            const string UrlStart   = @"http://www.peise.net/";

            foreach (HtmlNode nodeA in hrefs)
            {
                var nameNode    = nodeA.SelectNodes(namePath).FirstOrDefault();
                var colornodes  = nodeA.SelectNodes(colorPath);
                if (nameNode != null && colornodes != null)
                {
                    var res = new Palette()
                    {
                        Name    = nameNode.InnerText.Trim(),
                        Url     = UrlStart + nameNode.Attributes["href"].Value.Trim(),
                    };

                    foreach (var colornode in colornodes)
                    {
                        var Ref = colornode.Attributes["style"].Value.Trim();
                        var color = Ref.Substring(Ref.Length - 24, 24);
                        if (color.StartsWith("background-color:#"))
                        {
                            res.RGB.Add(color);
                        }
                    }
                    result.Add(res);
                };
            }

            return result;
        }
    }
}
