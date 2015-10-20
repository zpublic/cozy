using Nancy;

namespace CozyMarkdown.WebStie.Module {

    public class Home : NancyModule {

        public Home() {

            Get["/"] = x => {
                return View["Home/Index", new { Message = "hello,CozyMarkdown! " }];
            };

        }
    }
}
