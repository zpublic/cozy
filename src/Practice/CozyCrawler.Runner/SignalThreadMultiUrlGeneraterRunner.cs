using CozyCrawler.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyCrawler.Base;

namespace CozyCrawler.Runner
{
    public class MultiUrlGeneraterRunner : IUrlGeneraterRunner
    {
        List<IUrlGenerater> gens_ = new List<IUrlGenerater>();
        List<IUrlIn> tos_ = new List<IUrlIn>();

        private AsyncInvoker<string> InnerInvoker { get; set; }

        public MultiUrlGeneraterRunner(int threadCount = 1)
        {
            InnerInvoker = new AsyncInvoker<string>(threadCount);
            InnerInvoker.InvokerAction = Method;
        }

        public void OnNewUrl(string url)
        {
            InnerInvoker.Add(url);
        }

        public void From(IUrlGenerater i)
        {
            gens_.Add(i);
        }

        public void To(IUrlIn to)
        {
            tos_.Add(to);
        }

        public void Start()
        {
            InnerInvoker.Start();
            foreach (var i in gens_)
            {
                i?.To(this);
                i?.Start();
            }
        }

        public void Stop()
        {
            foreach (var i in gens_)
            {
                i?.Stop();
            }
            InnerInvoker.Stop();
        }

        private void Method(string url)
        {
            foreach (var t in tos_)
            {
                t?.OnNewUrl(url);
            }
        }
    }
}
