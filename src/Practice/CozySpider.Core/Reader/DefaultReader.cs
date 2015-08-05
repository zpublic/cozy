using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace CozySpider.Core.Reader
{
    public class DefaultReader : IUrlReader
    {
        public string Read(string url)
        {
            WebRequest request  = WebRequest.Create(url.Trim());
            WebResponse respone = request.GetResponse();
            Stream rspStream    = respone.GetResponseStream();
            StreamReader reader = new StreamReader(rspStream, Encoding.Default);
            string result       = reader.ReadToEnd();
            return result;
        }
    }
}
