using CozyCrawler.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCrawler.Sample1
{
    public class TestResultGetter : IResultGetter
    {
        public void NewUrl(string url)
        {
            Console.WriteLine(url);
        }
    }
}
