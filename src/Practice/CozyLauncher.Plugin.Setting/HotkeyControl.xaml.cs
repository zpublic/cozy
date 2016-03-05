using CozyLauncher.Infrastructure.Hotkey;
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

namespace CozyLauncher.Plugin.Setting
{
    /// <summary>
    /// HotkeyControl.xaml 的交互逻辑
    /// </summary>
    public partial class HotkeyControl : UserControl
    {
        public static readonly DependencyProperty HotkeyTextProperty =
            DependencyProperty.Register("HotkeyText", typeof(string), typeof(HotkeyControl), new PropertyMetadata(new PropertyChangedCallback(OnHotkeyTextChanged)));

        private static void OnHotkeyTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = d as HotkeyControl;
            if (obj != null)
            {
                obj.HotkeyTextBox.Text = (string)e.NewValue;
            }
        }

        public string HotkeyText
        {
            get
            {
                return (string)this.GetValue(HotkeyTextProperty);
            }
            set
            {
                this.SetValue(HotkeyTextProperty, value);
            }
        }

        public HotkeyControl()
        {
            InitializeComponent();
        }

        private void HotkeyTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled   = true;
            Key key     = (e.Key == Key.System ? e.SystemKey : e.Key);

            var hkm         = new HotkeyModel(GlobalHotkey.Instance.ModifyKeyStatus, key);
            var hotkeyStr   = hkm.ToString();
            if (hotkeyStr == HotkeyText)
            {
                return;
            }
            HotkeyText = hotkeyStr;
        }
    }
}
