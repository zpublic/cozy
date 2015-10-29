using Nancy;
using CozyMarkdown.Data.Models;
using System.Linq;


namespace CozyMarkdown.WebStie.Module {

    public class HomeModule : BaseModule {

        public HomeModule() {

            Get["/"] = x => {
                var model = db.GetAll<ArticlecModel>().ToList();
                return View["Home/Index", model];
            };

        }
    }
}
