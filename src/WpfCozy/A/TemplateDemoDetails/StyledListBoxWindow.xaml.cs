using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfCozy.A.TemplateDemoDetails
{
    /// <summary>
    /// Interaction logic for StyledListBoxWindow1.xaml
    /// </summary>
    public partial class StyledListBoxWindow : Window
    {
        public StyledListBoxWindow()
        {
            InitializeComponent();
            this.DataContext = Countries.GetCountries();
        }
    }
}
