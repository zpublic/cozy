using CozyCrawler.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCrawler.Runner
{
    public class BlockedUrl2UrlRunner : IUrl2UrlRunner
    {
        IUrlIn to_;
        IUrl2Url p_;

        public void SetProcessor(IUrl2Url p)
        {
            p_ = p;
            if (to_ != null)
            {
                p_.To(to_);
            }
        }

        public void OnNewUrl(string url)
        {
            p_?.OnNewUrl(url);
        }

        public void Start()
        {
        }

        public void Stop()
        {
        }

        public void To(IUrlIn to)
        {
            to_ = to;
            p_?.To(to_);
        }
    }
}
