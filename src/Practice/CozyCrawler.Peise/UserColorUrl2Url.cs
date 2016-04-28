using CozyCrawler.Base;
using CozyCrawler.Interface;
using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCrawler.Peise
{
    public class UserColorUrl2Url : IUrl2Url
    {
        private IUrlIn _To { get; set; }

        private AsyncInvoker<string> InnerInvoker { get; set; }

        public UserColorUrl2Url(int maxInvoker = 1)
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

        private readonly string PeisePath = @"//li[@class='indexcolor']";

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

        private List<UserColor> ReadAllXPathNode(HtmlDocument doc, string xpath)
        {
            List<UserColor> result = new List<UserColor>();

            HtmlNodeCollection hrefs = doc.DocumentNode.SelectNodes(xpath);
            if (hrefs == null)
            {
                return null;
            }

            const string namePath = @"h3/a[@target]";
            const string colorPath = @"div[@class='meta']/h4";
            const string UrlStart = @"http://www.peise.net/";

            foreach (HtmlNode nodeA in hrefs)
            {
                var nameNode = nodeA.SelectNodes(namePath).FirstOrDefault();
                var colornode = nodeA.SelectNodes(colorPath).FirstOrDefault();
                if (nameNode != null && colornode != null)
                {
                    var res = new UserColor()
                    {
                        Name = nameNode.InnerText.Trim(),
                        Url = UrlStart + nameNode.Attributes["href"].Value.Trim(),
                    };
                    var Ref = colornode.InnerText;
                    if (Ref.StartsWith("HEX:"))
                    {
                        res.RGB = Ref.Substring(4, 6);
                    }

                    result.Add(res);
                };
            }

            return result;
        }
    }
}
