using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lidgren.Network;
using NetworkHelper.Event;

namespace NetworkServer
{
    public class Server
    {
        public NetServer server { get; set; }
        private readonly Queue<NetServerMsg> MessageQueue = new Queue<NetServerMsg>();
        private readonly object MessageQueueLocker = new object();

        public bool IsRunning { get; set; }

        public Server(int MaxConnections ,int Port)
        {
            NetPeerConfiguration config = new NetPeerConfiguration("CozyAnywhere");
            config.MaximumConnections   = MaxConnections;
            config.Port                 = Port;
            server                      = new NetServer(config);
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
                        NetConnectionStatus status = (NetConnectionStatus)msg.ReadByte();
                        string reason = msg.ReadString();
                        OnStatusMessage(this, status, reason);
                        break;
                    case NetIncomingMessageType.Data:
                        OnDataMessage(this, msg);
                        break;
                    default:
                        break;
                }
            }
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
