using Nancy;
using Nancy.Hosting.Self;
using System;
using System.Diagnostics;

namespace CozyNote.ServerCore
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

    public class ServerHost
    {
        public static void Run(string uri)
        {
            HostConfiguration hostConfigs = new HostConfiguration()
            {
                UrlReservations = new UrlReservations() { CreateAutomatically = true }
            };
            var host = new NancyHost(new Uri(uri), new DefaultNancyBootstrapper(), hostConfigs);
            host.Start();
        }
    }
}
