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

namespace CozyBili.WpfExe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private LiveDanMu biliLive;

        public MainWindow()
        {
            InitializeComponent();

            biliLive = new LiveDanMu(34083);
            biliLive.OnlineNumChanged += OnlineNumChanged;
            biliLive.ReceiveDanMu += ShowDanMu;

            biliLive.Run();
        }

        static void ShowDanMu(DanMuModel danMuModel)
        {
            DanmakuWindow w = new DanmakuWindow();
            w.Show();
        }

        static void OnlineNumChanged(int num)
        {
        }

        private void OnClose(object sender, System.ComponentModel.CancelEventArgs e)
        {
            biliLive.Stop();
        }
    }
}
