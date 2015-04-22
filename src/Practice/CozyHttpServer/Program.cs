using Nancy;
using Nancy.Hosting.Self;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;

namespace CozyHttpServer
{
    public class TestModule : NancyModule
    {
        public TestModule()
        {
            Get["/"] = x =>
            {
                return "hello world!";
            };
            Get["/test/{name}"] = x =>
            {
                return string.Concat("Hello ", x.name);
            };

            // 每日一撸
            Get["/lu"] = x =>
            {
                var rsp = new
                {
                    title = "abc",
                    url   = "http:://www.laorouji.com",
                    text  = "aaaaa",
                };
                return JsonConvert.SerializeObject(rsp);
            };
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            NegativeEnergy.LoadData();

            using (var host = new NancyHost(new Uri("http://localhost:48360")))
            {
                host.Start();
                try
                {
                    Process.Start("http://localhost:48360/fnl");
                }
                catch (Exception)
                {
                }
                Console.ReadLine();
            }
        }
    }
}
