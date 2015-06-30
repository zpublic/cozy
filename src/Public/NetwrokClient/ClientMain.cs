using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;
using NetwrokClient.Event;
using NetworkHelper.Messages;

namespace NetwrokClient
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
            client = new NetClient(config);
            client.Start();
        }

        public void Connect(String ip, int port)
        {
            NetOutgoingMessage hail = client.CreateMessage("This is the kxlol hail message");
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
            RecivePacket();
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

        private void RecivePacket()
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
                StatusMessage(sender, new StatusMessageArgs(status, reason));
            }
        }

        public event EventHandler<DataMessageArgs> DataMessage;
        public void OnDataMessage(object sender, NetIncomingMessage im)
        {
            if(DataMessage != null)
            {
                uint id = im.ReadUInt32();
                switch(id)
                {
                    default:
                        break;
                }
            }
        }
    }
}
