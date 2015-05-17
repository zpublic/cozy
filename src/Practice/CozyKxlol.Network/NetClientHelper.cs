using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.Network
{
    public class NetClientHelper
    {
        private NetClient client;
        private class NetClientMsg
        {
            public NetOutgoingMessage Msg { get; set; }
            public NetDeliveryMethod Method { get; set; }

            public NetClientMsg(NetOutgoingMessage msg, NetDeliveryMethod method)
            {
                Msg = msg;
                Method = method;
            }
        }
        private readonly Queue<NetClientMsg> msgQueue = new Queue<NetClientMsg>();
        private readonly object msgQueuelocker = new object();

        public NetClientHelper()
        {
            NetPeerConfiguration config = new NetPeerConfiguration("CozyKxlol");
            config.EnableMessageType(NetIncomingMessageType.DiscoveryResponse);
            client = new NetClient(config);
            client.Start();
        }

        public void Connect(String ip, int port)
        {
            NetOutgoingMessage hail = client.CreateMessage("This is the kxlol hail message");
            client.Connect(ip, port, hail);
            //client.DiscoverLocalPeers(48360);
        }

        public void SendMessage(NetOutgoingMessage msg, NetDeliveryMethod method = NetDeliveryMethod.Unreliable)
        {
            lock(msgQueuelocker)
            {
                msgQueue.Enqueue(new NetClientMsg(msg, method));
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
            lock (msgQueuelocker)
            {
                if (msgQueue.Count > 0)
                {
                    msgArray = msgQueue.ToArray();
                    msgQueue.Clear();
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

        public class InternalMessageArgs : EventArgs
        {
            public String Msg { get; set; }

            public InternalMessageArgs(String msg)
            {
                Msg = msg;
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

        public class StatusMessageArgs : EventArgs
        {
            public ConnectionStatus Status { get; set; }
            public String Reason { get; set; }

            public StatusMessageArgs(NetConnectionStatus status, String reason)
            {
                Status = (ConnectionStatus)status;
                Reason = reason;
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

        public class DataMessageArgs : EventArgs
        {
            public NetIncomingMessage Msg { get; set; }

            public DataMessageArgs(NetIncomingMessage msg)
            {
                Msg = msg;
            }
        }
        public event EventHandler<DataMessageArgs> DataMessage;
        public void OnDataMessage(object sender, NetIncomingMessage msg)
        {
            if (DataMessage != null)
            {
                DataMessage(sender, new DataMessageArgs(msg));
            }
        }
    }
}
