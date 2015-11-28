using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyCrawler.Interface;
using CozyCrawler.Interface.Async;
using CozyCrawler.Model;

namespace CozyCrawler.AngryPowman
{
    public class ZhihuAnswerUrl2Url : IAsyncUrl2Url
    {
        private IUrlIn _To { get; set; }

        private AsyncInvoker<string> InnerInvoker { get; set; }

        public ZhihuAnswerUrl2Url(int maxInvoker = 1)
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

        }
    }
}
