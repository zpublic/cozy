using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CozyWallpaper.Gui.Storage
{
    public class WallpaperStorageObject
    {
        public string Url { get; set; }

        public string Titile { get; set; }

        public string FileName { get; set; }

        public string Extension { get; set; }
    }

    public class WallpaperStorage
    {
        public List<WallpaperStorageObject> Wallpapers { get; private set; }

        public WallpaperStorage()
        {
            Wallpapers = new List<WallpaperStorageObject>();
        }

        public void Add(WallpaperStorageObject obj)
        {
            Wallpapers.Add(obj);
        }

        public void Add(string url, string title, string filename, string ext)
        {
            Add(new WallpaperStorageObject()
            {
                Url         = url,
                Titile      = title,
                FileName    = filename,
                Extension   = ext,
            });
        }

        public void Remove(string url)
        {
            Wallpapers.RemoveAll((x) => { return x.Url == url; });
        }

        public string GetStorageJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public void ReadStorageJosn(string json)
        {
            var data    = JsonConvert.DeserializeObject<WallpaperStorage>(json);
            Wallpapers  = data.Wallpapers;
        }

        public IEnumerable<WallpaperStorageObject> GetWallpapers()
        {
            return Wallpapers.AsEnumerable();
        }
    }
}
