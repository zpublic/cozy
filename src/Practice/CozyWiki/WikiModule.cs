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
                    return new PageResponse(p);
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
                    return new PageResponse(p);
                }
                else
                {
                    return "404 Not Found";
                }
            };

            Get["/p/{path}/"] = x =>
            {
                var p = Path.Combine(Setting.Instance.RootDir, x.path + ".md");
                if (File.Exists(p))
                {
                    return new PageResponse(p);
                }
                else
                {
                    return new PageNotFoundResponse();
                }
            };

            Get["/m/{path}/"] = x =>
            {
                var p = Path.Combine(Setting.Instance.RootDir, x.path + ".md");
                if (File.Exists(p))
                {
                    return new MardDownResponse(p);
                }
                else
                {
                    return new PageNotFoundResponse();
                }
            };
        }
    }
}
