using CozyCrawler.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CozyCrawler.SampleMuitUrlGenerater
{
    public class TestUrlGenerater2 : IUrlGenerater
    {
        IUrlIn to_;

        Thread thread = null;
        private void Method()
        {
            while (true)
            {
                to_.OnNewUrl("https://coding.net/u/zapline/p/cozy/svn" + DateTime.Now.ToLongTimeString());
                Thread.Sleep(2000);
            }
        }

        public void Start()
        {
            thread = new Thread(new ThreadStart(Method));
            thread.Start();
        }

        public void Stop()
        {
            thread.Abort();
        }

        public void To(IUrlIn to)
        {
            to_ = to;
        }
    }
}
