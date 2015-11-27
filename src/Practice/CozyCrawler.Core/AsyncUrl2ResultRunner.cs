using CozyCrawler.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyCrawler.Core.Runner;

namespace CozyCrawler.Core
{
    public class AsyncUrl2ResultRunner : IUrl2ResultRunner
    {
        private AsyncRunner<string> InnerRunner { get; set; }

        private IUrl2Result ToResult { get; set; }
       
        public AsyncUrl2ResultRunner(int maxRunner = 1)
        {
            InnerRunner = new AsyncRunner<string>(maxRunner);
            InnerRunner.RunnerAction = str => ToResult?.OnNewUrl(str);
        }

        public void OnNewUrl(string url)
        {
            InnerRunner.Add(url);
        }

        public void Start()
        {
            InnerRunner.Start();
        }

        public void Stop()
        {
            InnerRunner.Stop();
        }

        public void To(IUrl2Result to)
        {
            if(InnerRunner.IsRunning)
            {
                throw new Exception("Result is running");
            }
            ToResult = to;
        }
    }
}
