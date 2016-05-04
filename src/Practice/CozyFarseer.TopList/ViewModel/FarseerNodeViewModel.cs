using CozyFarseer.TopList.Controls;
using CozyFarseer.TopList.Model;
using GalaSoft.MvvmLight;
using System;
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

        public string Time
        {
            get { return Node.time.ToString(); }
        }

        private readonly string[] TTypeDesc = { "未知", "具体看涨", "具体看跌", "纯看涨", "纯看跌" };
        public string TType
        {
            get
            {
                if (Node.ttype >= -1 && Node.ttype < TTypeDesc.Length - 1)
                {
                    return TTypeDesc[Node.ttype + 1];
                }
                return TTypeDesc[0];
            }
        }

        private readonly string[] TStatusDesc = { "未知", "进行中", "成功", "失败" };
        public string TStatus
        {
            get
            {
                if (Node.tstatus >= -1 && Node.tstatus < TStatusDesc.Length - 1)
                {
                    return TStatusDesc[Node.tstatus + 1];
                }
                return TStatusDesc[0];
            }
        }

        private const string defaultAvator = "/CozyFarseer.TopList;component/Resources/default.jpg";
        public BitmapImage AvatarImage
        {
            get
            {
                var s = new BitmapImage();
                s.BeginInit();
                s.UriSource = new Uri("http://api.imxz.net" + Node.avatar);
                s.EndInit();
                s.CacheOption = BitmapCacheOption.OnLoad;
                return s;
            }
        }
    }
}