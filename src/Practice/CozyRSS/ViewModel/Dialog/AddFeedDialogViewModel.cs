using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;

namespace CozyRSS.ViewModel.Dialog
{
    public class AddFeedDialogViewModel : ViewModelBase
    {
        public AddFeedDialogViewModel(RSSListFrameViewModel RSSListFrameViewModel, Action closeAction)
        {
            _vm = RSSListFrameViewModel;
            _closeAction = closeAction;
            CloseDialogCommand = new RelayCommand<object>(x =>
            {
                if (x is string)
                {
                    _vm?.AddFeed(x as string);
                }
                _closeAction?.Invoke();
            });
        }

        RSSListFrameViewModel _vm;
        Action _closeAction;
        public RelayCommand<object> CloseDialogCommand { get; private set; }
    }
}