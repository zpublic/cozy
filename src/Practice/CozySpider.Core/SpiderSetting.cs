using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozySpider.Core
{
    class SpiderSetting
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
    }
}
