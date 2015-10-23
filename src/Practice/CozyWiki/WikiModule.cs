using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CozyWiki.Responses;

namespace CozyWiki
{
    public class WikiModule : NancyModule
    {
        public WikiModule()
        {
            Get["/"] = x =>
            {
                var p = Path.Combine(Setting.Instance.RootDir, "index.html");
                if(File.Exists(p))
                {
                    return new HtmlResponse(p);
                }
                else
                {
                    return new PageNotFoundResponse();
                }
            };

            Get["/Inner/404"] = x =>
            {
                var p = Path.Combine(Setting.Instance.RootDir, "404.html");
                if (File.Exists(p))
                {
                    return new HtmlResponse(p);
                }
                else
                {
                    return "404 Not Found";
                }
            };

            Get["/p/{path}"] = x =>
            {
                var p = Path.Combine(Setting.Instance.RootDir + "/p/", x.path + ".html");
                if (File.Exists(p))
                {
                    return new HtmlResponse(p);
                }
                else
                {
                    return new PageNotFoundResponse();
                }
            };

            Get["/m/{path}"] = x =>
            {
                var p = Path.Combine(Setting.Instance.RootDir + "/m/", x.path + ".md");
                if (File.Exists(p))
                {
                    return new MarkdownResponse(p);
                }
                else
                {
                    return new PageNotFoundResponse();
                }
            };

            Get["/test"] = x =>
            {
                var model = new
                {
                    Name = "zapline",
                    User = new
                    {
                        Age = 100
                    }
                };
                return View["test", model];
            };
        }
    }
}
