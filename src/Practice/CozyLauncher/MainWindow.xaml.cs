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
using CozyLauncher.Infrastructure.Hotkey;

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

            this.QueryTextBox.Focus();
        }

        private void OnWindowMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.DragMove();
        }

        private void OnQueryKeyUp(object sender, KeyEventArgs e)
        {
            var oldHandle = e.Handled;
            e.Handled = true;
            Key key = (e.Key == Key.System ? e.SystemKey : e.Key);

            switch (key)
            {
                case Key.Up:
                    this.ViewModel.UpCommand.Execute(null);
                    break;
                case Key.Down:
                    this.ViewModel.DownCommand.Execute(null);
                    break;
                case Key.Enter:
                    this.ViewModel.DoCommand.Execute(null);
                    break;
                case Key.Escape:
                    this.ViewModel.CloseApp();
                    break;
                default:
                    e.Handled = oldHandle;
                    break;
            }
        }

        private void OnQueryTextChanged(object sender, TextChangedEventArgs e)
        {
            var textbox = sender as TextBox;
            if (textbox != null)
            {
                var text = textbox.Text.Trim();
                if (!string.IsNullOrWhiteSpace(text))
                {
                    this.ViewModel.QueryCommand.Execute(text);
                }
            }
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SystemCommand.HideApp")
            {
                HideApp();
            }
            else if (e.PropertyName == "SystemCommand.CloseApp")
            {
                CloseApp();
            }
            else if (e.PropertyName == "SystemCommand.ShowApp")
            {
                ShowApp();
            }
            else if(e.PropertyName == "SystemCommand.ShowConfig")
            {
                ShowConfig();
            }
            else if(e.PropertyName == "SystemCommand.About")
            {
                About();
            }
        }

        private void HideWox()
        {
            Hide();
        }

        private void ShowWox()
        {
            Show();
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

        public void ShowConfig()
        {
            var config = new ConfigWindow();
            config.ShowDialog();
        }

        public void About()
        {
            var about = new AboutWindow();
            about.ShowDialog();
        }
    }
}
