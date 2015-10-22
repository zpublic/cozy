using Nancy;
using CozyMarkdown.Data.Models;
using System.Linq;

namespace CozyMarkdown.WebStie.Module {

    public class Home : BaseModule {

        public Home() {

            Get["/"] = x => {
                var list = db.GetContent<Articlecs>();
                var models = list.FindAll().ToList();
                
                return View["Home/Index", models];
            };

        }
    }
}
