using Lidgren.Network;
using NetworkHelper.Event;
using NetworkProtocol;
using System;
using System.Collections.Generic;

namespace NetworkClient
{
    public class Client
    {
        private NetClient client { get; set; }

        private readonly Queue<NetClientMsg> MessageQueue   = new Queue<NetClientMsg>();
        private readonly object MessageQueueLocker          = new object();

        public Client()
        {
            NetPeerConfiguration config = new NetPeerConfiguration("CozyAnywhere");
            config.EnableMessageType(NetIncomingMessageType.DiscoveryResponse);
            client                      = new NetClient(config);
            client.RegisterReceivedCallback(RecivePacket);
        }

        public void Connect(String ip, int port)
        {
            client.Start();
            NetOutgoingMessage hail = client.CreateMessage("This is the anywhere hail message");
            client.Connect(ip, port, hail);
        }

        public void SendMessage(IMessage msg)
        {
            lock (MessageQueueLocker)
            {
                NetOutgoingMessage om = client.CreateMessage();
                om.Write(msg.Id);
                msg.Write(om);
                MessageQueue.Enqueue(new NetClientMsg(om, NetDeliveryMethod.Unreliable));
            }
        }

        public void DisConnect()
        {
            client.Disconnect("Disconnect");
            client.Shutdown("Shutdown");
        }

        public void Update()
        {
            SendPacket();
        }

        private void SendPacket()
        {
            NetClientMsg[] msgArray = null;
            lock (MessageQueueLocker)
            {
                if (MessageQueue.Count > 0)
                {
                    msgArray = MessageQueue.ToArray();
                    MessageQueue.Clear();
                }
            }
            if (msgArray != null)
            {
                foreach (var msg in msgArray)
                {
                    client.SendMessage(msg.Msg, msg.Method);
                }
            }
        }

        private void RecivePacket(object peer)
        {
            NetIncomingMessage msg;
            while ((msg = client.ReadMessage()) != null)
            {
                switch (msg.MessageType)
                {
                    case NetIncomingMessageType.DebugMessage:
                    case NetIncomingMessageType.ErrorMessage:
                    case NetIncomingMessageType.WarningMessage:
                    case NetIncomingMessageType.VerboseDebugMessage:
                        string text = msg.ReadString();
                        OnInternalMessage(this, text);
                        break;

                    case NetIncomingMessageType.DiscoveryResponse:
                        client.Connect(msg.SenderEndPoint);
                        break;

                    case NetIncomingMessageType.StatusChanged:
                        NetConnectionStatus status = (NetConnectionStatus)msg.ReadByte();
                        string reason = msg.ReadString();
                        OnStatusMessage(this, status, reason);
                        break;

                    case NetIncomingMessageType.Data:
                        OnDataMessage(this, msg);
                        break;
                }
            }
        }

        public event EventHandler<InternalMessageArgs> InternalMessage;

        public void OnInternalMessage(object sender, String msg)
        {
            if (InternalMessage != null)
            {
                InternalMessage(sender, new InternalMessageArgs(msg));
            }
        }

        public event EventHandler<StatusMessageArgs> StatusMessage;

        public void OnStatusMessage(object sender, NetConnectionStatus status, String reason)
        {
            if (StatusMessage != null)
            {
                StatusMessage(sender, new StatusMessageArgs((NetworkHelper.NetConnectionStatus)status, reason));
            }
        }

        public event EventHandler<DataMessageArgs> DataMessage;

        public void OnDataMessage(object sender, NetIncomingMessage im)
        {
            if (DataMessage != null)
            {
                DataMessage(sender, new DataMessageArgs(im));
            }
        }
    }
}