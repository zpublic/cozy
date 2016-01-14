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

namespace CozyLauncher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IPublicApi
    {
        PluginMgr pm = new PluginMgr();

        public MainWindow()
        {
            InitializeComponent();
            pm.Init(this);
        }

        private void HideWox()
        {
            Hide();
        }

        public void CloseApp()
        {
            Application.Current.Shutdown();
        }

        public void HideApp()
        {
            Dispatcher.Invoke(HideWox);
        }

        public void PushResults(List<Result> results)
        {
            ;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Query q = new Query();
            q.RawQuery = textBox.Text;
            pm.Query(q);
        }
    }
}
