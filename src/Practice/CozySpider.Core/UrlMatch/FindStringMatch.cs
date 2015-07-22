using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozySpider.Core.UrlMatch
{
    public class FindStringMatch : IUrlMatch
    {
        public string StringFind { get; set; }

        private bool noCase = false;
        public bool NoCase
        {
            get
            {
                return noCase;
            }
            set
            {
                noCase = value;
            }
        }

        public bool Match(string url)
        {
            if (StringFind == null || StringFind == "")
            {
                return true;
            }
            bool bMatch = false;
            if (NoCase)
            {
                bMatch = url.IndexOf(StringFind, StringComparison.OrdinalIgnoreCase) >= 0;
            }
            else
            {
                bMatch = url.Contains(StringFind);
            }
            return bMatch;
        }
    }
}
