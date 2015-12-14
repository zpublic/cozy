using Nancy;
using Nancy.Hosting.Self;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyPress.WebServer.Core
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

    public class CozyPressServer
    {
        public void Run(string uri)
        {
            CozyPressHolder.Instance.WhosYourDaddy();

            HostConfiguration hostConfigs = new HostConfiguration()
            {
                UrlReservations = new UrlReservations() { CreateAutomatically = true },
            };
            var host = new NancyHost(new Uri(uri), new DefaultNancyBootstrapper(), hostConfigs);
            host.Start();
        }
    }
}
