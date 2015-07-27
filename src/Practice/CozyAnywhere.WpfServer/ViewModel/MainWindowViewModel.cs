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
                    serverCore.Connect("127.0.0.1", 48360);
                });
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