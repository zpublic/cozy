using Nancy;
using CommonMark;
using Nancy.Hosting.Self;
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
                return "hello world!";
            };

            Get["/p/{path}"] = x =>
            {
                var p = Path.Combine(Setting.Instance.RootDir, x.path + ".md");
                if (File.Exists(p))
                {
                    return new PageResponse(p);
                }
                else
                {
                    return new NotFoundResponse();
                }
            };

            Get["/m/{path}"] = x =>
            {
                var p = Path.Combine(Setting.Instance.RootDir, x.path + ".md");
                if (File.Exists(p))
                {
                    return new MardDownResponse(p);
                }
                else
                {
                    return new NotFoundResponse();
                }
            };
        }
    }
}
