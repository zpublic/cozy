using CozyThunder.DistributedDownload.MasterGui.MessageCenter;
using CozyThunder.DistributedDownload.MasterGui.Models;
using CozyThunder.DistributedDownload.MasterGui.Models.Comparer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CozyThunder.DistributedDownload.MasterGui.ViewModels
{
    public partial class MainWindowViewModel
    {
        public ObservableCollection<PeerInfo> PeerInfoList { get; set; } = new ObservableCollection<PeerInfo>();

        private PeerInfo _SelectedInfo;
        public PeerInfo SelectedInfo
        {
            get { return _SelectedInfo; }
            set { Set(ref _SelectedInfo, value); }
        }

        private string _Address;
        public string Address
        {
            get { return _Address; }
            set { Set(ref _Address, value); }
        }

        private int _Port = 48336;
        public int Port
        {
            get { return _Port; }
            set { Set(ref _Port, value); }
        }

        public void OnAddPeer()
        {
            if(!CheckPeer())
            {
                GlobalMessageCenter.Instance.Send("MainWindow.MsgBox", "地址信息格式不对");
                return;
            }

            var peer = new PeerInfo()
            {
                Address = Address,
                Port = Port,
                Status = PeerStatus.Free,
                Range = PeerRange.Empty,
            };

            if (!CheckRepeat(peer))
            {
                GlobalMessageCenter.Instance.Send("MainWindow.MsgBox", "已存在该地址");
                return;
            }

            PeerInfoList.Add(peer);
        }

        private bool CheckPeer()
        {
            IPAddress addr;
            if (!IPAddress.TryParse(Address, out addr))
            {
                return false;
            }

            if (Port < 1024 || Port > 65535) return false;
            return true;
        }

        private bool CheckRepeat(PeerInfo peer)
        {
            return !PeerInfoList.Contains(peer, PeerInfoComparer.Default);
        }

        public void OnRemovePeer()
        {
            if(SelectedInfo != null)
            {
                PeerInfoList.Remove(SelectedInfo);
            }
        }

        public void OnConnectPeer()
        {

        }

        public void OnDisconnectPeer()
        {

        }
    }
}
