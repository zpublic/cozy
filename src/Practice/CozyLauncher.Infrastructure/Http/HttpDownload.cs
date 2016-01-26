using System.IO;
using System.Net;

namespace CozyLauncher.Infrastructure.Http
{
    public class HttpDownload
    {
        public static bool HttpDownloadFile(string url, string path)
        {
            using (var stream = HttpGetStream(url))
            {
                using (var fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
                {
                    byte[] bArr = new byte[4096];
                    int size    = stream.Read(bArr, 0, (int)bArr.Length);
                    while (size > 0)
                    {
                        fs.Write(bArr, 0, size);
                        size = stream.Read(bArr, 0, (int)bArr.Length);
                    }
                }
            }
            return File.Exists(path);
        }

        public static Stream HttpGetStream(string url)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            return response.GetResponseStream();
        }

        public static string HttpGetString(string url)
        {
            using (var stream = HttpGetStream(url))
            {
                using (var sr = new StreamReader(stream))
                {
                    return sr.ReadToEnd();
                }
            }
        }
    }
}
