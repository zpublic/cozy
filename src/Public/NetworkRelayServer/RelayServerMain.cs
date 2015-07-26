using System;
using System.Collections.Generic;
using Lidgren.Network;
using NetworkHelper.Event;
using NetworkProtocol;
using System.Threading;

namespace NetworkRelayServer
{
    public class ServerRelay
    {
        public NetServer server { get; set; }

        private readonly Queue<NetRelayMsg> MessageQueue = new Queue<NetRelayMsg>();
        private readonly object MessageQueueLocker = new object();

        public NetConnection ServerConn { get; set; }

        public NetConnection ClientConn { get; set; }

        public bool IsRunning { get; set; }

        public int Port { get; set; }

        public ServerRelay(int MaxConnections, int Port)
        {
            NetPeerConfiguration config = new NetPeerConfiguration("CozyAnywhereRelay");
            config.MaximumConnections = MaxConnections;
            config.Port = Port;
            this.Port = Port;
            server = new NetServer(config);
        }

        public void Listen()
        {
            server.Start();
            IsRunning = true;
        }

        public void Shutdown()
        {
            IsRunning = false;
            server.Shutdown("shutdown");
        }

        public void RecivePacket()
        {
            NetIncomingMessage msg;
            while ((msg = server.ReadMessage()) != null)
            {
                switch (msg.MessageType)
                {
                    case NetIncomingMessageType.DiscoveryRequest:
                        server.SendDiscoveryResponse(null, msg.SenderEndPoint);
                        break;

                    case NetIncomingMessageType.VerboseDebugMessage:
                    case NetIncomingMessageType.DebugMessage:
                    case NetIncomingMessageType.WarningMessage:
                    case NetIncomingMessageType.ErrorMessage:
                        string text = msg.ReadString();
                        OnInternalMessage(this, text);
                        break;
                    case NetIncomingMessageType.StatusChanged:
                        OnStatusMessage(this, msg);
                        break;
                    case NetIncomingMessageType.Data:
                        OnDataMessage(this, msg);
                        break;
                    default:
                        break;
                }
            }
        }

        public void SendMessage(IMessage msg, NetConnection conn)
        {
            NetOutgoingMessage om = server.CreateMessage();
            om.Write(msg.Id);
            msg.Write(om);
            server.SendMessage(om, conn, NetDeliveryMethod.Unreliable);
        }

        public void EnterMainLoop()
        {
            while (IsRunning)
            {
                RecivePacket();
                Thread.Sleep(1);
            }
            server.Shutdown("exit");
        }

        public void SendMessageToServer(IMessage msg)
        {
            if (ServerConn != null)
            {
                NetOutgoingMessage om = server.CreateMessage();
                om.Write(msg.Id);
                msg.Write(om);
                server.SendMessage(om, ServerConn, NetDeliveryMethod.Unreliable);
            }
        }

        public void SendMessageToClient(IMessage msg)
        {
            if (ClientConn != null)
            {
                NetOutgoingMessage om = server.CreateMessage();
                om.Write(msg.Id);
                msg.Write(om);
                server.SendMessage(om, ClientConn, NetDeliveryMethod.Unreliable);
            }
        }

        private void ConnectToServer(string ip)
        {
            server.Connect(ip, this.Port);
        }

        public event EventHandler<InternalMessageArgs> InternalMessage;

        public void OnInternalMessage(object sender, string msg)
        {
            if (InternalMessage != null)
            {
                InternalMessage(sender, new InternalMessageArgs(msg));
            }
        }

        public event EventHandler<DataMessageArgs> StatusMessage;

        public void OnStatusMessage(object sender, NetIncomingMessage im)
        {
            if (StatusMessage != null)
            {
                StatusMessage(sender, new DataMessageArgs(im));
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
