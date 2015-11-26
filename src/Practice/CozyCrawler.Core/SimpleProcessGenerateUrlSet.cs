using CozyCrawler.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CozyCrawler.Core
{
    public class SimpleProcessGenerateUrlSet : IProcessGenerateUrlSet
    {
        IGenerater gen_ = null;
        IUrlSetIn to_ = null;

        public void NewUrl(string url)
        {
            to_.NewUrl(url);
        }

        public void OnEvent(ControllableEvent ev)
        {
        }

        public void From(IGenerater i)
        {
            gen_ = i;
        }

        public void To(IUrlSetIn to)
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
            gen_.Stop();
            thread.Abort();
        }

        private void Method()
        {
            gen_.To(this);
            gen_.Start();
        }
    }
}
