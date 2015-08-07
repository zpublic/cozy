using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozySpider.Core.Model
{
    public class UrlAddressPool
    {
        private HashSet<string> AddressHashSet = new HashSet<string>();

        public bool Add(string url)
        {
            //if(AddressHashSet.Contains(url))
            //{
            //    return true;
            //}
            //AddressHashSet.Add(url);
            return false;
        }

        public List<string> GetUrls()
        {
            return AddressHashSet.ToList();
        }
    }
}
