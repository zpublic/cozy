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

namespace WpfCozy.A
{
    /// <summary>
    /// Interaction logic for StylesAndResources.xaml
    /// </summary>
    public partial class StylesAndResources : Window
    {
        public StylesAndResources()
        {
            InitializeComponent();
        }

        private void OnResources(object sender, RoutedEventArgs e)
        {
            new ResourceDemo().Show();
        }
    }
}
