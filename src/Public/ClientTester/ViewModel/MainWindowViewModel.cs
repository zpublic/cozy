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
using NetworkHelper.Messages;
using NetworkProtocol;
using CozyAnywhere.Protocol;
using CozyAnywhere.Protocol.Messages;
using System.Runtime.InteropServices;
using ClientTester.Model;

namespace ClientTester.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        Client client { get; set; }

        private DispatcherTimer timer { get; set; }

        public ICommand _TestCommand;
        public ICommand TestCommand
        {
            get
            {
                return _TestCommand = _TestCommand ?? new DelegateCommand((x) => 
                {
                    SendTestData();
                });
            }
        }

        public MainWindowViewModel()
        {
            client = new Client();
            client.Connect("127.0.0.1", 36048);
            RegisterTimer();
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

        private void SendTestData()
        {
            var fileList = new List<Tuple<string, uint, bool>>();
            FileUtil.FileEnum(@"D:\*", (file, b) =>
            {
                var filename = Marshal.PtrToStringAuto(file);
                fileList.Add(Tuple.Create<string, uint, bool>(filename, 0, b));
            });

            var msg = new FileEnumMessage();
            msg.FileInfoList = fileList;
            client.SendMessage(msg);
        }

        private void OnDataMessage(object sender, DataMessageArgs msg)
        {
            uint id = msg.Input.ReadUInt32();

            switch(id)
            {
                case DefaultMessageId.CommandMessage:
                    var commandMsg = new CommandMessage();
                    commandMsg.Read(msg.Input);

                    uint commandId = commandMsg.CommandId;
                    switch(commandId)
                    {
                        case CommandId.FileEnumCommand:
                            SendTestData();
                            break;
                        default:
                            break;
                    }
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
