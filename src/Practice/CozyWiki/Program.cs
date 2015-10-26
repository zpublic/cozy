using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy.Hosting.Self;
using CozyWiki.Cache;

namespace CozyWiki
{
    class Program
    {
        static void Main(string[] args)
        {
            CacheManager.Instance.MaxSize       = 128;
            CacheManager.Instance.Timeout       = 10000;
            CacheManager.Instance.CleanEnable   = true;

            Setting.Instance.Init();
            using (var host = new NancyHost(new Uri("http://localhost:" + Setting.Instance.Port)))
            {
                host.Start();
                try
                {
                    Process.Start("http://localhost:" + Setting.Instance.Port + "/m/readme");
                }
                catch (Exception)
                {
                }
                Console.ReadLine();
            }
        }
    }
}
