using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using NetwrokClient;
using System.Windows.Input;
using ClientTester.Command;
using ClientTester.Ext;
using NetworkHelper.Event;
using Lidgren.Network;
using NetworkProtocol;
using CozyAnywhere.Protocol;
using CozyAnywhere.Protocol.Messages;
using System.Runtime.InteropServices;
using ClientTester.Model;

namespace ClientTester.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region Network

        Client client { get; set; }

        private DispatcherTimer timer { get; set; }

        #endregion

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
            
            RegisterTimer();
            RegisterEvent();
        }

        private void RegisterEvent()
        {
            client.DataMessage += new EventHandler<DataMessageArgs>(OnDataMessage);
            client.StatusMessage += new EventHandler<StatusMessageArgs>(OnStatusMessage);
        }

        private void OnStatusMessage(object sender, StatusMessageArgs msg)
        {
            switch(msg.Status)
            {
                case NetworkHelper.NetConnectionStatus.Connected:
                    break;
                default:
                    break;
            }
        }

        private void SendPathEnumData(string path)
        {
            var fileList = new List<Tuple<string, uint, bool>>();
            FileUtil.FileEnum(path, (file, b) =>
            {
                var filename = Marshal.PtrToStringAuto(file);
                fileList.Add(Tuple.Create<string, uint, bool>(filename, 0, b));
            });

            var msg = new FileEnumMessageRsp();
            msg.FileInfoList = fileList;
            client.SendMessage(msg);
        }

        private void OnDataMessage(object sender, DataMessageArgs msg)
        {
            uint id = msg.Input.ReadUInt32();
            switch(id)
            {
                case MessageId.FileEnumMessage:
                    var enumMsg = new FileEnumMessage();
                    enumMsg.Read(msg.Input);
                    SendPathEnumData(enumMsg.Path);
                    break;
                default:
                    break;
            }
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
