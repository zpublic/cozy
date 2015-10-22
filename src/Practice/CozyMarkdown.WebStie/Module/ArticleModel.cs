using CozyMarkdown.Data.Models;
using CommonMark;
namespace CozyMarkdown.WebStie.Module {

    public class ArticleModel : BaseModule {

        public ArticleModel() {

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
        }
    }
}
