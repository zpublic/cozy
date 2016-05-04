using CozyFarseer.TopList.Controls;
using CozyFarseer.TopList.Extension;
using CozyFarseer.TopList.Model;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;

namespace CozyFarseer.TopList.ViewModel
{
    public class FarseerNodeViewModel : ViewModelBase
    {
        public FarseerNodeViewModel(FarseerNode node)
        {
            _node = node;
        }

        FarseerNode _node;
        public FarseerNode Node
        {
            get
            {
                return _node;
            }
            set
            {
                _node = value;
            }
        }

        public string Nick
        {
            get { return Node.nick; }
            set { Set("Nick", ref value);
                RaisePropertyChanged("Nick");
            }
        }

        public string Accuracy
        {
            get { return $"胜率{Node.accuracy}%"; }
        }

        public string TextContent
        {
            get { return Node.content; }
        }

        public int Likes
        {
            get { return Node.likes; }
        }

        public string Time
        {
            get { return Node.time.ToTotalTime(); }
        }

        public int TType
        {
            get { return Node.ttype; }
        }

        public int TStatus
        {
            get { return Node.tstatus; }
        }

        private const string defaultAvator = "/CozyFarseer.TopList;component/Resources/default.jpg";
        private Dictionary<string, BitmapImage> ImageCache { get; set; } = new Dictionary<string, BitmapImage>();

        public BitmapImage AvatarImage
        {
            get
            {
                if(!ImageCache.ContainsKey(Node.avatar))
                {
                    var s = new BitmapImage();
                    s.BeginInit();
                    s.UriSource = new Uri("http://api.imxz.net" + Node.avatar);
                    s.EndInit();
                    s.CacheOption = BitmapCacheOption.OnLoad;

                    ImageCache[Node.avatar] = s;
                    return s;
                }
                return ImageCache[Node.avatar];
            }
        }
    }
}