using CozyAnywhere.ClientCore;
using CozyAnywhere.WpfClient.Command;
using System;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using CozyAnywhere.WpfClient.UserControls;
using CozyAnywhere.WpfClient.Model;
using System.ComponentModel;
using CozyAnywhere.ClientCore.EventArg;

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

        private ObservableCollection<string> _PluginList = new ObservableCollection<string>();
        public ObservableCollection<string> PluginList
        {
            get
            {
                return _PluginList;
            }
            set
            {
                Set(ref _PluginList, value, "PluginList");
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
            Port                            = 48360;
            clientCore                      = new AnywhereClient(1000, Port);
            BindCoreCollections();
            clientCore.PluginChangedHandler += new EventHandler<PluginChangedEvnetArgs>(OnPluginChanged);
            SetUpdateTimer();
        }

        private void OnPluginChanged(object sender, PluginChangedEvnetArgs e)
        {
            ControlList.Clear();
            if (PluginList.Contains("FilePlugin"))
            {
                var fileControl = new DefaultControlInfo()
                {
                    Name = "FilePlugin",
                    Controls = new FilePluginPage(),
                };
                ControlList.Add(fileControl);
            }
            if (PluginList.Contains("ProcessPlugin"))
            {
                var processControl = new DefaultControlInfo()
                {
                    Name = "ProcessPlugin",
                    Controls = new ProcessPluginPage(),
                };
                ControlList.Add(processControl);
            }
        }

        private void BindCoreCollections()
        {
            if (clientCore != null)
            {
                clientCore.PluginNameCollection = PluginList;
            }
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