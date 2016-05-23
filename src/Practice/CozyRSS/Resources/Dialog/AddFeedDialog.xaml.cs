using CozyRSS.ViewModel;
using CozyRSS.ViewModel.Dialog;
using System;
using System.Windows;

namespace CozyRSS.Resources.Dialog
{
    /// <summary>
    /// AddFeedDialog.xaml 的交互逻辑
    /// </summary>
    public partial class AddFeedDialog : Window
    {
        public AddFeedDialog(RSSListFrameViewModel RSSListFrameViewModel)
        {
            InitializeComponent();
            DataContext = new AddFeedDialogViewModel(RSSListFrameViewModel, new Action(() =>
            {
                this.Close();
            }));
        }
    }
}
