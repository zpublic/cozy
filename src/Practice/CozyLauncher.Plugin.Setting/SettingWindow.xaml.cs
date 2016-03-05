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
using MahApps.Metro.Controls;
using CozyLauncher.PluginBase;

namespace CozyLauncher.Plugin.Setting
{
    /// <summary>
    /// SettingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SettingWindow : MetroWindow
    {
        private PluginInitContext _context { get; set; }

        public SettingWindow(PluginInitContext context = null)
        {
            InitializeComponent();

            _context = context;

            this.ViewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "SystemCommand.SaveSetting")
            {
                _context?.Api.RunCommand("SystemCommand.SaveSetting");
            }
        }
    }
}
