using CozyRSS.Resources.Dialog;
using CozyRSS.ViewModel.Dialog;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MaterialDesignThemes.Wpf;

namespace CozyRSS.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            OpenAddFeedDialogCommand = new RelayCommand(() =>
            {
                AddFeedDialog dlg = new AddFeedDialog() { DataContext = new AddFeedDialogViewModel() };
                DialogHost.Show(dlg, "RootDialog", ClosingEventHandler);
            });
        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
        }

        public RSSListFrameViewModel RSSListFrameViewModel { get; } = new RSSListFrameViewModel();

        public RelayCommand OpenAddFeedDialogCommand { get; private set; }
    }
}