using CozyCrawler.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CozyCrawler.Runner
{
    public class SingleThreadMultSourceUrlGeneraterRunner : IUrlGeneraterRunner
    {
        List<IUrlGenerater> gens_ = new List<IUrlGenerater>();
        IUrlIn to_ = null;

        public void OnNewUrl(string url)
        {
            to_?.OnNewUrl(url);
        }

        public void From(IUrlGenerater i)
        {
            gens_.Add(i);
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
            foreach (var i in gens_)
            {
                i?.Stop();
            }
            thread?.Join();
        }

        private void Method()
        {
            foreach (var i in gens_)
            {
                i?.To(this);
                i?.Start();
            }
        }
    }
}
