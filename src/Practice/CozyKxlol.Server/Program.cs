using CozyKxlol.Network.Msg;
using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CozyKxlol.Server
{
    class Program
    {
        private static uint _GameId = 1;
        public static uint GameId
        {
            get
            {
                return _GameId++;
            }
        }

        public static Random _RandomMaker = new Random();
        public static Random RandomMaker
        {
            get
            {
                return _RandomMaker;
            }
        }

        public static FixedBallManager FixedBallMgr = new FixedBallManager();

        static void Main(string[] args)
        {
            NetPeerConfiguration config = new NetPeerConfiguration("CozyKxlol");
            config.MaximumConnections = 10000;
            config.Port = 48360;

            NetServer server = new NetServer(config);
            server.Start();

            // 推送FixedBall的增加
            FixedBallMgr.FixedCreateMessage += (sender, msg) =>
            {
                Msg_AgarFixedBall r = new Msg_AgarFixedBall();
                r.Operat = Msg_AgarFixedBall.Add;
                r.BallId = msg.BallId;
                r.X = msg.Ball.X;
                r.Y = msg.Ball.Y;
                r.Color = msg.Ball.Color;

                NetOutgoingMessage om = server.CreateMessage();
                om.Write(r.Id);
                r.W(om);
                server.SendToAll(om, NetDeliveryMethod.Unreliable);
            };

            FixedBallMgr.Update();

            while (!Console.KeyAvailable || Console.ReadKey().Key != ConsoleKey.Escape)
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
                            Console.WriteLine(msg.ReadString());
                            break;
                        case NetIncomingMessageType.StatusChanged:
                            NetConnectionStatus status = (NetConnectionStatus)msg.ReadByte();
                            if (status == NetConnectionStatus.Connected)
                            {
                                Console.WriteLine(NetUtility.ToHexString(msg.SenderConnection.RemoteUniqueIdentifier) + " connected!");
                            }
                            break;
                        case NetIncomingMessageType.Data:
                            int id = msg.ReadInt32();
                            if (!ProcessPacket(server, id, msg))
                            {
                                DispatchPacket(server, id, msg);
                            }
                            break;
                    }
                }
                Thread.Sleep(1);
            }
            server.Shutdown("app exiting");
        }

        private static void DispatchPacket(NetServer server, int id, NetIncomingMessage msg)
        {
            List<NetConnection> all = server.Connections;
            all.Remove(msg.SenderConnection);
            if (all.Count > 0)
            {
                NetOutgoingMessage om = server.CreateMessage();
                om.Write(id);
                om.Write(msg);
                server.SendMessage(om, all, NetDeliveryMethod.Unreliable, 0);
            }
        }


        private static bool ProcessPacket(NetServer server, int id, NetIncomingMessage msg)
        {
            if (id == MsgId.AccountReg)
            {
                Msg_AccountReg r = new Msg_AccountReg();
                r.R(msg);
                Msg_AccountRegRsp rr = new Msg_AccountRegRsp();
                rr.suc = true;
                rr.detail = r.name + r.pass;
                NetOutgoingMessage om = server.CreateMessage();
                om.Write(rr.Id);
                rr.W(om);
                server.SendMessage(om, msg.SenderConnection, NetDeliveryMethod.Unreliable, 0);
                return true;
            }
            else if(id == MsgId.AgarLogin)
            {
                Msg_AgarLogin r = new Msg_AgarLogin();
                r.R(msg);

                // 返回客户端玩家坐标
                Msg_AgarLoginRsp rr = new Msg_AgarLoginRsp();
                rr.Uid = GameId;
                rr.X = RandomMaker.Next(800);
                rr.Y = RandomMaker.Next(600);
                NetOutgoingMessage om = server.CreateMessage();
                om.Write(rr.Id);
                rr.W(om);
                server.SendMessage(om, msg.SenderConnection, NetDeliveryMethod.Unreliable, 0);

                // 为新加入的玩家推送FixedBall
                var FixedList = FixedBallMgr.ToList();
                foreach(var obj in FixedList)
                {
                    Msg_AgarFixedBall rb = new Msg_AgarFixedBall();
                    rb.Operat = Msg_AgarFixedBall.Add;
                    rb.BallId = obj.Key;
                    rb.X = obj.Value.X;
                    rb.Y = obj.Value.Y;
                    rb.Color = obj.Value.Color;
                    
                    NetOutgoingMessage bom = server.CreateMessage();
                    bom.Write(rb.Id);
                    rb.W(bom);
                    server.SendMessage(bom, msg.SenderConnection, NetDeliveryMethod.Unreliable, 0);
                }
                
                return true;
            }
            return false;
        }
    }
}
