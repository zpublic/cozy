using CozyMarkdown.Data.Models;
using CommonMark;
using Nancy.ModelBinding;

namespace CozyMarkdown.WebStie.Module {

    public class ArticleModule : BaseModule {

        public ArticleModule() {

            Get["Article/{title}"] = param => {
                string title = param.title;
                var q = db.GetContent<ArticlecModel>();
                var article = q.FindOne(y => y.Title == title);
                var model = new {
                    a = article,
                    html = CommonMarkConverter.Convert(article.Content)
                };
                return View["Article/Index", model];
            };

            Get["Article/Insert"] = param => {
                return View["Article/Insert"];
            };

            Post["Article/Insert"] = param => {
                var model = this.Bind<ArticlecModel>();
                return model;
            };
        }
    }
}
