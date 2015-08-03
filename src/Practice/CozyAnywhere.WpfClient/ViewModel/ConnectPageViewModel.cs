using CozyAnywhere.ClientCore;
using CozyAnywhere.ClientCore.EventArg;
using System;
using CozyAnywhere.WpfClient.Model;
using System.Collections.ObjectModel;

namespace CozyAnywhere.WpfClient.ViewModel
{
    public class ConnectPageViewModel : BaseViewModel
    {
        public AnywhereClient clientCore { get; set; }

        private ObservableCollection<ConnectInfo> _ConnectList = new ObservableCollection<ConnectInfo>();
        public ObservableCollection<ConnectInfo> ConnectList
        {
            get
            {
                return _ConnectList;
            }
            set
            {
                Set(ref _ConnectList, value, "ConnectList");
            }
        }

        public ConnectPageViewModel()
        {
            clientCore                          = MainWindowViewModel.clientCore;
            clientCore.ServerConnectHandler     += new EventHandler<ServerConnectEventArgs>(OnConnectAttach);
        }

        private void OnConnectAttach(object sender, ServerConnectEventArgs msg)
        {
            var connInfo = new ConnectInfo()
            {
                Address = msg.Address,
                Name    = msg.Name,
                Info    = msg.Infomation,
            };
            ConnectList.Add(connInfo);
        }
    }
}
