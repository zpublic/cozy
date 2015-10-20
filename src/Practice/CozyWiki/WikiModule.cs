using Nancy;
using CommonMark;
using Nancy.Hosting.Self;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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

            Get["/p/{path}"] = (x) =>
            {
                var p = Path.Combine(Setting.Instance.RootDir, x.path + ".md");
                if (File.Exists(p))
                {
                    using (var fs = new FileStream(p, FileMode.Open, FileAccess.Read))
                    {
                        using (var reader = new StreamReader(fs, Encoding.UTF8))
                        {
                            var context = reader.ReadToEnd();
                            return CommonMarkConverter.Convert(context);
                        }
                    }
                }
                else
                {
                    return Get404Page();
                }
            };
            Get["/m/{path}"] = x =>
            {
                var p = Path.Combine(Setting.Instance.RootDir, x.path + ".md");
                if (File.Exists(p))
                {
                    using (var fs = new FileStream(p, FileMode.Open, FileAccess.Read))
                    {
                        using (var reader = new StreamReader(fs, Encoding.UTF8))
                        {
                            var context = reader.ReadToEnd();
                            return context;
                        }
                    }
                }
                else
                {
                    return Get404Page();
                }
            };
        }

        public string Get404Page()
        {
            var p = Path.Combine(Setting.Instance.RootDir, "404.html");
            if (File.Exists(p))
            {
                using (var fs = new FileStream(p, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = new StreamReader(fs))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
            else
            {
                return "404 fot found";
            }
        }
    }
}
