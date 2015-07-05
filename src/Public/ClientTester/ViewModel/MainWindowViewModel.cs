using ClientTester.Command;
using ClientTester.Ext;
using CozyAnywhere.Protocol;
using CozyAnywhere.Protocol.Messages;
using NetworkHelper;
using NetworkHelper.Event;
using NetworkProtocol;
using NetwrokClient;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Input;
using System.Windows.Threading;

namespace ClientTester.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region Network

        private Client client { get; set; }

        private DispatcherTimer timer { get; set; }

        #endregion Network

        public ICommand _TestCommand;

        public ICommand TestCommand
        {
            get
            {
                return _TestCommand = _TestCommand ?? new DelegateCommand((x) =>
                {
                    client.Connect("127.0.0.1", 36048);
                });
            }
        }

        public MainWindowViewModel()
        {
            client = new Client();

            RegisterMessageType();
            RegisterTimer();
            RegisterEvent();
        }

        private void OnStatusMessage(object sender, StatusMessageArgs msg)
        {
            switch (msg.Status)
            {
                case NetworkHelper.NetConnectionStatus.Connected:
                    break;

                default:
                    break;
            }
        }

        private void OnDataMessage(object sender, DataMessageArgs msg)
        {
            IMessage baseMsg = MessageReader.GetTypeInstanceByStream(msg.Input);
            switch (baseMsg.Id)
            {
                case MessageId.FileEnumMessage:
                    var enumMsg         = (FileEnumMessage)baseMsg;
                    SendPathEnumData(enumMsg.Path);
                    break;
                case MessageId.ProcessEnumMessage:
                    SendProcessEnumData();
                    break;
                case MessageId.ProcessTerminateMessage:
                    var terminateMsg    = (ProcessTerminateMessage)baseMsg;
                    ProcessUtil.ProcessTerminate(terminateMsg.ProcessId);
                    break;
                case MessageId.FileDeleteMessage:
                    var deleteMsg       = (FileDeleteMessage)baseMsg;
                    FileUtil.FileDelete(deleteMsg.Path);
                    break;
                default:
                    break;
            }
        }

        private void SendPathEnumData(string path)
        {
            var actualPath  = path + '*';
            var fileList    = new List<Tuple<string, uint, bool>>();
            FileUtil.FileEnum(actualPath, (file, b) =>
            {
                var filename = Marshal.PtrToStringAuto(file);
                fileList.Add(Tuple.Create<string, uint, bool>(path + filename, 0, b));
                return false;
            });

            var msg             = new FileEnumMessageRsp();
            msg.FileInfoList    = fileList;
            client.SendMessage(msg);
        }

        private void SendProcessEnumData()
        {
            var processList = new List<Tuple<uint, string>>();
            ProcessUtil.ProcessEnum((pid) => 
            {
                string result = null;
                ProcessUtil.GetProcessName(pid, (x) =>
                {
                    result = Marshal.PtrToStringAuto(x);
                });
                if (result != null)
                {
                    processList.Add(Tuple.Create<uint, string>(pid, result));
                }
                return false;
            });

            var msg             = new ProcessEnumMessageRsp();
            msg.ProcessList     = processList;
            client.SendMessage(msg);
        }

        private void RegisterEvent()
        {
            client.DataMessage      += new EventHandler<DataMessageArgs>(OnDataMessage);
            client.StatusMessage    += new EventHandler<StatusMessageArgs>(OnStatusMessage);
        }

        private void RegisterMessageType()
        {
            MessageReader.RegisterType<FileEnumMessage>(MessageId.FileEnumMessage);
            MessageReader.RegisterType<ProcessEnumMessage>(MessageId.ProcessEnumMessage);
            MessageReader.RegisterType<FileDeleteMessage>(MessageId.FileDeleteMessage);
            MessageReader.RegisterType<ProcessTerminateMessage>(MessageId.ProcessTerminateMessage);
        }

        private void RegisterTimer()
        {
            timer           = new DispatcherTimer();
            timer.Interval  = new TimeSpan(0, 0, 0, 0, 200);
            timer.Tick      += new EventHandler((sender, msg) => { client.Update(); });
            timer.Start();
        }
    }
}