using CozyRSS.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace CozyRSS.ViewModel
{
    public class RSSContentFrameViewModel : ViewModelBase
    {
        public RSSContentFrameViewModel()
        {
            UpdateContentCommand = new RelayCommand(() =>
            {
                // http://feed.cnblogs.com/blog/u/132703/rss
                var feed = RssService.GetRssFeed("http://www.peise.net/rss.php?rssid=32");
            });
        }

        public string Title { get; } = "老肉鸡的博客";

        public RelayCommand UpdateContentCommand { get; }
    }
}