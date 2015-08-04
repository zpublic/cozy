using Lidgren.Network;
using NetworkHelper.Event;
using NetworkProtocol;
using System;
using System.Collections.Generic;
using System.Threading;

namespace NetworkServer
{
    public class Server
    {
        public NetServer server { get; set; }

        private readonly Queue<NetServerMsg> MessageQueue   = new Queue<NetServerMsg>();
        private readonly object MessageQueueLocker          = new object();

        private readonly Dictionary<long, NetOutgoingMessage> PacketMessageDictionary 
            = new Dictionary<long, NetOutgoingMessage>();
        private readonly object PacketMessageDictionaryLocker = new object();

        private readonly Dictionary<long, PacketNetBuffer> SendPacketMessageRecvDictionary
            = new Dictionary<long, PacketNetBuffer>();
        private readonly object SendPacketMessageRecvDictionaryLocker = new object();

        private readonly Queue<NetBuffer> AlreadyMessageQueue = new Queue<NetBuffer>();
        private readonly object AlreadyMessageQueueLocker = new object();

        public bool IsRunning { get; set; }

        private long _MessageRecvId = 0;
        public long MessageRecvId
        {
            get
            {
                return _MessageRecvId;
            }
        }

        public Server(int MaxConnections, int Port)
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

        public void Connect(string ip, int port)
        {
            server.Connect(ip, port);
        }

        public void Shutdown()
        {
            IsRunning = false;
            server.Shutdown("shutdown");
        }

        public void SendMessage(IMessage msg, NetConnection conn)
        {
            NetOutgoingMessage om = server.CreateMessage();
            om.Write(msg.Id);
            msg.Write(om);
            if (om.LengthBytes > 65535)
            {
                var id = server.UniqueIdentifier;
                lock(PacketMessageDictionaryLocker)
                {
                    PacketMessageDictionary[id] = om;
                }
                var packetMsg = new SendPacketMessage()
                {
                    UniqueIdentifier = id,
                    TargetSize = (om.LengthBytes + 59999) / 60000,
                };
                SendMessage(packetMsg, conn);
            }
            else
            {
                server.SendMessage(om, conn, NetDeliveryMethod.Unreliable);
            }
        }

        public void SendMessage(IMessage msg)
        {
            NetOutgoingMessage om = server.CreateMessage();
            om.Write(msg.Id);
            msg.Write(om);
            server.SendToAll(om, NetDeliveryMethod.Unreliable);
        }

        public static void SendMessageExceptOne(NetServer server, IMessage msg, NetConnection except)
        {
            NetOutgoingMessage om = server.CreateMessage();
            om.Write(msg.Id);
            msg.Write(om);

            List<NetConnection> all = server.Connections;
            if (all.Contains(except))
            {
                all.Remove(except);
            }
            if (all.Count > 0)
            {
                server.SendMessage(om, all, NetDeliveryMethod.Unreliable, 0);
            }
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
                        var status = (NetworkHelper.NetConnectionStatus)msg.ReadByte();
                        string reason = msg.ReadString();
                        OnStatusMessage(this, status, reason, msg.SenderConnection);
                        break;
                    case NetIncomingMessageType.Data:
                        OnDataMessage(this, msg, msg.SenderConnection);
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
                RecvAlreadyPacketMessage();
                Thread.Sleep(1);
            }
            server.Shutdown("exit");
        }


