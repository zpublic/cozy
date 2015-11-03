using System;
using System.Linq;
using Nancy;
using Nancy.ModelBinding;
using LiteDB;
using CozyBored.Server.Models;

namespace CozyBored.Server.Modules {

    public class RankModule : NancyModule {

        private LiteCollection<RankModel> table;

        public RankModule() {

            table = DbContent.GetInstance().GetTable<RankModel>();

            Get["query-rank/{ver}"] = param => {
                Console.WriteLine("query-rank");
                string ver = param.ver;
                var result = table.Find(x => x.ver == ver)
                    .OrderByDescending(x => x.time).Take(10).ToList();
                return result;
            };

            Get["get-rank/{ver}/{time}"] = param => {
                Console.WriteLine("query-rank");
                string ver = param.ver;
                int time = param.time;
                var result = table.Find(x => x.ver == ver)
                    .OrderByDescending(x => x.time).Count(x => x.time <= time) + 1;
                return result;
            };

            Post["save"] = param => {
                Console.WriteLine("save");
                var model = this.Bind<RankModel>();
                model.id = Guid.NewGuid();
                if (table.Insert(model) != null)
                    return HttpStatusCode.OK;
                return HttpStatusCode.InternalServerError;
            };
        }

    }
}
