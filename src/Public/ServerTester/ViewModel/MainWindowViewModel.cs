using CozyAnywhere.Protocol;
using CozyAnywhere.Protocol.Messages;
using NetworkHelper;
using NetworkHelper.Event;
using NetworkProtocol;
using NetworkServer;
using ServerTester.Command;
using ServerTester.Model;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Threading;

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

        public FileInfo FileInfoListSelectedItem { get; set; }

        private ObservableCollection<ProcessInfo> _ProcessInfoList = new ObservableCollection<ProcessInfo>();

        public ObservableCollection<ProcessInfo> ProcessInfoList
        {
            get
            {
                return _ProcessInfoList;
            }
            set
            {
                Set(ref _ProcessInfoList, value, "ProcessInfoList");
            }
        }

        public ProcessInfo ProcessInfoListSelectedItem { get; set; }

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

        #endregion Network

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

        private ICommand _FileDeleteCommand;
        public ICommand FileDeleteCommand
        {
            get
            {
                return _FileDeleteCommand = _FileDeleteCommand ?? new DelegateCommand((x) =>
                {
                    if(FileInfoListSelectedItem != null)
                    {
                        var deleteMsg   = new FileDeleteMessage();
                        deleteMsg.Path  = FileInfoListSelectedItem.Name;
                        server.SendMessage(deleteMsg);
                    }
                });
            }
        }

        private ICommand _ProcessTerminateCommand;
        public ICommand ProcessTerminateCommand
        {
            get
            {
                return _ProcessTerminateCommand = _ProcessTerminateCommand ?? new DelegateCommand((x) =>
                {
                    if(ProcessInfoListSelectedItem != null)
                    {
                        var TerminateMsg        = new ProcessTerminateMessage();
                        TerminateMsg.ProcessId  = ProcessInfoListSelectedItem.Pid;
                        server.SendMessage(TerminateMsg);
                    }
                });
            }
        }

        #endregion Command

        public MainWindowViewModel()
        {
            server = new Server(1000, 36048);
           
            RegisterEvent();
            RegisterMessageType();
            RegisterTimer();
        }

        private void OnDataMessage(object sender, DataMessageArgs msg)
        {
            IMessage baseMsg = MessageReader.GetTypeInstanceByStream(msg.Input);
            switch (baseMsg.Id)
            {
                case MessageId.FileEnumMessageRsp:
                    var enumMsg = (FileEnumMessageRsp)baseMsg;
                    foreach (var obj in enumMsg.FileInfoList)
                    {
                        FileInfoList.Add(
                            new FileInfo
                            {
                                Name        = obj.Item1,
                                Size        = obj.Item2,
                                IsFolder    = obj.Item3,
                            }
                            );
                    }
                    break;
                case MessageId.ProcessEnumMessageRsp:
                    var ProcEnumMsg = (ProcessEnumMessageRsp)baseMsg;
                    foreach(var obj in ProcEnumMsg.ProcessList)
                    {
                        ProcessInfoList.Add(new ProcessInfo
                        {
                            Pid     = obj.Item1,
                            Name    = obj.Item2,
                        }
                        );
                    }
                    break;
                default:
                    break;
            }
        }

        private void OnStatusMessage(object sender, StatusMessageArgs msg)
        {
            if (msg.Status == NetworkHelper.NetConnectionStatus.Connected)
            {
                const string TestPath = @"E:\";

                var enumMsg     = new FileEnumMessage();
                enumMsg.Path    = TestPath;
                server.SendMessage(enumMsg);

                var procMsg = new ProcessEnumMessage();
                server.SendMessage(procMsg);
            }
        }

        private void RegisterTimer()
        {
            timer           = new DispatcherTimer();
            timer.Interval  = new TimeSpan(0, 0, 0, 0, 200);
            timer.Tick      += new EventHandler((sender, msg) => { server.RecivePacket(); });
            timer.Start();
        }

        private void RegisterMessageType()
        {
            MessageReader.RegisterType<FileEnumMessageRsp>(MessageId.FileEnumMessageRsp);
            MessageReader.RegisterType<ProcessEnumMessageRsp>(MessageId.ProcessEnumMessageRsp);
        }

        private void RegisterEvent()
        {
            server.StatusMessage    += new EventHandler<StatusMessageArgs>(OnStatusMessage);
            server.DataMessage      += new EventHandler<DataMessageArgs>(OnDataMessage);
        }
    }
}