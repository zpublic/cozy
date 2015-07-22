using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozySpider.Core
{
    public class SpiderSeeds
    {
        private HashSet<string> seedsAddress = new HashSet<string>();

        public void AddSeed(string url)
        {
            seedsAddress.Add(url);
        }

        public void AddSeeds(IEnumerable<string> urls)
        {
            foreach (var i in urls)
            {
                seedsAddress.Add(i);
            }
        }

        public void DelSeed(string url)
        {
            seedsAddress.Remove(url);
        }

        public IEnumerable<string> GetSeeds()
        {
            return seedsAddress.AsEnumerable();
        }
    }
}
