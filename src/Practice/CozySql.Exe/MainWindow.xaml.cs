using MahApps.Metro;
using MahApps.Metro.Controls;

namespace CozySql.Exe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            //ChangedTheme();
            this.ViewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "SelectedAccent":
                case "IsLight":
                    ChangedTheme();
                    break;
                default:
                    break;
            }
        }

        void ChangedTheme()
        {
            ThemeManager.ChangeAppStyle(App.Current,
                                   ThemeManager.GetAccent(ViewModel.SelectedAccent),
                                   ThemeManager.GetAppTheme(ViewModel.IsLight ? ViewModel.Themes[0] : ViewModel.Themes[1]));
        }
    }
}
