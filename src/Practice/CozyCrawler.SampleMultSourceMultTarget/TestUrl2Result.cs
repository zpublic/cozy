using CozyCrawler.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCrawler.SampleMultSourceMultTarget
{
    public class TestUrl2Result : IUrl2Result
    {
        public void OnNewUrl(string url)
        {
            Console.WriteLine(url);
        }
    }
}
