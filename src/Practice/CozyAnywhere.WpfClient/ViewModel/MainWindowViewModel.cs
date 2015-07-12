using CozyAnywhere.ClientCore;
using CozyAnywhere.WpfClient.Command;
using System;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using CozyAnywhere.WpfClient.UserControls;
using CozyAnywhere.WpfClient.Model;

namespace CozyAnywhere.WpfClient.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        public int Port { get; set; }

        public static AnywhereClient clientCore { get; set; }

        private ObservableCollection<DefaultControlInfo> _ControlList = new ObservableCollection<DefaultControlInfo>();
        public ObservableCollection<DefaultControlInfo> ControlList
        {
            get
            {
                return _ControlList;
            }
            set
            {
                Set(ref _ControlList, value, "ControlList");
            }
        }

        private string _ListenButtonText = "Listen";

        public string ListenButtonText
        {
            get
            {
                return _ListenButtonText;
            }
            set
            {
                Set(ref _ListenButtonText, value, "ListenButtonText");
            }
        }

        private ICommand _ListenCommand;

        public ICommand ListenCommand
        {
            get
            {
                return _ListenCommand = _ListenCommand ?? new DelegateCommand((x) =>
                {
                    if (clientCore.IsListing)
                    {
                        clientCore.Shutdown();
                        ListenButtonText = "Listen";
                    }
                    else
                    {
                        clientCore.Listen();
                        ListenButtonText = "Shutdown";
                    }
                });
            }
        }

        public MainWindowViewModel()
        {
            Port        = 48360;
            clientCore  = new AnywhereClient(1000, Port);
            SetUpdateTimer();
            LoadControls();
        }

        private void LoadControls()
        {
            var fileControl = new DefaultControlInfo()
            {
                Name = "File",
               Controls = new FilePluginPage(),
            };
            ControlList.Add(fileControl);

            var processControl = new DefaultControlInfo()
            {
                Name = "Process",
                Controls = new ProcessPluginPage(),
            };
            ControlList.Add(processControl);
        }

        private void SetUpdateTimer()
        {
            var dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += (sender, msg) =>
            {
                if (clientCore != null)
                {
                    clientCore.Update();
                }
            };
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }
    }
}