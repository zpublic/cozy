using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CozyWallpaper.Gui.Storage;
using CozyWallpaper.Gui.Model;
using System.Windows.Media.Imaging;
using CozyWallpaper.Core;
using MMS.UI.Default;

namespace CozyWallpaper.Gui.ViewModels
{
    public partial class MainWindowViewModel
    {
        private WallpaperStorage Storage = new WallpaperStorage();

        private const string ImagePath      = @".\wallpaper\";
        private const string JsonFileName   = @".\setting.json";

        private void StorageImage(string url, BitmapImage img)
        {
            var filename    = Path.GetFileNameWithoutExtension(url);
            var extension   = Path.GetExtension(url);
            if (!Directory.Exists(ImagePath))
            {
                Directory.CreateDirectory(ImagePath);
            }
            using (FileStream fs = new FileStream(ImagePath + filename + extension, FileMode.Create, FileAccess.ReadWrite))
            {
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(img));
                encoder.Save(fs);
            }
            Storage.Add(url, UrlSet[url], filename, extension);

            var json = Storage.GetStorageJson();
            using (FileStream fs = new FileStream(JsonFileName, FileMode.Create, FileAccess.ReadWrite))
            {
                var data = Encoding.UTF8.GetBytes(json);
                fs.Write(data, 0, data.Length);
            }
        }

        private void LoadStorage()
        {
            if (!File.Exists(JsonFileName))
            {
                throw new FileNotFoundException("cannot find file " + JsonFileName);
            }

            using (FileStream fs = new FileStream(JsonFileName, FileMode.Open, FileAccess.Read))
            {
                var reader = new StreamReader(fs);
                var data = reader.ReadToEnd();
                Storage.ReadStorageJosn(data);
            }

            foreach (var obj in Storage.GetWallpapers())
            {
                var abspath = Path.GetFullPath(ImagePath + obj.FileName + obj.Extension);
                var img = new BitmapImage(new Uri(abspath));
                lock (objLocker)
                {
                    ImageDictionary[obj.Url] = img;
                    UrlSet[obj.Url] = obj.Titile;
                }
                //WallpaperList.Add(new WallpaperInfo() { Url = obj.Url, Title = obj.Titile });
            }
        }
    }
}
