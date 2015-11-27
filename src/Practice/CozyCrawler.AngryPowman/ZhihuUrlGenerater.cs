using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyCrawler.Interface;

namespace CozyCrawler.AngryPowman
{
    public class ZhihuUrlGenerater : IUrlGenerater
    {
        private string Name { get; set; }
        private const string SeedUrl = @"http://www.zhihu.com/people/";

        IUrlIn _To { get; set; }

        public ZhihuUrlGenerater(string name)
        {
            Name = name;
        }

        public void Start()
        {
            _To.OnNewUrl(SeedUrl + Name);
        }

        public void Stop()
        {
            // nothing to do
        }

        public void To(IUrlIn to)
        {
            _To = to;
        }
    }
}
