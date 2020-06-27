using CozyCrawler.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CozyCrawler.Tarheelants
{
    class XJpgDownloader : IUrl2Result
    {
        void DoGetImage(string url, string path)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

            req.ServicePoint.Expect100Continue = false;
            req.Method = "GET";
            req.KeepAlive = true;

            req.ContentType = "image/jpeg";
            HttpWebResponse rsp = (HttpWebResponse)req.GetResponse();

            System.IO.Stream stream = null;

            try
            {
                // 以字符流的方式读取HTTP响应
                stream = rsp.GetResponseStream();
                Image.FromStream(stream).Save(path);
            }
            finally
            {
                // 释放资源
                if (stream != null) stream.Close();
                if (rsp != null) rsp.Close();
            }
        }

        string path_;
        public void SetSavePath(string path)
        {
            path_ = path;
        }

        public void OnNewUrl(string url)
        {
            int i = url.IndexOf("x|x");
            var u1 = url.Substring(0, i);
            var u2 = url.Substring(i + 3);
            string tempPath = path_ + u2 + ".jpg";
            DoGetImage(u1, tempPath);
        }
    }
}
