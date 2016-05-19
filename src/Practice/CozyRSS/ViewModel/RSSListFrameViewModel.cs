using CozyRSS.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;

namespace CozyRSS.ViewModel
{
    public class RSSListFrameViewModel : ViewModelBase
    {
        public RSSListFrameViewModel()
        {
            FeedManageService.FeedManage.Load();
            foreach (var i in FeedManageService.FeedManage.GetFeeds())
            {
                _RSSListFrame_ListItems.Add(new RSSListFrame_ListItemViewModel(i));
            }
            Messenger.Default.Register<RSSListFrame_ListItemViewModelMsg>(this, true, m =>
            {
                if (m.MsgType == "RemoveFeedCommand")
                {
                    _RSSListFrame_ListItems.Remove(m.ListItem);
                }
            });
        }

        public string Title { get; } = "订阅列表栏";

        ObservableCollection<RSSListFrame_ListItemViewModel> _RSSListFrame_ListItems
            = new ObservableCollection<RSSListFrame_ListItemViewModel>();
        public ObservableCollection<RSSListFrame_ListItemViewModel> RSSListFrame_ListItems
        {
            get { return _RSSListFrame_ListItems; }
        }

        public void AddFeed(string url)
        {
            var i = FeedManageService.FeedManage.AddFeed(url);
            if (i != null)
                _RSSListFrame_ListItems.Add(new RSSListFrame_ListItemViewModel(i));
        }
    }
}
