using CozyCrawler.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CozyCrawler.Sample1
{
    public class TestGenerater : IGenerater
    {
        IUrlSetIn to_;

        public void OnEvent(ControllableEvent ev)
        {
        }

        Thread thread = null;
        private void Method()
        {
            while (true)
            {
                to_.NewUrl("https://coding.net/u/zapline/p/cozy/git" + DateTime.Now.ToLongTimeString());
                Thread.Sleep(1000);
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

        public void To(IUrlSetIn to)
        {
            to_ = to;
        }
    }
}
