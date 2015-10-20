using CommonMark;
using Nancy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyWiki.Responses
{
    public class MardDownResponse : Response
    {
        public MardDownResponse(string path)
        {
            ContentType = "text/plain; charset=utf-8";

            Contents = stream =>
            {
                using (var writer = new StreamWriter(stream))
                {
                    using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                    {
                        using (var reader = new StreamReader(fs, Encoding.UTF8))
                        {
                            writer.Write(reader.ReadToEnd());
                        }
                    }
                }
            };
        }
    }
}
