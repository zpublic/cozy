using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozySpider.Core.Model
{
    class UrlInfo
    {
        public UrlInfo(string url, int depth)
        {
            Url = url;
            Depth = depth;
        }

        public string Url { get; set; }

        public int Depth { get; set; }
    }
}
