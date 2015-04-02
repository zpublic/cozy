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
using WpfCozy.A.TemplateDemoDetails;

namespace WpfCozy.A
{
    /// <summary>
    /// Interaction logic for TemplateDemo.xaml
    /// </summary>
    public partial class TemplateDemo : Window
    {
        public TemplateDemo()
        {
            InitializeComponent();
        }

        private void OnStyledButton(object sender, RoutedEventArgs e)
        {
            new StyledButtonWindow().Show();
        }

        private void OnStyledListBox(object sender, RoutedEventArgs e)
        {
            new StyledListBoxWindow().Show();
        }
    }
}
