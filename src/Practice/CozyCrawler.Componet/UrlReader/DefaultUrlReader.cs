using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyCrawler.Interface;
using System.Net;

namespace CozyCrawler.Componet.UrlReader
{
    public class DefaultUrlReader : IUrlReader
    {
        private const string DefaultUA = @"Mozilla / 5.0(Windows NT 6.2; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/32.0.1667.0 Safari/537.36";

        public Stream ReadData(string url)
        {
            var req = (HttpWebRequest)WebRequest.Create(url);

            req.ServicePoint.Expect100Continue = false;
            req.Method      = "GET";
            req.KeepAlive   = true;
            req.UserAgent   = DefaultUA;
            req.ContentType = "text/html";

            var rsp = (HttpWebResponse)req.GetResponse();
            return rsp.GetResponseStream();
        }

        public string ReadHtml(string url)
        {
            using (var sr = new StreamReader(ReadData(url)))
            {
                return sr.ReadToEnd();
            }
        }
    }
}
