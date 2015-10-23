using Nancy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyWiki.Responses
{
    public class HtmlResponse : Response
    {
        public HtmlResponse(string path)
        {
            StatusCode  = HttpStatusCode.OK;
            ContentType = "text/plain; charset=utf-8";

            Contents    = stream =>
            {
                using (var writer = new StreamWriter(stream))
                {
                    FileInfo fi = new FileInfo(path);
                    var cache   = CacheManager.Instance.HtmlCache.GetCache(path);
                    if (cache != null && cache.Item2 != fi.LastWriteTime)
                    {
                        // Using Cache
                        writer.Write(cache.Item1);
                        return;
                    }

                    var now         = DateTime.Now;
                    string context  = null;
                    using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                    {
                        using (var reader = new StreamReader(fs, Encoding.UTF8))
                        {
                            context = reader.ReadToEnd();
                            writer.Write(context);
                        }
                    }
                    fi.LastWriteTime = now;
                    CacheManager.Instance.HtmlCache.Update(path, context, now);
                }
            };
        }
    }
}
