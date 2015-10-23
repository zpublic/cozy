using Nancy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyWiki.Cache;
using CommonMark;

namespace CozyWiki.Responses
{
    public class MarkdownResponse : Response
    {
        public MarkdownResponse(string path)
        {
            StatusCode  = HttpStatusCode.OK;
            ContentType = "text/html; charset=utf-8";

            Contents    = stream =>
            {
                using (var writer = new StreamWriter(stream))
                {
                    FileInfo fi = new FileInfo(path);
                    var cache   = CacheManager.Instance.MarkdownCache.GetCache(path);

                    if (cache != null && cache.IsEffective(fi))
                    {
                        // Using Cache
                        writer.Write(cache.Data);
                        return;
                    }

                    // Cache miss
                    string context      = null;
                    using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                    {
                        using (var reader = new StreamReader(fs, Encoding.UTF8))
                        {
                            context = CommonMarkConverter.Convert(reader.ReadToEnd());
                            writer.Write(context);
                        }
                    }

                    // cache update
                    if (cache == null) cache = new CacheBlock();
                    cache.Update(context, fi);
                    CacheManager.Instance.MarkdownCache.Update(path, cache);
                }
            };
        }
    }
}
