using CozySql.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CozySql.Exe.UserControls
{
    /// <summary>
    /// Interaction logic for SqlView.xaml
    /// </summary>
    public partial class SqlView : UserControl
    {
        private readonly SqlViewViewModel _viewModel;

        public SqlView()
        {
            InitializeComponent();
            DataContext = _viewModel = new SqlViewViewModel();
            _viewModel.PropertyChanged += ViewModelPropertyChanged;

            var t1 = new System.Timers.Timer(3000);
            t1.AutoReset = true;
            t1.Elapsed += TimeAction;
            t1.Start();
        }

        void TimeAction(object sender, System.Timers.ElapsedEventArgs e)
        {
            _viewModel.TestData();
        }

        void ViewModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "hehe")
            {

            }
        }
    }
}
