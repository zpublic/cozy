using System.IO;
using System.Net;

namespace CozyThunder.HttpDownload
{
    public class HttpDownloadRange
    {
        public static byte[] Download(string url, long from, long to)
        {
            int count = (int)(to - from + 1);
            HttpWebRequest httprequest = (HttpWebRequest)WebRequest.Create(url);
            httprequest.AddRange(from, to);
            HttpWebResponse httpresponse = (HttpWebResponse)httprequest.GetResponse();
            if (httpresponse.ContentLength == count)
            {
                byte[] by = new byte[count];
                Stream httpFileStream = httpresponse.GetResponseStream();
                int getByteSize = httpFileStream.Read(by, 0, (int)by.Length);
                while (getByteSize != count)
                {
                    getByteSize += httpFileStream.Read(by, getByteSize, (int)by.Length - getByteSize);
                }
                return by;
            }
            return null;
        }
    }
}
