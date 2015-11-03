using System;
using System.Linq;
using Nancy;
using Nancy.ModelBinding;
using LiteDB;
using CozyBored.Server.Models;

namespace CozyBored.Server.Modules
{
    public class RankModule : NancyModule
    {
        private LiteCollection<RankModel> table = DbContent.GetInstance().GetTable<RankModel>();

        public RankModule()
        {
            Get["query-rank/{ver}"] = param =>
            {
                Console.WriteLine("query-rank");
                string ver = param.ver;
                var i = table.FindAll();
                var i2 = i.OrderByDescending(x => x.time);
                var i3 = i2.Take(10);
                var result = i3.ToList();
                return result;
            };

            Get["get-rank/{ver}/{time}"] = param =>
            {
                Console.WriteLine("get-rank");
                string ver = param.ver;
                double atime = param.time;
                var num = table.FindAll().OrderByDescending(x => x.time).Count(x => x.time > atime) + 1;
                var result = new { num = num };
                return result;
            };

            Post["save/{ver}"] = param =>
            {
                var model = this.Bind<RankModel>();
                model.id = Guid.NewGuid();
                Console.WriteLine("save:" + model.name + "-" + model.time);
                if (table.Insert(model) != null)
                {
                    return HttpStatusCode.OK;
                }
                return HttpStatusCode.InternalServerError;
            };

            Get["delete/time/{time}"] = param =>
            {
                double atime = param.time;
                table.Delete(x => x.time == atime);
                return HttpStatusCode.OK;
            };
            Get["delete/name/{name}"] = param =>
            {
                string aname = param.name;
                table.Delete(x => x.name == aname);
                return HttpStatusCode.OK;
            };
        }

    }
}