        public void RecvAlreadyPacketMessage()
        {
            if(AlreadyMessageQueue.Count > 0)
            {
                lock (AlreadyMessageQueueLocker)
                {
                    while(AlreadyMessageQueue.Count > 0)
                    {
                        var msg = AlreadyMessageQueue.Dequeue();

                        //TODO send msg to OnDataMessage;
                    }
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

        public void OnStatusMessage(object sender, NetworkHelper.NetConnectionStatus status, String reason, NetConnection conn)
        {
            if (StatusMessage != null)
            {
                StatusMessage(sender, new StatusMessageArgs(status, reason, conn));
            }
        }

        public event EventHandler<DataMessageArgs> DataMessage;

        public void OnDataMessage(object sender, NetBuffer im, NetConnection conn)
        {
            if (DataMessage != null)
            {
                uint id = im.ReadUInt32();
                if(!DefMessageProc(id, im, conn))
                {
                    DataMessage(sender, new DataMessageArgs(im, id, conn));
                }
            }
        }


        private bool DefMessageProc(uint id, NetBuffer im, NetConnection conn)
        {
            bool result = false;
            switch(id)
            {
                case DefaultMessageId.SendPacketMessage:
                    OnSendPacketMessage(im, conn);
                    result = true;
                    break;
                case DefaultMessageId.SendPacketMessageRecv:
                    OnSendPacketMessageRecv(im, conn);
                    result = true;
                    break;

                case DefaultMessageId.PacketMessage:
                    OnPacketMessage(im);
                    result = true;
                    break;
                default:
                    break;
            }
            return result;
        }

        private void OnPacketMessage(NetBuffer msg)
        {
            var packetMsg = new PacketMessage();
            packetMsg.Read(msg);

            long id = packetMsg.MessagePacketId;
            if(SendPacketMessageRecvDictionary.ContainsKey(id))
            {
                PacketNetBuffer packet = null;
                lock (SendPacketMessageRecvDictionaryLocker)
                {
                    if(SendPacketMessageRecvDictionary.ContainsKey(id))
                    {
                        packet = SendPacketMessageRecvDictionary[id];
                    }
                }
                if(packet != null)
                {
                    packet.Add(packetMsg);
                    if (packet.IsComplete)
                    {
                        var ms = packet.ToBuffer();
                        lock(SendPacketMessageRecvDictionaryLocker)
                        {
                            SendPacketMessageRecvDictionary.Remove(id);
                        }
                        lock(AlreadyMessageQueueLocker)
                        {
                            AlreadyMessageQueue.Enqueue(ms);
                        }
                    }
                }
            }
        }

        private void OnSendPacketMessage(NetBuffer msg, NetConnection conn)
        {
            long id         = MessageRecvId;
            var packetMsg   = new SendPacketMessage();
            packetMsg.Read(msg);


            lock(SendPacketMessageRecvDictionaryLocker)
            {
                SendPacketMessageRecvDictionary[id] = new PacketNetBuffer(packetMsg.TargetSize, conn);
            }

            var rspMsg = new SendPacketMessageRecv()
            {
                UniqueIdentifier    = packetMsg.UniqueIdentifier,
                MessagePacketId     = id,
            };
            SendMessage(rspMsg, conn);
        }

        private void OnSendPacketMessageRecv(NetBuffer msg, NetConnection conn)
        {
            var recvMsg = new SendPacketMessageRecv();
            recvMsg.Read(msg);
            var UniqueIdentifier    = recvMsg.UniqueIdentifier;
            var MessageRacketId     = recvMsg.MessagePacketId;
            if(PacketMessageDictionary.ContainsKey(UniqueIdentifier))
            {
                NetOutgoingMessage packetMsg = null;
                lock (PacketMessageDictionaryLocker)
                {
                    if (PacketMessageDictionary.ContainsKey(UniqueIdentifier))
                    {
                        packetMsg = PacketMessageDictionary[UniqueIdentifier];
                        PacketMessageDictionary.Remove(UniqueIdentifier);
                    }
                }
                if(packetMsg != null)
                {
                    lock(SendPacketMessageRecvDictionaryLocker)
                    {
                        int n = (packetMsg.LengthBytes + 59999) / 60000;
                        for(int i= 0; i < n; ++i)
                        {
                            int l = packetMsg.LengthBytes - i * 60000;
                            int length = l < 60000 ? l : 60000;

                            if (length > 0)
                            {
                                NetOutgoingMessage om = server.CreateMessage();
                                var pm = new PacketMessage()
                                {
                                    Number = i,
                                    MessagePacketId = MessageRacketId,
                                    Bytes = new byte[length],
                                };

                                Array.Copy(packetMsg.Data, pm.Bytes, length);
                                om.Write(pm.Id);
                                pm.Write(om);
                                server.SendMessage(om, conn, NetDeliveryMethod.Unreliable);
                            }
                        }
                    }
                }
            }
        }
    }
}