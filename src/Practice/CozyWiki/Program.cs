using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy.Hosting.Self;

namespace CozyWiki
{
    class Program
    {
        static void Main(string[] args)
        {
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
