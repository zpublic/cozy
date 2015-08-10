using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozySpider.Core.UrlFilter
{
    public interface IUrlFilter
    {
        bool Filter(string url);
    }
}
