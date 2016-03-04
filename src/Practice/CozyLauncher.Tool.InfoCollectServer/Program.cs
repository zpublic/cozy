using Nancy;
using Nancy.Hosting.Self;
using System;

namespace CozyLauncher.Tool.InfoCollectServer
{
    class Program
    {
        public class HelloModule : NancyModule
        {
            public HelloModule()
            {
                Get["/"] = x =>
                {
                    return "hello world!";
                };
            }
        }

        static void Main(string[] args)
        {
            HostConfiguration hostConfigs = new HostConfiguration()
            {
                UrlReservations = new UrlReservations() { CreateAutomatically = true }
            };
            var host = new NancyHost(
                new Uri("http://localhost:12306"),
                new DefaultNancyBootstrapper(),
                hostConfigs);
            host.Start();
            Console.ReadLine();
        }
    }
}
