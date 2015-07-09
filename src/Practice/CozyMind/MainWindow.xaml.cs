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

namespace CozyMind
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel vm = new MainWindowViewModel();

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this.vm;
            mindEditor.Options.ShowTabs = true;
            mindEditor.Options.ShowSpaces = true;
            mindEditor.Options.ConvertTabsToSpaces = true;
            mindEditor.Options.HighlightCurrentLine = true;
            mindEditor.Options.EnableHyperlinks = true;
            mindEditor.ShowLineNumbers = true;
        }
    }
}
