using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using CozyWallpaper.Gui.Model;
using System.Windows.Media.Imaging;
using CozyWallpaper.Core;

namespace CozyWallpaper.Gui.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private List<WallpaperInfo> wallpaperList = new List<WallpaperInfo>();
        public List<WallpaperInfo> WallpaperList
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

        public MainWindowViewModel()
        {
            PropertyChanged += new PropertyChangedEventHandler(OnPropertyChanged);
            TestData();
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
            lock (objLocker)
            {
                ImageDictionary[url] = img;
            }
        }

        public void TestData()
        {
            WallpaperList.Add(new WallpaperInfo() { Title = "123", Url = @"https://www.baidu.com/img/baidu_jgylogo3.gif?v=38768253.gif" });
            WallpaperList.Add(new WallpaperInfo() { Title = "456", Url = @"https://ss2.baidu.com/6ONYsjip0QIZ8tyhnq/it/u=2435223104,2211683229&fm=58" });
        }
    }
}
