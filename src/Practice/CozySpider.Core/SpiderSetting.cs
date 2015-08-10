using CozySpider.Core.UrlFilter;
using CozySpider.Core.UrlMatch;
using CozySpider.Core.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozySpider.Core
{
    public class SpiderSetting
    {
        private int depth = 3;
        public int Depth
        {
            get
            {
                return depth;
            }
            set
            {
                depth = value;
            }
        }

        private int workerCount = 3;
        public int WorkerCount
        {
            get
            {
                return workerCount;
            }
            set
            {
                workerCount = value;
            }
        }

        public SpiderSeeds Seeds { get; set; }

        public IUrlMatch Match { get; set; }

        public IUrlFilter Filter { get; set; }

        public IUrlReader Reader { get; set; }
    }
}
