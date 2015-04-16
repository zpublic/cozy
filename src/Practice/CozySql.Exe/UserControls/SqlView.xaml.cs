using CozySql.Exe.ViewModels;
using System.Windows.Controls;

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
