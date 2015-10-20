using Nancy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonMark;

namespace CozyWiki.Responses
{
    public class PageResponse : Response
    {
        public PageResponse(string path)
        {
            StatusCode  = HttpStatusCode.OK;
            ContentType = "text/html; charset=utf-8";

            Contents    = stream =>
            {
                using (var writer = new StreamWriter(stream))
                {
                    using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                    {
                        using (var reader = new StreamReader(fs, Encoding.UTF8))
                        {
                            CommonMarkConverter.Convert(reader, writer);
                        }
                    }
                }
            };
        }
    }
}
