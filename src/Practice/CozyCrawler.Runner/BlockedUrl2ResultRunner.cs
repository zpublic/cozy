using CozyCrawler.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CozyCrawler.Runner
{
    public class BlockedUrl2ResultRunner : IUrl2ResultRunner
    {
        IUrl2Result trans_;

        public void To(IUrl2Result to)
        {
            trans_ = to;
        }

        public void OnNewUrl(string url)
        {
            trans_?.OnNewUrl(url);
        }

        public void Start()
        {
        }

        public void Stop()
        {
        }
    }
}
