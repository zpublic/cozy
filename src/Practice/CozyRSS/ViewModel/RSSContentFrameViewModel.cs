using CozyRSS.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CozyRSS.ViewModel
{
    public class RSSContentFrameViewModel : ViewModelBase
    {
        public RSSContentFrameViewModel()
        {
            UpdateContentCommand = new RelayCommand(() =>
            {
                _UpdateContent("https://isocpp.org/blog/rss");
            });
            OpenWebPageCommand = new RelayCommand(() =>
            {
                if (_RSSContentList_SelectedItem?.Item?.link != null)
                {
                    try
                    {
                        Process proc = new Process();
                        proc.StartInfo.FileName = _RSSContentList_SelectedItem?.Item.link;
                        proc.Start();
                    }
                    catch { }
                }
            });
            Messenger.Default.Register<RSSListFrame_ListItemViewModelMsg>(this, true, m =>
            {
                if (m.MsgType == "FlushFeedCommand")
                {
                    _UpdateContent(m.ListItem.Feed.url);
                }
            });
        }

        string _title;
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                Set("Title", ref _title, value);
            }
        }

        public RelayCommand UpdateContentCommand { get; }
        public RelayCommand OpenWebPageCommand { get; }

        ObservableCollection<RSSContentList_ListItemViewModel> _RSSContentList_ListItems
            = new ObservableCollection<RSSContentList_ListItemViewModel>();
        public ObservableCollection<RSSContentList_ListItemViewModel> RSSContentList_ListItems
        {
            get { return _RSSContentList_ListItems; }
        }

        RSSContentList_ListItemViewModel _RSSContentList_SelectedItem;
        public RSSContentList_ListItemViewModel RSSContentList_SelectedItem
        {
            get { return _RSSContentList_SelectedItem; }
            set
            {
                _RSSContentList_SelectedItem = value;
                RaisePropertyChanged("RSSListFrame_SelectedItem");
                RaisePropertyChanged("ViewTitle");
                RaisePropertyChanged("ViewTime");
                RaisePropertyChanged("ViewContent");
            }
        }

        public string ViewTitle { get { return _RSSContentList_SelectedItem?.Item?.title; } }
        public string ViewTime { get { return _RSSContentList_SelectedItem?.Item?.pubDate; } }
        public string ViewContent
        {
            get
            {
                /*
                return _RSSListFrame_SelectedItem?.Item?.description?
                    .Replace("&nbsp;", " ")
                    .Replace('<', '[')
                    .Replace('>', ']');*/
                return _RSSContentList_SelectedItem?.Item?.description?
                    .Replace("&nbsp;", " ")
                    .Replace("<b>", "")
                    .Replace("</br>", "");
            }
        }

        async void _UpdateContent(string url)
        {
            var feed = await Task.Run(() => RssService.GetRssFeed(url));
            if (feed?.items.Count > 0)
            {
                _RSSContentList_ListItems.Clear();
                foreach (var i in feed.items)
                {
                    _RSSContentList_ListItems.Add(new RSSContentList_ListItemViewModel(i));
                }
                RSSContentList_SelectedItem = _RSSContentList_ListItems[0];
                Title = feed.title;
            }
        }
    }
}