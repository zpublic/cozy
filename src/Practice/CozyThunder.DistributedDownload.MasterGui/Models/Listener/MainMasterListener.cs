using CozyThunder.Botnet.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyThunder.Botnet.Common;
using CozyThunder.DistributedDownload.MasterGui.MessageCenter;

namespace CozyThunder.DistributedDownload.MasterGui.Models.Listener
{
    public class MainMasterListener : IMasterPeerListener
    {
        public void OnConnect(Peer peer)
        {
            GlobalMessageCenter.Instance.Send("PeerListener.Connect", peer);
        }

        public void OnDisConnect(Peer peer)
        {
            GlobalMessageCenter.Instance.Send("PeerListener.Disconnect", peer);
        }

        public void OnMessage(Peer peer, byte[] msg)
        {
            GlobalMessageCenter.Instance.Send("PeerListener.Message", peer, msg);
        }
    }
}
