using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CozySql.Model.ViewModels;

namespace CozySql.Exe.UserControls
{
    /// <summary>
    /// Interaction logic for WelcomePage.xaml
    /// </summary>
    public partial class WelcomePage : UserControl
    {
        private readonly WelcomePageViewModel _viewMode;
        public WelcomePage()
        {
            InitializeComponent();
            _viewMode = new WelcomePageViewModel();
            _viewMode.PropertyChanged += ViewModelPropertyChanged;

            _viewMode.TestData();
        }

        void ViewModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "Data")
            {
                WelcomeList.ItemsSource = _viewMode.Data;
            }
        }
    }
}
