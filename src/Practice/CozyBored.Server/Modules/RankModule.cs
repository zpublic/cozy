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

            Get["query-rank"] = param => {
                var result = table.FindAll()
                    .OrderByDescending(x => x.time).Take(10).ToList();
                return result;
            };

            Post["save"] = param => {
                var model = this.Bind<RankModel>();
                model.id = Guid.NewGuid();
                if (table.Insert(model) != null)
                    return HttpStatusCode.OK;
                return HttpStatusCode.InternalServerError;
            };
        }

    }
}
