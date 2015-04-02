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

namespace WpfCozy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnBtnShapesDemo(object sender, RoutedEventArgs e)
        {
            ShapesDemo w = new ShapesDemo();
            w.ShowDialog();
        }

        private void OnBtnGeometryDemo(object sender, RoutedEventArgs e)
        {
            GeometryDemo w = new GeometryDemo();
            w.ShowDialog();
        }
    }
}
