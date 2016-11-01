using CozyDump.Core;
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

namespace CozyDump.InfoView
{
    public partial class MainWindow : Window
    {
        MiniDumpParser _dumpParser;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonSelectFile_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.DefaultExt = ".dmp";
            ofd.Filter = "dump file|*.dmp";
            if (ofd.ShowDialog() == true)
            {
                textBoxDumpPath.Text = ofd.FileName;
            }
        }

        private void buttonAnalyse_Click(object sender, RoutedEventArgs e)
        {
            _dumpParser?.Dispose();
            _dumpParser = new MiniDumpParser();
            _dumpParser.Parse(textBoxDumpPath.Text);
            string output = "";
            for (var i = 0; i < _dumpParser.ModuleNums; ++i)
            {
                var moduleName = _dumpParser.GetStringFromRva(_dumpParser.ModuleInfo(i).ModuleNameRva);
                output += moduleName;
                output += "\r\n";
            }
            textBlock.Text = output;
        }
    }
}
