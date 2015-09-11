using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;

namespace CozyClient.Core
{
    public class CozyClient
    {
        private NetClient InnerClient { get; set; }

        public CozyClient(string ClientName)
        {
            NetPeerConfiguration config = new NetPeerConfiguration(ClientName);
            config.EnableMessageType(NetIncomingMessageType.DiscoveryResponse);
            InnerClient = new NetClient(config);
            InnerClient.RegisterReceivedCallback(RecivePacket);
        }

        public void Connect(String ip, int port)
        {
            if (InnerClient.Status != NetPeerStatus.Running)
            {
                InnerClient.Start();
                NetOutgoingMessage hail = InnerClient.CreateMessage("This is the hail message");
                InnerClient.Connect(ip, port, hail);
            }
        }

        public void DisConnect()
        {
            if (InnerClient.Status == NetPeerStatus.Running)
            {
                InnerClient.Disconnect("Disconnect");
                InnerClient.Shutdown("Shutdown");
            }
        }

        public void SendMessage(NetOutgoingMessage msg)
        {
            if (InnerClient.Status == NetPeerStatus.Running)
            {
                InnerClient.SendMessage(msg, NetDeliveryMethod.Unreliable);
            }
        }

        private void RecivePacket(object peer)
        {
            NetIncomingMessage msg;
            while ((msg = InnerClient.ReadMessage()) != null)
            {
                switch (msg.MessageType)
                {
                    case NetIncomingMessageType.DebugMessage:
                    case NetIncomingMessageType.ErrorMessage:
                    case NetIncomingMessageType.WarningMessage:
                    case NetIncomingMessageType.VerboseDebugMessage:
                        break;
                    case NetIncomingMessageType.DiscoveryResponse:
                        InnerClient.Connect(msg.SenderEndPoint);
                        break;
                    case NetIncomingMessageType.StatusChanged:
                        OnStatusMessage(this, msg);
                        break;
                    case NetIncomingMessageType.Data:
                        OnDataMessage(this, msg);
                        break;
                }
            }
        }

        public event EventHandler<ClienEventArgs> StatusMessage;
        public event EventHandler<ClienEventArgs> DataMessage;

        private void OnDataMessage(CozyClient cozyClient, NetIncomingMessage msg)
        {
            if(DataMessage != null)
            {
                DataMessage(this, new ClienEventArgs(cozyClient.InnerClient, msg));
            }
        }

        private void OnStatusMessage(CozyClient cozyClient, NetIncomingMessage msg)
        {
            if(StatusMessage != null)
            {
                StatusMessage(this, new ClienEventArgs(cozyClient.InnerClient, msg));
            }
        }
    }
}
