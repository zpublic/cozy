using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MvvmCozy.View;
using System.Windows;

namespace MvvmCozy.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
        }

        private RelayCommand _gotoView1Command;

        public RelayCommand GotoView1Command
        {
            get
            {
                return _gotoView1Command
                    ?? (_gotoView1Command = new RelayCommand(
                    () =>
                    {
                        MessageBox.Show("Navigate to Page 2!");
                        MvvmView1 v = new MvvmView1();
                        v.Show();
                    }));
            }
        }
    }
}