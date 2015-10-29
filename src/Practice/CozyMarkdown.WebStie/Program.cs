using System;
using Nancy;
using Nancy.Hosting.Self;
using System.Linq;

namespace CozyMarkdown.WebStie {

    class Program {

        static void Main(string[] args) {

#if DEBUG
            var db = Data.DbContent.GetInstance();
            if (db.GetAll<Data.Models.ArticlecModel>().Count() < 1) {
                var ariclecsContent = System.IO.File.ReadAllText("Test.md");
                for (int i = 0; i < 5; i++) {
                    db.Insert(new Data.Models.ArticlecModel {
                        Id = Guid.NewGuid(),
                        Title = $"Cozy{i}",
                        SubTitle = $"Cozy-readme{i}",
                        Content = ariclecsContent,
                        CreateDate = DateTime.Now,
                        UpdateDate = DateTime.Now
                    });
                }
            }
#endif

            var baseUrl = "http://localhost:1024/";

            HostConfiguration hostconfig = new HostConfiguration() {
                UrlReservations = new UrlReservations { CreateAutomatically = true }
            };

            using (var host = new NancyHost(new Url(baseUrl), new DefaultNancyBootstrapper(), hostconfig)) {
                host.Start();
                //Process.Start(baseUrl);
                Console.WriteLine("CozyMarkdown is runing , Press enter to stop");
                Console.ReadKey();

            }

        }
    }
}
