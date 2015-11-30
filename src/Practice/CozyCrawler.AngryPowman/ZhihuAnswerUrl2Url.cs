using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyCrawler.Interface;
using CozyCrawler.Interface.Async;
using CozyCrawler.Componet;
using HtmlAgilityPack;
using System.Collections;
using System.Collections.Concurrent;
using CozyCrawler.Base;
using CozyCrawler.Componet.UrlReader;

namespace CozyCrawler.AngryPowman
{
    public class ZhihuAnswerUrl2Url : IAsyncUrl2Url
    {
        private ConcurrentBag<string> Urls { get; set; }
            = new ConcurrentBag<string>();

        private IUrlIn _To { get; set; }

        private AsyncInvoker<string> InnerInvoker { get; set; }

        private Uri Url { get; set; }

        public IUrlReader InnerReader { get; set; } = new DefaultUrlReader();

        public ZhihuAnswerUrl2Url(string name, int maxInvoker = 1)
        {
            Url = new Uri(@"http://www.zhihu.com/people/" + name + @"/answers");

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

        private readonly string AnsersPath = @"//a[@href and boolean(contains(@href, 'page=') or boolean(contains(@href, 'answers')))]";
        private readonly string AnserPath = @"//div[@class='zm-profile-section-list profile-answer-wrap']//a[@href and contains(@href, 'answer') and contains(@href, 'question')]";

        private void InvokerProc(string url)
        {
            HtmlDocument doc = new HtmlDocument();
            try
            {
                doc.LoadHtml(InnerReader.ReadHtml(url));
            }
            catch (Exception)
            {
                // todo 
            }

            var answersSet = ReadAllXPathNode(doc, AnsersPath);
            var asnwerSet = ReadAllXPathNode(doc, AnserPath);

            if(answersSet != null)
            {
                foreach (var answers in answersSet)
                {
                    if (!Urls.Contains(answers))
                    {
                        Urls.Add(answers);
                        OnNewUrl(answers);
                    }
                }
            }

            if (asnwerSet != null)
            {
                foreach (var answer in asnwerSet)
                {
                    if (!Urls.Contains(answer))
                    {
                        Urls.Add(answer);
                        _To.OnNewUrl(answer);
                    }
                }
            }
        }

        private HashSet<string> ReadAllXPathNode(HtmlDocument doc, string xpath)
        {
            HashSet<string> result = new HashSet<string>();

            HtmlNodeCollection hrefs = doc.DocumentNode.SelectNodes(xpath);
            if (hrefs == null)
            {
                return null;
            }

            foreach (HtmlNode nodeA in hrefs)
            {
                var Ref = nodeA.Attributes["href"].Value.Trim();
                if (!Ref.StartsWith(Url.ToString()))
                {
                    Uri newUri = null;
                    if (Uri.TryCreate(Url, Ref, out newUri))
                    {
                        Ref = newUri.ToString();
                    }
                }
                result.Add(Ref);
            }
            return result;
        }
    }
}
