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
using CozyThunder.DistributedDownload.MasterGui.Common;

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
                SetPeerInfoStatus(peer, PeerStatus.Free);
            }
        }

        private void OnDisconnectMessage(object arg)
        {
            var peer = arg as Peer;
            if (peer != null)
            {
                SetPeerInfoStatus(peer, PeerStatus.Unknow);
            }
        }

        private Dictionary<string, MessageBuffer> sbuff_ = new Dictionary<string, MessageBuffer>();

        private void OnMessageMessage(object arg1, object arg2)
        {
            var peer = arg1 as Peer;
            var data = arg2 as byte[];
            if (peer != null && data != null)
            {
                sbuff_[peer.EndPoint.ToString()].Append(data, data.Length);
            }
            PacketTest t = new PacketTest(sbuff_[peer.EndPoint.ToString()].RawData, 0);
            if (t.PacketLength > sbuff_[peer.EndPoint.ToString()].Length)
            {
                //拆包
            }
            else if (t.PacketLength < sbuff_[peer.EndPoint.ToString()].Length)
            {
                //粘包
            }
            else if (t.PacketLength == sbuff_[peer.EndPoint.ToString()].Length)
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
