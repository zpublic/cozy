using CommonMark;
using Nancy;
using Nancy.Hosting.Self;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyWiki
{
    public class WikiModule : NancyModule
    {
        public WikiModule()
        {
            Get["/"] = x =>
            {
                return "hello world!";
            };
            Get["/p/{name}"] = x =>
            {
                return string.Concat("Hello ", x.name);
            };
            Get["/m"] = x =>
            {
                string mkText = @"
aaaa
====
[hehe](www.baidu.com)";
                return CommonMarkConverter.Convert(mkText);
            };
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            using (var host = new NancyHost(new Uri("http://localhost:23333")))
            {
                host.Start();
                try
                {
                    Process.Start("http://localhost:23333/p/lulu");
                }
                catch (Exception)
                {
                }
                Console.ReadLine();
            }
        }
    }
}
