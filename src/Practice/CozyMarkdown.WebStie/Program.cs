using System;
using Nancy;
using Nancy.Hosting.Self;
using System.Diagnostics;

namespace CozyMarkdown.WebStie {

    class Program {

        static void Main(string[] args) {

            var baseUrl = "http://localhost:1024/";

            HostConfiguration hostconfig = new HostConfiguration() {
                UrlReservations = new UrlReservations { CreateAutomatically = true }
            };

            using (var host = new NancyHost(new Url(baseUrl), new DefaultNancyBootstrapper(), hostconfig)) {
                host.Start();
                Process.Start(baseUrl);
                Console.WriteLine("CozyMarkdown is runing , Press enter to stop");
                Console.ReadKey();

            }

        }
    }
}
