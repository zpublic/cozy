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
using System.Windows.Shapes;
using WpfCozy.A.TriggerDemoDetails;

namespace WpfCozy.A
{
    /// <summary>
    /// Interaction logic for TriggerDemo.xaml
    /// </summary>
    public partial class TriggerDemo : Window
    {
        public TriggerDemo()
        {
            InitializeComponent();
        }

        private void OnPropertyTrigger(object sender, RoutedEventArgs e)
        {
            new PropertyTriggerWindow().Show();
        }

        private void OnMultiTrigger(object sender, RoutedEventArgs e)
        {
            new MultiTriggerWindow().Show();
        }

        private void OnDataTrigger(object sender, RoutedEventArgs e)
        {
            new DataTriggerWindow().Show();
        }
    }
}
