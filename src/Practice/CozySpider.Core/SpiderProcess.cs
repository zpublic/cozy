using CozySpider.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozySpider.Core
{
    class SpiderProcess
    {
        public static void Seed2Queue(SpiderSeeds seeds, UrlAddressQueue urlQueue)
        {
            if (seeds != null && urlQueue != null)
            {
                var c = seeds.GetSeeds();
                foreach (var i in c)
                {
                    
                }
            }
        }

        public static void UrlFilter()
        {

        }

        public static void UrlMatch()
        {

        }
    }
}
