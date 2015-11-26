using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using CozySpider.Core.Reader;

namespace CozySpider.Image12306
{
    public class ImageReader : IUrlReader
    {
        public ImageReader()
        {
            ServicePointManager.ServerCertificateValidationCallback += (s, c, ch, ssl) => true;
        }

        public string Read(string url)
        {
            using (var rspStream = ReadData(url))
            {
                using (StreamReader reader = new StreamReader(rspStream, Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public Stream ReadData(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url.Trim());
            request.AllowAutoRedirect = true;
            request.Method = "GET";
            request.KeepAlive = true;
            request.ContentType = "image/jpeg";

            WebResponse respone = request.GetResponse();
            return respone.GetResponseStream();
        }
    }
}
