using System;
using Nancy.Hosting.Self;

namespace CozyOfficialAccounts.Server {

    class Program {

        static void Main(string[] args) {

            using (var host = new NancyHost(new Uri("http://localhost:1024"))) {
                host.Start();
                Console.ReadKey();
            }
        }
    }
}
