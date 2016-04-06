using CozyWallpaper.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CozyWallpaper.Core
{
    public class ZhuokuWallpaperWebSite : WallpaperWebSite
    {
        public ZhuokuWallpaperWebSite()
            : base("http://desk.zhuoku.com/all/%C4%D0%BA%A2.html")
        {

        }

        public override List<Model.WallpaperInfo> GetWallpaper()
        {
            List<Model.WallpaperInfo> wallpapers = new List<WallpaperInfo>();
            string html = this.DownloadHeml();
            Regex imageRegex = new Regex(@"<img[^>]*");
            MatchCollection images = imageRegex.Matches(html);
            foreach(Match image in images)
            {
                try
                {
                    string[] str = image.Value.Split(new Char[] { ' ', '=' });
                    WallpaperInfo wall = new WallpaperInfo();
                    wall.Title = str[4].Replace("\"", "");
                    wall.Url = str[2].Replace("\"", "");
                    wallpapers.Add(wall);
                }
                catch(Exception e)
                {

                }
            }
            return wallpapers;
        }
    }
}
