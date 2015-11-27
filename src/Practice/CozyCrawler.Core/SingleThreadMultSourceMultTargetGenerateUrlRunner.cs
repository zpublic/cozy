using CozyCrawler.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CozyCrawler.Core
{
    public class SingleThreadMultSourceMultTargetGenerateUrlRunner : IGenerateUrlRunner
    {
        List<IUrlGenerater> gens_ = new List<IUrlGenerater>();
        List<IUrlIn> tos_ = new List<IUrlIn>();

        public void OnNewUrl(string url)
        {
            foreach (var t in tos_)
            {
                t?.OnNewUrl(url);
            }
        }

        public void From(IUrlGenerater i)
        {
            gens_.Add(i);
        }

        public void To(IUrlIn to)
        {
            tos_.Add(to);
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
