using CozyPress.WebServer.Core;
using Nancy;
using Nancy.Hosting.Self;
using System;

namespace CozyPress.WebServer.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            CozyPressServer.Init();

            HostConfiguration hostConfigs = new HostConfiguration()
            {
                UrlReservations = new UrlReservations() { CreateAutomatically = true },
            };
            var host = new NancyHost(new Uri("http://localhost:12306"), new DefaultNancyBootstrapper(), hostConfigs);
            host.Start();

            Console.ReadKey();
        }
    }
}
