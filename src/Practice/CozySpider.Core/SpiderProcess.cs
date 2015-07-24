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
        public static void Seed2Queue(
            UrlAddressQueue urlQueue,
            SpiderSetting setting)
        {
            if (setting.Seeds != null && urlQueue != null)
            {
                var c = setting.Seeds.GetSeeds();
                foreach (var i in c)
                {
                    if (UrlMatch(i, setting))
                    {
                        urlQueue.EnQueue(new UrlInfo(i, setting.Depth));
                    }
                }
            }
        }

        public static bool UrlFilter(string url, SpiderSetting setting)
        {
            return false;
        }

        public static bool UrlMatch(string url, SpiderSetting setting)
        {
            if (url != null && setting.Match != null)
            {
                return setting.Match.Match(url);
            }
            return false;
        }
    }
}
