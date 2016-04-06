using CozyWallpaper.Core.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CozyWallpaper.Core
{
    public abstract class WallpaperWebSite
    {
        protected readonly string mUrl;

        public WallpaperWebSite(string url)
        {
            this.mUrl = url;
        }

        public abstract List<WallpaperInfo> GetWallpaper();

        protected string DownloadHeml()
        {
            WebClient wc = new WebClient();
            Stream stream = wc.OpenRead(this.mUrl);
            StreamReader sr = new StreamReader(stream, Encoding.Default);
            string html = sr.ReadToEnd();
            return html;
        }
    }
}
