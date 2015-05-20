using CozyKxlol.Network.Msg;
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

        public void SendMessage(MsgBase msg)
        {
            lock (msgQueuelocker)
            {
                NetOutgoingMessage om = client.CreateMessage();
                om.Write(msg.Id);
                msg.W(om);
                msgQueue.Enqueue(new NetClientMsg(om, NetDeliveryMethod.Unreliable));
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
            public MsgBase Msg { get; set; }

            public DataMessageArgs(MsgBase msg)
            {
                Msg = msg;
            }
        }
        public event EventHandler<DataMessageArgs> DataMessage;
        public void OnDataMessage(object sender, NetIncomingMessage im)
        {
            if (DataMessage != null)
            {
                int id = im.ReadInt32();
                if (id == MsgId.ChatToAll)
                {
                    MsgBase msg = new Msg_ChatToAll();
                    msg.R(im);
                    DataMessage(sender, new DataMessageArgs(msg));
                }
                else if (id == MsgId.AccountRegRsp)
                {
                    MsgBase msg = new Msg_AccountRegRsp();
                    msg.R(im);
                    DataMessage(sender, new DataMessageArgs(msg));
                }
                else if(id == MsgId.AgarLoginRsp)
                {
                    MsgBase msg = new Msg_AgarLoginRsp();
                    msg.R(im);
                    DataMessage(sender, new DataMessageArgs(msg));
                }
                else if(id == MsgId.AgarFixedBall)
                {
                    MsgBase msg = new Msg_AgarFixedBall();
                    msg.R(im);
                    DataMessage(sender, new DataMessageArgs(msg));
                }
                else if(id == MsgId.AgarPlayInfo)
                {
                    MsgBase msg = new Msg_AgarPlayInfo();
                    msg.R(im);
                    DataMessage(sender, new DataMessageArgs(msg));
                }
                else if(id == MsgId.AgarFixBallPack)
                {
                    MsgBase msg = new Msg_AgarFixBallPack();
                    msg.R(im);
                    DataMessage(sender, new DataMessageArgs(msg));
                }
            }
        }
    }
}
