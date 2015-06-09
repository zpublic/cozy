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
using System.ComponentModel;

namespace CozyMabi.WpfExe
{
    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            ViewModel.PropertyChanged += PropertyChangedEvent;
        }

        private void PropertyChangedEvent(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Result")
            {
                this.DialogResult = ViewModel.Result;
            }
        }
    }
}
