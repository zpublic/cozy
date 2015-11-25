using CozySpider.Core.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozySpider.Core
{
    public class SpiderProcess
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
                    urlQueue.EnQueue(new UrlInfo(i, 0));
                }
            }
        }

        public static bool UrlFilter(string url, SpiderSetting setting)
        {
            if (url != null && setting.Filter != null)
            {
                return setting.Filter.Filter(url);
            }
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

        public static string UrlRead(string url, SpiderSetting setting)
        {
            if(url != null && setting.Reader != null)
            {
                return setting.Reader.Read(url);
            }
            return null;
        }

        public static Stream UrlReadData(string url, SpiderSetting setting)
        {
            if (url != null && setting.Reader != null)
            {
                return setting.Reader.ReadData(url);
            }
            return null;
        }
    }
}
