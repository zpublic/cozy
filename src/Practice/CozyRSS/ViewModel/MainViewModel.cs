using CozyRSS.Services;
using GalaSoft.MvvmLight;

namespace CozyRSS.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
        }

        public RSSListFrameViewModel RSSListFrameViewModel { get; } = new RSSListFrameViewModel();
    }
}