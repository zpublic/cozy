using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozySpider.Core.Reader
{
    public interface IUrlReader
    {
        string Read(string url);
    }
}
