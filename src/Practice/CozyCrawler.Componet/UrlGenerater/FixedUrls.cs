using CozyCrawler.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCrawler.Component.UrlGenerater
{
    public class FixedUrls : IUrlGenerater
    {
        IUrlIn to_;
        public List<string> Urls = new List<string>();

        public void Start()
        {
            if (to_ != null)
            {
                foreach (var url in Urls)
                {
                    to_.OnNewUrl(url);
                }
            }
        }

        public void Stop()
        {
        }

        public void To(IUrlIn to)
        {
            to_ = to;
        }
    }
}
