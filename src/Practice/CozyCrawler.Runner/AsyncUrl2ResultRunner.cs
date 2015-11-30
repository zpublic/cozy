using CozyCrawler.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyCrawler.Base;

namespace CozyCrawler.Runner
{
    public class AsyncUrl2ResultRunner : IUrl2ResultRunner
    {
        private AsyncInvoker<string> InnerInvoker { get; set; }

        private IUrl2Result ToResult { get; set; }
       
        public AsyncUrl2ResultRunner(int maxInvoker = 1)
        {
            InnerInvoker = new AsyncInvoker<string>(maxInvoker);
            InnerInvoker.InvokerAction = str => ToResult?.OnNewUrl(str);
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

        public void To(IUrl2Result to)
        {
            if(InnerInvoker.IsRunning)
            {
                throw new Exception("Result is running");
            }
            ToResult = to;
        }
    }
}
