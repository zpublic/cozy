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
using NHotkey.Wpf;

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
            ReadHotkeyConfig();
        }

        private void ReadHotkeyConfig()
        {
            var hkm = GlobalHotkey.Instance.GetRegistedHotkey("HotKey.ShowApp");
            if(hkm != null)
            {
                this.HotkeyBox.Text = hkm.ToString();
            }
        }

        private void HotkeyBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled   = true;
            Key key     = (e.Key == Key.System ? e.SystemKey : e.Key);

            var hkm         = new HotkeyModel(GlobalHotkey.Instance.ModifyKeyStatus, key);
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
            if(hkm.CharKey != Key.None)
            {
                GlobalHotkey.Instance.RegisterHotkey("HotKey.ShowApp", hkm);

                HotkeyManager.Current.AddOrReplace("HotKey.ShowApp", hkm.CharKey, hkm.ModifierKeyStatus, (s, ee) =>
                {
                    GlobalHotkey.Instance.InvokeHotkeyAction("HotKey.ShowApp");
                });
            }
            else
            {
                MessageBox.Show("regist hotkey failed");
            }
        }
    }
}
