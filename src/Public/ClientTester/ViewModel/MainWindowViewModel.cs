using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using NetwrokClient;

namespace ClientTester.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        Client client { get; set; }

        private DispatcherTimer timer { get; set; }

        public MainWindowViewModel()
        {
            client = new Client();
            client.Connect("127.0.0.1", 36048);

            RegisterTimer();
        }

        private void RegisterTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 200);
            timer.Tick += new EventHandler((sender, msg) => { client.Update(); });
            timer.Start();
        }
    }
}
