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

        private HashSet<string> UrlSet = new HashSet<string>();

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
                        if (!UrlSet.Contains(obj.Url))
                        {
                            UrlSet.Add(obj.Url);
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
    }
}
