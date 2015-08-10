using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace CozyWallpaper.Core
{
    public partial class WallpaperNative
    {
        public static byte[] DownloadUrl(string url)
        {
            HttpWebRequest rq = (HttpWebRequest)WebRequest.Create(url.Trim());

            HttpWebResponse rsp = (HttpWebResponse)rq.GetResponse();

            using (MemoryStream ms = new MemoryStream())
            {
                using (Stream stream = rsp.GetResponseStream())
                {
                    byte[] buffer = new byte[4096];
                    int c = 0;
                    while ((c = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ms.Write(buffer, 0, c);
                    }
                }
                byte[] result = new byte[ms.Length];
                ms.Seek(0, SeekOrigin.Begin);
                ms.Read(result, 0, result.Length);
                return result;
            }
        }

        public static void SaveFile(string path, byte[] data)
        {
            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
            {
                fs.Write(data, 0, data.Length);
            }
        }
    }
}
