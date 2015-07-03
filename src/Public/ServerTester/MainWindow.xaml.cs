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
using NetworkServer;
using System.Windows.Threading;

namespace ServerTester
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Server server { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            server = new Server(1000, 36048);
            server.StatusMessage += (sender, msg) =>
                {
                    MessageBox.Show("fuck");
                };

            System.Threading.ThreadStart s = () =>
                {
                    server.EnterMainLoop();
                };
            new System.Threading.Thread(s).Start();
            
        }
    }
}
