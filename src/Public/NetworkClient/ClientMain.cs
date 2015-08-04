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

        private readonly Dictionary<long, NetOutgoingMessage> PacketMessageDictionary
    = new Dictionary<long, NetOutgoingMessage>();
        private readonly object PacketMessageDictionaryLocker = new object();

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
                if (om.LengthBytes > 548)
                {
                    var id = client.UniqueIdentifier;
                    lock (PacketMessageDictionaryLocker)
                    {
                        PacketMessageDictionary[id] = om;
                    }
                    var packetMsg = new SendPacketMessage()
                    {
                        UniqueIdentifier = id,
                        TargetSize = (om.LengthBytes + 547) / 548,
                    };
                    SendMessage(packetMsg);
                }
                else
                {
                    MessageQueue.Enqueue(new NetClientMsg(om, NetDeliveryMethod.Unreliable));
                }
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
                        OnStatusMessage(this, status, reason, msg.SenderConnection);
                        break;
                    case NetIncomingMessageType.Data:
                        OnDataMessage(this, msg);
                        break;
                }
            }
        }

        private bool DefMessageProc(uint id, NetBuffer im)
        {
            bool result = false;
            switch (id)
            {
                case DefaultMessageId.SendPacketMessageRecv:
                    OnSendPacketMessageRecv(im);
                    result = true;
                    break;
                default:
                    break;
            }
            return result;
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

        public void OnStatusMessage(object sender, NetConnectionStatus status, String reason, NetConnection conn)
        {
            if (StatusMessage != null)
            {
                StatusMessage(sender, new StatusMessageArgs((NetworkHelper.NetConnectionStatus)status, reason, conn));
            }
        }

        public event EventHandler<DataMessageArgs> DataMessage;

        public void OnDataMessage(object sender, NetIncomingMessage im)
        {
            if (DataMessage != null)
            {
                uint id = im.ReadUInt32();
                if (!DefMessageProc(id, im))
                {
                    DataMessage(sender, new DataMessageArgs(im, id, im.SenderConnection));
                }
            }
        }

        private void OnSendPacketMessageRecv(NetBuffer msg)
        {
            var recvMsg = new SendPacketMessageRecv();
            recvMsg.Read(msg);
            var UniqueIdentifier = recvMsg.UniqueIdentifier;
            var MessageRacketId = recvMsg.MessagePacketId;
            if (PacketMessageDictionary.ContainsKey(UniqueIdentifier))
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
                if (packetMsg != null)
                {
                    int n = (packetMsg.LengthBytes + 547) / 548;
                    for (int i = 0; i < n; ++i)
                    {
                        int l = packetMsg.LengthBytes - i * 548;
                        int length = l < 548 ? l : 548;

                        if (length > 0)
                        {
                            NetOutgoingMessage om = client.CreateMessage();
                            var pm = new PacketMessage()
                            {
                                Number = i,
                                MessagePacketId = MessageRacketId,
                                Bytes = new byte[length],
                            };

                            Array.Copy(packetMsg.Data, i * 548, pm.Bytes, 0, length);
                            om.Write(pm.Id);
                            pm.Write(om);
                            client.SendMessage(om, NetDeliveryMethod.Unreliable);
                        }
                    }
                }
            }
        }
    }
}