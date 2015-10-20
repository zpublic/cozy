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
            Contents = stream =>
            {
                using (var writer = new StreamWriter(stream))
                {
                    var p = Path.Combine(Setting.Instance.RootDir, "404.html");
                    if (File.Exists(p))
                    {
                        using (var fs = new FileStream(p, FileMode.Open, FileAccess.Read))
                        {
                            using (var reader = new StreamReader(fs))
                            {
                                writer.Write(reader.ReadToEnd());
                            }
                        }
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
