using CozyLauncher.Core.Plugin;
using CozyLauncher.PluginBase;
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
using System.ComponentModel;

namespace CozyLauncher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.ViewModel.PropertyChanged += OnViewModelPropertyChanged;
            this.InnerTextBox.TextChanged += (o, e) =>
            {
                var text = InnerTextBox.Text.Trim();
                if (!string.IsNullOrWhiteSpace(text))
                {
                    this.ViewModel.QueryCommand.Execute(text);
                }
            };
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "HideApp")
            {
                HideApp();
            }
            else if(e.PropertyName == "CloseApp")
            {
                CloseApp();
            }
            else if(e.PropertyName == "ShowApp")
            {
                ShowApp();
            }
        }

        private void HideWox()
        {
            Hide();
        }

        private void ShowWox()
        {

        }

        public void CloseApp()
        {
            Application.Current.Shutdown();
        }

        public void HideApp()
        {
            Dispatcher.Invoke(HideWox);
        }

        public void ShowApp()
        {
            Dispatcher.Invoke(ShowWox);
        }
    }
}
