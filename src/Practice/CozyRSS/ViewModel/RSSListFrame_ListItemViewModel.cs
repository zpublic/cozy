using CozyRSS.FeedManage;
using CozyRSS.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace CozyRSS.ViewModel
{
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
                Messenger.Default.Send<RSSListFrame_ListItemViewModel, RSSListFrameViewModel>(this);
            });
        }
        FeedNode _feed;


        public RelayCommand RemoveFeedCommand
        {
            get;
            private set;
        }
    }
}
