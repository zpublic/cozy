using MahApps.Metro;
using MahApps.Metro.Controls;
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
using CozySqlite.Model;

namespace CozySqlite.Exe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            ChangedTheme();
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
