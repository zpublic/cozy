using CozyBili.Core;
using CozyBili.Core.Models;
using CozyBili.Danmaku.View;
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
using System.Windows.Threading;

namespace CozyBili.WpfExe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private LiveDanMu biliLive;

        private const int _maxCapacity = 10;
        private Queue<string> _messageQueue = new Queue<string>(_maxCapacity);

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ShowDanMu(DanMuModel danMuModel)
        {
            if (textBox1.Dispatcher.CheckAccess())
            {
                _messageQueue.Enqueue(danMuModel.UserName + ": " + danMuModel.Content);
                textBox1.Text = string.Join("\n", _messageQueue);
                textBox1.ScrollToEnd();
            }
            else
            {
                textBox1.Dispatcher.BeginInvoke(
                    DispatcherPriority.Normal,
                    new Action(() => ShowDanMu(danMuModel)));
            }

            //DanmakuWindow w = new DanmakuWindow();
            //w.Show();
        }

        private void OnlineNumChanged(int num)
        {
        }

        private void OnClose(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (biliLive != null)
            {
                biliLive.Stop();
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            button.IsEnabled = false;
            button1.IsEnabled = true;

            string roomId = textBox.GetLineText(0);
            int nId = int.Parse(roomId);

            if (biliLive == null)
            {
                biliLive = new LiveDanMu(nId);
                biliLive.OnlineNumChanged += OnlineNumChanged;
                biliLive.ReceiveDanMu += ShowDanMu;
                biliLive.Run();
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            button1.IsEnabled = false;
            button.IsEnabled = true;

            if (biliLive != null)
            {
                biliLive.Stop();
                biliLive = null;
            }
        }
    }
}
