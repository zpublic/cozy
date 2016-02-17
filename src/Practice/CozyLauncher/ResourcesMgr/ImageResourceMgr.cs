using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Net.Cache;

namespace CozyLauncher.ResourcesMgr
{
    public class ImageResourceMgr
    {
        #region Property

        private static ImageResourceMgr _instance = new ImageResourceMgr();
        public static ImageResourceMgr Instance
        {
            get
            {
                return _instance;
            }
        }

        private List<ResourceLoader> _imgLoaders = new List<ResourceLoader>();
        public List<ResourceLoader> imgLoaders
        {
            get
            {
                return _imgLoaders;
            }
        }

        private static readonly List<string> imgExts = new List<string>
        {
            ".png",
            ".jpg",
            ".jpeg",
            ".gif",
            ".bmp",
            ".tiff",
            ".ico"
        };

        private static readonly Dictionary<string, ImageSource> imageCache = new Dictionary<string, ImageSource>();

        #endregion

        #region Image Loaders

        private ResourceLoader imgLoaderByResName = new ResourceLoader(
            name => name.StartsWith("[Res]:", StringComparison.OrdinalIgnoreCase), /*canLoad*/
            name => /*Load*/
            {
                name = name.Replace("[Res]:", "");
                ResourceKey resKey = null;
                switch (name)
                {
                    case "app":
                        resKey = IconRes.appDrawingImageKey;
                        break;
                    case "baidu":
                        resKey = IconRes.baiduDrawingImageKey;
                        break;
                    case "exit":
                        resKey = IconRes.exitDrawingImageKey;
                        break;
                    case "folder_open":
                        resKey = IconRes.folder_openDrawingImageKey;
                        break;
                    case "help":
                        resKey = IconRes.helpDrawingImageKey;
                        break;
                    case "setting":
                        resKey = IconRes.settingDrawingImageKey;
                        break;
                    default:
                        break;
                }

                if (resKey != null)
                {
                    return Application.Current.TryFindResource(resKey) as ImageSource;
                }

                return null;
            });

        private ResourceLoader imgLoaderByPath = new ResourceLoader(
            path => System.IO.File.Exists(path), /*canLoad*/
            path => /*Load*/
            {
                ImageSource img = null;

                try
                {
                    // Firstly, check whether the file is kind of image type
                    if (imgExts.Contains(System.IO.Path.GetExtension(path).ToLower()))
                    {
                        img = new BitmapImage(new Uri(path), new RequestCachePolicy(RequestCacheLevel.CacheIfAvailable));
                    }
                    else
                    {
                        // Then, try to load associated icon
                        var icon = System.Drawing.Icon.ExtractAssociatedIcon(path);
                        if (icon != null)
                        {
                            img = Imaging.CreateBitmapSourceFromHIcon(
                                icon.Handle,
                                new Int32Rect(0, 0, icon.Width, icon.Height),
                                BitmapSizeOptions.FromEmptyOptions());

                        }
                    }
                }
                catch
                {
                    throw;
                }

                return img;
            });

        private ResourceLoader imgLoaderByUrl = new ResourceLoader(
            url => Uri.IsWellFormedUriString(url, UriKind.Absolute), /*canLoad*/
            url =>  /*Load*/
            {
                if (imageCache.ContainsKey(url))
                {
                    return imageCache[url];
                }

                ImageSource img = null;

                try
                {
                    // Should support asynchronously download in the future.
                    img = new BitmapImage(new Uri(url), new RequestCachePolicy(RequestCacheLevel.CacheIfAvailable));
                    imageCache[url] = img;
                }
                catch (Exception)
                {
                    // Net work error
                    throw;
                }

                return img;
            });

        private ResourceLoader imgLoaderDefault = new ResourceLoader(
            x => true, /*canLoad*/
            x => /*Load*/
            {
                return Application.Current.TryFindResource(IconRes.defaultDrawingImageKey) as ImageSource; }
            );

        #endregion

        #region Functions

        public ImageResourceMgr()
        {
            imgLoaders.Add(imgLoaderByResName);
            imgLoaders.Add(imgLoaderByPath);
            imgLoaders.Add(imgLoaderByUrl);
            imgLoaders.Add(imgLoaderDefault);
        }

        public ImageSource Load(string imagePath)
        {
            ImageSource img = null;
            foreach (var loader in imgLoaders)
            {
                try
                {
                    if (loader.CanLoad(imagePath))
                    {
                        img = loader.Load(imagePath) as ImageSource;
                        if (img != null)
                        {
                            return img;
                        }
                    }
                }
                catch
                {
                    // Continue throwing exception before alpha.
                    throw;
                }
            }
            return null;
        }

        #endregion
    }
}
