using CozyCrawler.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCrawler.Sample1
{
    public class TestUrl2Url : IUrl2Url
    {
        IUrlIn to_;

        public void OnNewUrl(string url)
        {
            to_?.OnNewUrl(url + "hehe");
        }

        public void To(IUrlIn to)
        {
            to_ = to;
        }
    }
}
