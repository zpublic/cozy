using CozyRSS.Actions;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace CozyRSS.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        MainWindowActions _actions;
        public MainViewModel()
        {
            RSSListFrameViewModel = new RSSListFrameViewModel();
            RSSContentFrameViewModel = new RSSContentFrameViewModel();
            _actions = new MainWindowActions();
            _actions.RSSListFrameViewModel = RSSListFrameViewModel;
            OpenAddFeedDialogCommand = new RelayCommand(_actions.OpenAddFeedDialogAction);
            MoveWindowCommand = new RelayCommand<object>(_actions.MoveWindowAction);
            DoubleClickCommand = new RelayCommand<object>(_actions.DoubleClickAction);
        }

        public RSSListFrameViewModel RSSListFrameViewModel { get; } 
        public RSSContentFrameViewModel RSSContentFrameViewModel { get; } 

        public RelayCommand OpenAddFeedDialogCommand { get; private set; }
        public RelayCommand<object> MoveWindowCommand { get; private set; }
        public RelayCommand<object> DoubleClickCommand { get; private set; }
    }
}