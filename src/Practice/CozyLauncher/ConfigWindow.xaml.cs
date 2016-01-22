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
using CozyLauncher.Infrastructure.Hotkey;

namespace CozyLauncher
{
    /// <summary>
    /// ConfigWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ConfigWindow : Window
    {
        public ConfigWindow()
        {
            InitializeComponent();
        }

        private void HotkeyBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled   = true;
            Key key     = (e.Key == Key.System ? e.SystemKey : e.Key);

            ModifyKeyStatus status = GlobalHotkey.Instance.ModifyKeyStatus;

            var hkm         = new HotkeyModel(status, key);
            var hotkeyStr   = hkm.ToString();
            if(hotkeyStr == this.HotkeyBox.Text)
            {
                return;
            }
            this.HotkeyBox.Text = hotkeyStr;
        }

        private void submit_Click(object sender, RoutedEventArgs e)
        {
            var hkm = new HotkeyModel(this.HotkeyBox.Text);

            ModifierKeys keys = hkm.ModifierKeyStatus;
            GlobalHotkey.Instance.RegisterHotkey("HotKey.ShowApp", hkm);

            //NHotkey.Wpf.HotkeyManager.Current.AddOrReplace("HotKey.ShowApp", Key.Q, keys, (s, ee) =>
            //{
            //    GlobalHotkey.Instance.InvokeHotkeyAction("HotKey.ShowApp");
            //});
        }
    }
}
