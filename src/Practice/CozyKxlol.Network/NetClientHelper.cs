using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.Network
{
    public class NetClientHelper
    {
        NetClient client;

        public NetClientHelper()
        {
            NetPeerConfiguration config = new NetPeerConfiguration("CozyKxlol");
            config.EnableMessageType(NetIncomingMessageType.DiscoveryResponse);
            client = new NetClient(config);
            client.Start();
        }

        public void Connect()
        {
            client.DiscoverLocalPeers(48360);
        }

        public void DisConnect()
        {
            client.Shutdown("bye");
        }

        public void Update()
        {
            NetIncomingMessage msg;
            while ((msg = client.ReadMessage()) != null)
            {
                switch (msg.MessageType)
                {
                    case NetIncomingMessageType.DiscoveryResponse:
                        client.Connect(msg.SenderEndPoint);
                        break;
                    case NetIncomingMessageType.Data:
                        break;
                }
            }
        }
    }
}
