using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozySpider.Core.UrlMatch
{
    public interface IUrlMatch
    {
        bool Match(string url);
    }
}
