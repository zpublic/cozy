using System.Windows.Controls;

namespace CozySql.Exe.UserControls
{
    /// <summary>
    /// Interaction logic for WelcomePage.xaml
    /// </summary>
    public partial class WelcomePage : UserControl
    {
        public WelcomePage()
        {
            InitializeComponent();
            this.ViewMode.PropertyChanged += ViewModelPropertyChanged;
        }

        void ViewModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "Data")
            {

            }
        }
    }
}
