using Nancy;
using CozyMarkDonw.Mvc.Models;

namespace CozyMarkDown.Mvc.Modlues {

    public class HomeModlue : NancyModule {

        public HomeModlue() {


            Get["/"] = x => {
                return View["/Home/Index", ArticleModel.TestData()];
            };
        }
    }
}
