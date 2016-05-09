using CozyRSS.Services;
using GalaSoft.MvvmLight;

namespace CozyRSS.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            var f = RssService.GetRssFeed("https://isocpp.org/blog/rss");
            var f2= RssService.GetRssFeed("http://www.peise.net/rss.php?rssid=32");
        }
    }
}