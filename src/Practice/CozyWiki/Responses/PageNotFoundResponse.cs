using Nancy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyWiki.Responses
{
    public class PageNotFoundResponse : Response
    {
        public PageNotFoundResponse()
        {
            StatusCode  = HttpStatusCode.NotFound;
            ContentType = "text/plain; charset=utf-8";

            Contents    = stream =>
            {
                using (var writer = new StreamWriter(stream))
                {
                    var p       = Path.Combine(Setting.Instance.RootDir, "404.html");
                    FileInfo fi = new FileInfo(p);
                    var cache   = CacheManager.Instance.HtmlCache.GetCache(p);

                    if (cache != null && cache.Item2 >= fi.LastWriteTime)
                    {
                        // Using Cache
                        writer.Write(cache.Item1);
                        return;
                    }

                    // Cache Need Update
                    if (File.Exists(p))
                    {
                        var now         = DateTime.Now;
                        string context  = null;

                        using (var fs = new FileStream(p, FileMode.Open, FileAccess.Read))
                        {
                            using (var reader = new StreamReader(fs))
                            {
                                context = reader.ReadToEnd();
                                writer.Write(context);
                            }
                        }
                        fi.LastWriteTime = now;
                        CacheManager.Instance.HtmlCache.Update(p, context, now);
                    }
                    else
                    {
                        writer.Write("404 fot found");
                    }
                }
            };
        }
    }
}
