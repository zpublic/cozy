using CozyRSS.Syndication.Model;
using GalaSoft.MvvmLight;

namespace CozyRSS.ViewModel
{
    public class RSSContentList_ListItemViewModel : ViewModelBase
    {
        SyndicationItem _item;
        public RSSContentList_ListItemViewModel(SyndicationItem i)
        {
            _item = i;
        }

        public SyndicationItem Item { get { return _item; } }

        public string Title { get { return _item?.title; } }
        public string Time { get { return _item?.pubDate; } }
        public string Icon { get; } = "StarOutline";
    }
}