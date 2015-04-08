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
        public event Action<string, string> EventCreateTimer;

        public CreateTimerWindow()
        {
            InitializeComponent();
            var click = Observable.FromEventPattern<RoutedEventArgs>(CreateTimerBtn, "Click");
            click.Subscribe(_ =>
            {
                //触发事件
                TriggerEvent(TimerName.GetLineText(0), TimerInputTime.GetLineText(0));
                //MessageBox.Show(TimerName.GetLineText(0) + TimerInputTime.GetLineText(0));
            });
        }

        private void TriggerEvent(string timerName,string second)
        {
            if (EventCreateTimer != null)
            {
                //调用该事件注册的方法
                EventCreateTimer(timerName, second);
            }
        }
    }
}
