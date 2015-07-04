using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerTester.Model;
using NetworkServer;
using NetworkHelper.Event;
using ServerTester.Command;
using System.Windows.Threading;
using System.Windows.Input;

namespace ServerTester.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        private ObservableCollection<FileInfo> _FileInfoList = new ObservableCollection<FileInfo>();
        public ObservableCollection<FileInfo> FileInfoList
        {
            get
            {
                return _FileInfoList;
            }
            set
            {
                Set(ref _FileInfoList, value, "FileInfoList");
            }
        }

        private bool IsListing { get; set; }

        private string _ListenButton = "Listen";
        public string ListenButton
        {
            get
            {
                return _ListenButton;
            }
            set
            {
                Set(ref _ListenButton, value, "ListenButton");
            }
        }

        #region Network

        private Server server { get; set; }

        private DispatcherTimer timer { get; set; }

        #endregion

        #region Command
        private ICommand _ListenCommand;
        public ICommand ListenCommand
        {
            get
            {
                return _ListenCommand = _ListenCommand ?? new DelegateCommand((x) =>
                    {
                        if (IsListing)
                        {
                            server.Shutdown();
                            ListenButton = "Listen";
                        }
                        else
                        {
                            server.Listen();
                            ListenButton = "shutdown";
                        }
                        IsListing = !IsListing;
                    });
            }
        }

        #endregion

        public MainWindowViewModel()
        {
            server = new Server(1000, 36048);
            server.StatusMessage += new EventHandler<StatusMessageArgs>(OnStatusMessage);
            server.DataMessage += new EventHandler<DataMessageArgs>(OnDataMessage);

            RegisterTimer();
        }

        private void RegisterTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 200);
            timer.Tick += new EventHandler((sender, msg) => { server.RecivePacket(); });
            timer.Start();
        }

        private void OnDataMessage(object sender, DataMessageArgs msg)
        {

        }

        private void OnStatusMessage(object sender, StatusMessageArgs msg)
        {
            if (msg.Status == NetworkHelper.NetConnectionStatus.Connected)
            {
                FileInfoList.Add(
                    new FileInfo
                    {
                        Name = "TestName",
                        Size = 233,
                        IsFolder = true,
                    }
                    );
            }
        }
    }
}
