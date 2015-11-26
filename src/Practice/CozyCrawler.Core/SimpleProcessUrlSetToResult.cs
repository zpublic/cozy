using CozyCrawler.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CozyCrawler.Core
{
    public class SimpleProcessUrlSetToResult : IProcessUrlSetToResult
    {
        IResultGetter getter_;

        public void To(IResultGetter to)
        {
            getter_ = to;
        }

        public void NewUrl(string url)
        {
            // 可以添加到一个列表，然后异步处理，这里直接同步干活
            getter_.NewUrl(url);
        }

        public void OnEvent(ControllableEvent ev)
        {
        }

        Thread thread = null;
        public void Start()
        {
            thread = new Thread(new ThreadStart(Method));
            thread.Start();
        }

        public void Stop()
        {
            thread.Abort();
        }

        private void Method()
        {
            // 可以异步处理取到的url结果
            //getter_.NewUrl(url);
        }
    }
}
