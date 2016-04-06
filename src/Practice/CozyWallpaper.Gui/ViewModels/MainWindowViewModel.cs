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
using MMS.UI.Default;
using MMS.Installation;
namespace CozyWallpaper.Gui.ViewModels
{
    public partial class MainWindowViewModel : BaseViewModel
    {
        private static MainWindowViewModel mMainWindow;

        public static MainWindowViewModel GetInstance()
        {
            if (mMainWindow == null)
            {
                mMainWindow = new MainWindowViewModel();
            }
            return mMainWindow;
        }

        private MainWindowViewModel()
        {
            //PropertyChanged += new PropertyChangedEventHandler(OnPropertyChanged);
            //LoadStorage();
            this.NextButton = new NextButton();
            this.BackButton = new BackButton();
            this.UpdateButton = new UpdateButton();
            this.Menu = Menus.GetInstance().Menu;


            //暂时这样写
            Task.Run(() =>
            {
                var images = new ZhuokuWallpaperWebSite().GetWallpaper();
                List<ImageInfo> temp = new List<ImageInfo>();
                foreach (var image in images)
                {
                    try
                    {
                        Uri u = new Uri(image.Url);
                        ImageInfo item = new ImageInfo()
                        {
                            Title = image.Title,
                            Url = image.Url,
                            DownloadImage = new DownloadCommand(),
                            SetWallpaper = new SetWallpaperCommand()
                        };
                        temp.Add(item);
                    }
                    catch (Exception e)
                    {

                    }
                }
                this.WallpaperList = temp;
            });
        }

        private List<ImageInfo> mWallpaperList = new List<ImageInfo>();
        public List<ImageInfo> WallpaperList { get { return this.mWallpaperList; } set { Set(ref this.mWallpaperList, value, "WallpaperList"); } }

        private string mImage = String.Empty;
        public string Image { get { return this.mImage; } set { this.mImage = value; OnPropertyChanged("Image"); } }

        public NextButton NextButton { get; set; }

        public BackButton BackButton { get; set; }

        public UpdateButton UpdateButton { get; set; }

        public List<NavigationItem> Menu { get; set; }

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

        public Dictionary<string, string> UrlSet = new Dictionary<string, string>();

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
    }

    public class DownloadCommand:ICommand
    {

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            WallpaperNative.DownloadImage(parameter as string);
        }
    }

    public class SetWallpaperCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            WallpaperNative.SetWallpaperNet(parameter as string);
        }
    }

}
