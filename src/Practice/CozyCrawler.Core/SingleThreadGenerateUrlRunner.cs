using CozyCrawler.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CozyCrawler.Core
{
    public class SingleThreadGenerateUrlRunner : IGenerateUrlRunner
    {
        IUrlGenerater gen_ = null;
        IUrlIn to_ = null;

        public void OnNewUrl(string url)
        {
            to_?.OnNewUrl(url);
        }

        public void From(IUrlGenerater i)
        {
            gen_ = i;
        }

        public void To(IUrlIn to)
        {
            to_ = to;
        }

        Thread thread = null;
        public void Start()
        {
            thread = new Thread(new ThreadStart(Method));
            thread.Start();
        }

        public void Stop()
        {
            gen_?.Stop();
            thread?.Join();
        }

        private void Method()
        {
            gen_?.To(this);
            gen_?.Start();
        }
    }
}
