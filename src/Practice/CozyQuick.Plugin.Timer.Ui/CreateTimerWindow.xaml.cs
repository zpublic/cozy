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
using System.Reactive.Linq;

namespace CozyQuick.Plugin.Timer.Ui
{
    /// <summary>
    /// Interaction logic for CreateTimerWindow.xaml
    /// </summary>
    public partial class CreateTimerWindow : Window
    {
        public CreateTimerWindow()
        {
            InitializeComponent();
            var click = Observable.FromEventPattern<RoutedEventArgs>(CreateTimerBtn, "Click");
            click.Subscribe(_ =>
            {
                MessageBox.Show(TimerName.GetLineText(0) + TimerInputTime.GetLineText(0));
            });
        }
    }
}
