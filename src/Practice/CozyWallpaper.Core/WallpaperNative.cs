using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using CozyWallpaper.Core.json;
using CozyWallpaper.Core.Model;

namespace CozyWallpaper.Core
{
    public partial class WallpaperNative
    {
        public static int SetWallpaper(string picture)
        {
            return SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, picture, SPIF_SENDCHANGE);
        }

        public static string ReadPageContent(string url)
        {
            var data = DownloadUrl(url);
            return Encoding.UTF8.GetString(data);
        }

        private const string BingWallpaperApi = @"http://www.bing.com/HPImageArchive.aspx?format=js&idx=0&n=1&mkt=en-US";

        public static List<WallpaperInfo> GetBingWallpaperUrl()
        {
            var content = ReadPageContent(BingWallpaperApi);
            var obj     = JsonConvert.DeserializeObject<BingWallpaperObject>(content);
            var s =
                from o
                in obj.images
                select
                new WallpaperInfo()
                {
                    Title   = o.copyright,
                    Url     = o.url,
                };
            return s.ToList();
        }
    }
}
