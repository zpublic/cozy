using BiliDMLib;
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
        private DanmakuLoader biliLive;

        private const int _maxCapacity = 10;
        private Queue<string> _messageQueue = new Queue<string>(_maxCapacity);

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnClose(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (biliLive != null)
            {
                biliLive.Disconnect();
            }
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            button.IsEnabled = false;
            button1.IsEnabled = true;

            string roomId = textBox.GetLineText(0);
            int nId = int.Parse(roomId);

            if (biliLive == null)
            {
                biliLive = new BiliDMLib.DanmakuLoader();
                biliLive.Disconnected += b_Disconnected;
                biliLive.ReceivedDanmaku += b_ReceivedDanmaku;
                biliLive.ReceivedRoomCount += b_ReceivedRoomCount;
                await biliLive.ConnectAsync(nId);
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            button1.IsEnabled = false;
            button.IsEnabled = true;

            if (biliLive != null)
            {
                biliLive.Disconnect();
                biliLive = null;
            }
        }

        private void b_ReceivedRoomCount(object sender, ReceivedRoomCountArgs e)
        {
            string s = e.UserCount + "";
        }

        private void b_ReceivedDanmaku(object sender, ReceivedDanmakuArgs e)
        {
            switch (e.Danmaku.MsgType)
            {
                case MsgTypeEnum.Comment:
                    if (textBox1.Dispatcher.CheckAccess())
                    {
                        _messageQueue.Enqueue("收到彈幕:" + (e.Danmaku.isAdmin ? "[管]" : "") + (e.Danmaku.isVIP ? "[爷]" : "") + e.Danmaku.CommentUser + " 說: " + e.Danmaku.CommentText);
                        textBox1.Text = string.Join("\n", _messageQueue);
                        textBox1.ScrollToEnd();
                    }
                    else
                    {
                        textBox1.Dispatcher.BeginInvoke(
                            DispatcherPriority.Normal,
                            new Action(() => b_ReceivedDanmaku(sender, e)));
                    }
                    break;
                case MsgTypeEnum.GiftTop:
                    break;
                case MsgTypeEnum.GiftSend:
                    {
                        break;
                    }
                case MsgTypeEnum.Welcome:
                    {
                        break;
                    }
            }
        }

        private void b_Disconnected(object sender, DisconnectEvtArgs args)
        {
        }
    }
}
