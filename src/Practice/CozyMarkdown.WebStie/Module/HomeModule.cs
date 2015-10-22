using Nancy;
using CozyMarkdown.Data.Models;
using System.Linq;


namespace CozyMarkdown.WebStie.Module {

    public class HomeModule : BaseModule {

        public HomeModule() {

            Get["/"] = x => {
                var q = db.GetContent<ArticlecModel>();
                var model = q.FindAll().ToList();
                return View["Home/Index", model];
            };

        }
    }
}
