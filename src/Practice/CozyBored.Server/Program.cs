using Nancy;
using Nancy.Hosting.Self;
using System;

namespace CozyBored.Server {

    class Program {

        static void Main(string[] args) {

            var baseUrl = "http://localhost:23333/";

            HostConfiguration hostconfig = new HostConfiguration() {
                UrlReservations = new UrlReservations { CreateAutomatically = true }
            };

            using (var host = new NancyHost(new Url(baseUrl), new DefaultNancyBootstrapper(), hostconfig)) {
                host.Start();
                Console.WriteLine("火力全开！");
                Console.ReadKey();

            }
        }
    }
}
