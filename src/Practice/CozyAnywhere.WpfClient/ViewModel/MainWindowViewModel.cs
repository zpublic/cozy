using CozyAnywhere.ClientCore;
using CozyAnywhere.WpfClient.Command;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CozyAnywhere.WpfClient.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        private ObservableCollection<Tuple<string, bool>> _FileList = new ObservableCollection<Tuple<string, bool>>();

        public ObservableCollection<Tuple<string, bool>> FileList
        {
            get
            {
                return _FileList;
            }
            set
            {
                Set(ref _FileList, value, "FileList");
            }
        }

        public int Port { get; set; }

        public AnywhereClient clientCore { get; set; }

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
            Port = 48360;
            clientCore = new AnywhereClient(1000, Port);
            BindCoreCollections();
            SetUpdateTimer();
        }

        private void BindCoreCollections()
        {
            if (clientCore != null)
            {
                clientCore.FileCollection = FileList;
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