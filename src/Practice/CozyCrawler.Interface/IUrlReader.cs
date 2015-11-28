using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCrawler.Interface
{
    public interface IUrlReader
    {
        string ReadHtml(string url);
        Stream ReadData(string url);
    }
}
