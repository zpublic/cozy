using CozyRSS.FeedManage;
using CozyRSS.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace CozyRSS.ViewModel
{
    public struct RSSListFrame_ListItemViewModelMsg
    {
        public RSSListFrame_ListItemViewModel ListItem;
        public string MsgType;
    }

    public class RSSListFrame_ListItemViewModel : ViewModelBase
    {
        public string Name { get { return _feed?.name; } }
        public string News { get { return "10"; } }

        public RSSListFrame_ListItemViewModel(FeedNode feed)
        {
            _feed = feed;
            RemoveFeedCommand = new RelayCommand(() =>
            {
                FeedManageService.FeedManage.RemoveFeed(_feed);
                Messenger.Default.Send<RSSListFrame_ListItemViewModelMsg, RSSListFrameViewModel>(
                    new RSSListFrame_ListItemViewModelMsg() { ListItem = this, MsgType = "RemoveFeedCommand" });
            });
            FlushFeedCommand = new RelayCommand(() =>
            {
                Messenger.Default.Send<RSSListFrame_ListItemViewModelMsg, RSSContentFrameViewModel>(
                    new RSSListFrame_ListItemViewModelMsg() { ListItem = this, MsgType = "FlushFeedCommand" });
                Messenger.Default.Send<RSSListFrame_ListItemViewModelMsg, MainViewModel>(
                    new RSSListFrame_ListItemViewModelMsg() { ListItem = this, MsgType = "FlushFeedCommand" });
            });
        }
        FeedNode _feed;
        public FeedNode Feed
        {
            get { return _feed; }
            set { _feed = value; }
        }

        public RelayCommand RemoveFeedCommand
        {
            get;
            private set;
        }

        public RelayCommand FlushFeedCommand
        {
            get;
            private set;
        }
    }
}
