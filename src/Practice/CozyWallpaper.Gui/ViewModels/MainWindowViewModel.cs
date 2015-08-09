using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using CozyWallpaper.Gui.Model;
using System.Windows.Media.Imaging;
using CozyWallpaper.Core;
using System.Windows.Input;
using CozyWallpaper.Gui.Command;
using System.IO;
using CozyWallpaper.Gui.Storage;
namespace CozyWallpaper.Gui.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private ObservableCollection<WallpaperInfo> wallpaperList = new ObservableCollection<WallpaperInfo>();
        public ObservableCollection<WallpaperInfo> WallpaperList
        {
            get
            {
                return wallpaperList;
            }
            set
            {
                Set(ref wallpaperList, value, "WallpaperList");
            }
        }

        private WallpaperInfo selectedWallpaperListItem;
        public WallpaperInfo SelectedWallpaperListItem
        {
            get
            {
                return selectedWallpaperListItem;
            }
            set
            {
                Set(ref selectedWallpaperListItem, value, "SelectedWallpaperListItem");
            }
        }

        private Dictionary<string, BitmapImage> ImageDictionary = new Dictionary<string, BitmapImage>();
        private object objLocker = new object();

        private BitmapImage selectedImage = new BitmapImage();
        public BitmapImage SelectedImage
        {
            get
            {
                return selectedImage;
            }
            set
            {
                Set(ref selectedImage, value, "SelectedImage");
            }
        }

        private string selectedItemUrl;
        public string SelectedItemUrl
        {
            get
            {
                return selectedItemUrl;
            }
            set
            {
                Set(ref selectedItemUrl, value, "SelectedItemUrl");
            }
        }

        private Dictionary<string, string> UrlSet = new Dictionary<string, string>();

        private ICommand updateCommand;
        public ICommand UpdateCommand
        {
            get
            {
                return updateCommand = updateCommand ?? new DelegateCommand((x)=> 
                {
                    var result = WallpaperNative.GetBingWallpaperUrl();
                    foreach (var obj in result)
                    {
                        if (!UrlSet.ContainsKey(obj.Url))
                        {
                            UrlSet[obj.Url] = obj.Title;
                            WallpaperList.Add(new WallpaperInfo() { Title = obj.Title, Url = obj.Url });
                        }
                        else
                        {
                            // TODO no update
                        }
                    }
                });
            }
        }

        public MainWindowViewModel()
        {
            PropertyChanged += new PropertyChangedEventHandler(OnPropertyChanged);
            LoadStorage();
        }

        public void OnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "SelectedWallpaperListItem")
            {
                OnSelectedWallpaperItemChanged();
            }
        }

        private void OnSelectedWallpaperItemChanged()
        {
            if (SelectedWallpaperListItem != null)
            {
                SelectedItemUrl = SelectedWallpaperListItem.Url;
                if (ImageDictionary.ContainsKey(SelectedWallpaperListItem.Url))
                {
                    lock (objLocker)
                    {
                        if (ImageDictionary.ContainsKey(SelectedWallpaperListItem.Url))
                        {
                            SelectedImage = ImageDictionary[SelectedWallpaperListItem.Url];
                        }
                    }
                }
                else
                {
                    LoadImage(SelectedWallpaperListItem.Url);
                    lock (objLocker)
                    {
                        SelectedImage = ImageDictionary[SelectedWallpaperListItem.Url];
                    }
                }
            }
        }

        private void LoadImage(string url)
        {
            var img = new BitmapImage(new Uri(url));
            img.DownloadCompleted += (sender, msg) => 
            {
                StorageImage(url, img);
            };
            lock (objLocker)
            {
                ImageDictionary[url] = img;
            }
        }

        private WallpaperStorage Storage = new WallpaperStorage();

        private const string ImagePath = @".\wallpaper\";
        private const string JsonFileName = @".\setting.json";

        private void StorageImage(string url, BitmapImage img)
        {
            var filename = Path.GetFileNameWithoutExtension(url);
            var extension = Path.GetExtension(url);
            if(!Directory.Exists(ImagePath))
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
            if(!File.Exists(JsonFileName))
            {
                throw new FileNotFoundException("cannot find file " + JsonFileName);
            }

            using (FileStream fs = new FileStream(JsonFileName, FileMode.Open, FileAccess.Read))
            {
                var reader = new StreamReader(fs);
                var data = reader.ReadToEnd();
                Storage.ReadStorageJosn(data);
            }

            foreach(var obj in Storage.GetWallpapers())
            {
                var abspath = Path.GetFullPath(ImagePath + obj.FileName + obj.Extension);
                var img = new BitmapImage(new Uri(abspath));
                lock (objLocker)
                {
                    ImageDictionary[obj.Url] = img;
                    UrlSet[obj.Url] = obj.Titile;
                }
                WallpaperList.Add(new WallpaperInfo() { Url = obj.Url, Title = obj.Titile });
            }
        }
    }
}
