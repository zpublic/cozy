using CozyCrawler.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCrawler.Peise
{
    public class UserColorUrlGenerater : IUrlGenerater
    {
        IUrlIn to_;

        public int Begin { get; set; }
        public int End { get; set; }

        public UserColorUrlGenerater(int begin, int end)
        {
            Begin = begin;
            End = end;
        }

        public void Start()
        {
            for (int i = Begin; i < End; ++i)
            {
                to_.OnNewUrl(string.Format("http://www.peise.net/color/{0}.html", i));
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
