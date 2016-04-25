using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyThunder.Botnet.Master;
using CozyThunder.Botnet.Common;
using CozyThunder.DistributedDownload.MasterGui.Models;
using CozyThunder.DistributedDownload.MasterGui.MessageCenter;
using CozyThunder.Protocol;

namespace CozyThunder.DistributedDownload.MasterGui.ViewModels
{
    public partial class MainWindowViewModel
    {
        private MasterPeer master { get; set; } = new MasterPeer();

        private void RegistMessage()
        {
            GlobalMessageCenter.Instance.RegistMessage("App.Clear", OnClear);
            GlobalMessageCenter.Instance.RegistMessage("PeerListener.Connect", OnConnectMessage);
            GlobalMessageCenter.Instance.RegistMessage("PeerListener.Disconnect", OnDisconnectMessage);
            GlobalMessageCenter.Instance.RegistMessage("PeerListener.Message", OnMessageMessage);
        }

        private void UnregistMessage()
        {
            GlobalMessageCenter.Instance.UnregistMessage("PeerListener.Message", OnMessageMessage);
            GlobalMessageCenter.Instance.UnregistMessage("PeerListener.Disconnect", OnDisconnectMessage);
            GlobalMessageCenter.Instance.UnregistMessage("PeerListener.Connect", OnConnectMessage);
            GlobalMessageCenter.Instance.UnregistMessage("App.Clear", OnClear);
        }

        private void OnConnectMessage(object arg)
        {
            var peer = arg as Peer;
            if (peer != null)
            {
                var addr = peer.EndPoint.Address.ToString();
                var port = peer.EndPoint.Port;
                var info = PeerInfoList.Where(x => x.Address == addr && x.Port == port).First();
                if (info != null)
                {
                    info.Status = PeerStatus.Free;
                }
            }
        }

        private void OnDisconnectMessage(object arg)
        {
            var peer = arg as Peer;
            if (peer != null)
            {
                var addr = peer.EndPoint.Address.ToString();
                var port = peer.EndPoint.Port;
                var info = PeerInfoList.Where(x => x.Address == addr && x.Port == port).First();
                if (info != null)
                {
                    info.Status = PeerStatus.Unknow;
                }
            }
        }

        private Dictionary<string, byte[]> sbuff_ = new Dictionary<string, byte[]>();
        private Dictionary<string, int> sbufflen_ = new Dictionary<string, int>();

        private void OnMessageMessage(object arg1, object arg2)
        {
            var peer = arg1 as Peer;
            var data = arg2 as byte[];
            if (peer != null && data != null)
            {
                Array.Copy(data, 0, sbuff_[peer.EndPoint.ToString()],
                    sbufflen_[peer.EndPoint.ToString()], data.Length);
                sbufflen_[peer.EndPoint.ToString()] += data.Length;
            }
            PacketTest t = new PacketTest(sbuff_[peer.EndPoint.ToString()], 0);
            if (t.PacketLength > sbufflen_[peer.EndPoint.ToString()])
            {
                //拆包
            }
            else if (t.PacketLength < sbufflen_[peer.EndPoint.ToString()])
            {
                //粘包
            }
            else if (t.PacketLength == sbufflen_[peer.EndPoint.ToString()])
            {
                //完整包
                MessageDispatch(peer, data, t.PacketId);
            }
        }

        public void OnEnableDistributedCommand()
        {

        }

        public void OnDisableDistributedCommand()
        {

        }
    }
}
