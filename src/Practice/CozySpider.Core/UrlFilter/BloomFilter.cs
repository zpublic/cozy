using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozySpider.Core.UrlFilter
{
    public class BloomFilter : IUrlFilter
    {
        protected Abot.Util.IBloomFilter<string> bloomFilter = new Abot.Util.BloomFilter<string>(2000001, 0.001F);

        public bool Filter(string url)
        {
            if (AddIfNew(url))
            {
                return false;
            }
            return true;
        }

        protected bool Contains(string url)
        {
            if (url == null)
                return false;

            return bloomFilter.Contains(url);
        }

        protected bool AddIfNew(string url)
        {
            if (url == null)
                return false;

            if (bloomFilter.Contains(url))
                return false;

            bloomFilter.Add(url);
            return true;
        }
    }
}
