using Nancy;
using Nancy.Hosting.Self;
using System;

namespace CozyNote.WebSite
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

    class WebSiteHost
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
