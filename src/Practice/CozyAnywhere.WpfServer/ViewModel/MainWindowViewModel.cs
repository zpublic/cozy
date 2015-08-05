using CozyAnywhere.ServerCore;
using CozyAnywhere.WpfServer.Command;
using System;
using System.Windows.Input;

namespace CozyAnywhere.WpfServer.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        public AnywhereServer serverCore { get; set; }

        private ICommand _ConnectCommand;

        public ICommand ConnectCommand
        {
            get
            {
                return _ConnectCommand = _ConnectCommand ?? new DelegateCommand((x) =>
                {
                    serverCore.Connect(Address, Port);
                });
            }
        }

        private string _Address = "127.0.0.1";

        public string Address
        {
            get
            {
                return _Address;
            }
            set
            {
                Set(ref _Address, value, "Address");
            }
        }

        private int _Port = 48360;
        public int Port
        {
            get
            {
                return _Port;
            }
            set
            {
                Set(ref _Port, value, "Port");
            }
        }


        public MainWindowViewModel()
        {
            serverCore = new AnywhereServer();
            SetUpdateTimer();
        }

        private void SetUpdateTimer()
        {
            var dispatcherTimer     = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick    += (sender, msg) =>
            {
                if (serverCore != null)
                {
                    serverCore.Update();
                }
            };
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }
    }
}