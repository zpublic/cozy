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

namespace CozyLauncher.Tool.Update
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.ViewModel.PropertyChanged += ViewModel_PropertyChanged;
            this.ViewModel.DoUpdate();
        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "UpdateCommand.Show")
            {
                Show();
            }
            else if(e.PropertyName == "UpdateCommand.Exit")
            {
                App.Current.Shutdown();
            }
        }
    }
}
