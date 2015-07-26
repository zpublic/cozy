using CozyAnywhere.ClientCore;
using CozyAnywhere.ClientCore.EventArg;
using CozyAnywhere.WpfClient.Command;
using CozyAnywhere.WpfClient.Factory;
using CozyAnywhere.WpfClient.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CozyAnywhere.WpfClient.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        private int _Port;
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

        private string _Address = "114.215.134.101";
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

        private List<string> _PluginList = new List<string>();

        public List<string> PluginList
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

        private ICommand _ConnectCommand;

        public ICommand ConnectCommand
        {
            get
            {
                return _ConnectCommand = _ConnectCommand ?? new DelegateCommand((x)=>
                {
                    if (Address != null)
                    {
                        if(!clientCore.IsListing)
                        {
                            clientCore.Listen();
                        }
                        clientCore.ConnectServer(Address, 48360);
                    }
                });
            }
        }


        public MainWindowViewModel()
        {
            RegisterControls();
            Port                            = 48360;
            clientCore                      = new AnywhereClient(1000, Port);
            BindCoreCollections();
            clientCore.PluginChangedHandler += new EventHandler<PluginChangedEvnetArgs>(OnPluginChanged);
            SetUpdateTimer();
        }

        private Dictionary<string, IControlFactory> ControlCreateDictionary = new Dictionary<string, IControlFactory>();

        private void RegisterControl<T>()
            where T : IControlFactory, new()
        {
            T t = new T();
            ControlCreateDictionary[t.ProductName] = t;
        }

        private void RegisterControls()
        {
            RegisterControl<FilePluginPageFactory>();
            RegisterControl<ProcessPluginPageFactory>();
            RegisterControl<CapturePluginPageFactory>();
        }

        private void OnPluginChanged(object sender, PluginChangedEvnetArgs e)
        {
            foreach (var obj in e.NewPlugins)
            {
                if (ControlCreateDictionary.ContainsKey(obj))
                {
                    ControlList.Add(ControlCreateDictionary[obj].Create());
                }
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