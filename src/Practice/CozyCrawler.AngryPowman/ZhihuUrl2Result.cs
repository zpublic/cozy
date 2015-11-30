using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyCrawler.Interface;

namespace CozyCrawler.AngryPowman
{
    public class ZhihuUrl2Result : IUrl2Result
    {
        public void OnNewUrl(string url)
        {
            lock(this)
            {
                Console.WriteLine(url);
            }
        }
    }
}
