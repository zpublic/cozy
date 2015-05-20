using CozyKxlol.Network.Msg;
using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CozyKxlol.Server.Manager;
using CozyKxlol.Server.Model;

namespace CozyKxlol.Server
{
    class Program
    {
        private static uint _GameId         = 1;
        public static uint GameId
        {
            get
            {
                return _GameId++;
            }
        }

        public static Random _RandomMaker   = new Random();
        public static Random RandomMaker
        {
            get
            {
                return _RandomMaker;
            }
        }

        public static FixedBallManager FixedBallMgr     = new FixedBallManager();
        public static PlayerBallManager PlayerBallMgr   = new PlayerBallManager();

        static void Main(string[] args)
        {
            NetPeerConfiguration config     = new NetPeerConfiguration("CozyKxlol");
            config.MaximumConnections       = 10000;
            config.Port                     = 48360;

            NetServer server                = new NetServer(config);
            server.Start();

            // 推送FixedBall的增加
            FixedBallMgr.FixedCreateMessage += (sender, msg) =>
            {
                Msg_AgarFixedBall r     = new Msg_AgarFixedBall();
                r.Operat                = Msg_AgarFixedBall.Add;
                r.BallId                = msg.BallId;
                r.X                     = msg.Ball.X;
                r.Y                     = msg.Ball.Y;
                r.Color                 = msg.Ball.Color;

                NetOutgoingMessage om   = server.CreateMessage();
                om.Write(r.Id);
                r.W(om);
                server.SendToAll(om, NetDeliveryMethod.Unreliable);
            };

            // 推送FixedBall的移除
            FixedBallMgr.FixedRemoveMessage += (sender, msg) =>
            {
                Msg_AgarFixedBall r     = new Msg_AgarFixedBall();
                r.Operat                = Msg_AgarFixedBall.Remove;
                r.BallId                = msg.BallId;

                NetOutgoingMessage om   = server.CreateMessage();
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

        private static void SendToAllExceptOne(NetServer server, int id, NetOutgoingMessage msg, NetConnection except)
        {
            List<NetConnection> all = server.Connections;
            all.Remove(except);
            if(all.Count > 0)
            {
                server.SendMessage(msg, all, NetDeliveryMethod.Unreliable, 0);
            }
        }

        private static bool ProcessPacket(NetServer server, int id, NetIncomingMessage msg)
        {
            if (id == MsgId.AccountReg)
            {
                Msg_AccountReg r        = new Msg_AccountReg();
                r.R(msg);
                Msg_AccountRegRsp rr    = new Msg_AccountRegRsp();
                rr.suc                  = true;
                rr.detail               = r.name + r.pass;
                NetOutgoingMessage om   = server.CreateMessage();
                om.Write(rr.Id);
                rr.W(om);
                server.SendMessage(om, msg.SenderConnection, NetDeliveryMethod.Unreliable, 0);
                return true;
            }
            else if(id == MsgId.AgarLogin)
            {
                Msg_AgarLogin r         = new Msg_AgarLogin();
                r.R(msg);

                // 返回客户端玩家坐标
                Msg_AgarLoginRsp rr     = new Msg_AgarLoginRsp();
                uint uid                = GameId;
                rr.Uid                  = uid;
                rr.X                    = RandomMaker.Next(800);
                rr.Y                    = RandomMaker.Next(600);
                rr.Radius               = PlayerBall.DefaultPlayerRadius;
                rr.Color                = CustomColors.Colors[RandomMaker.Next(CustomColors.Colors.Length)];

                NetOutgoingMessage om   = server.CreateMessage();
                om.Write(rr.Id);
                rr.W(om);
                server.SendMessage(om, msg.SenderConnection, NetDeliveryMethod.Unreliable, 0);

                // 推送之前加入的玩家数据到新玩家
                var PlayerList          = PlayerBallMgr.ToList();
                foreach(var obj in PlayerList)
                {
                    Msg_AgarPlayInfo rp = new Msg_AgarPlayInfo();
                    rp.Operat           = Msg_AgarPlayInfo.Add;
                    rp.PlayerId         = obj.Key;
                    rp.X                = obj.Value.X;
                    rp.Y                = obj.Value.Y;
                    rp.Radius           = obj.Value.Radius;
                    rp.Color            = obj.Value.Color;

                    NetOutgoingMessage pom = server.CreateMessage();
                    pom.Write(rp.Id);
                    rp.W(pom);
                    server.SendMessage(pom, msg.SenderConnection, NetDeliveryMethod.Unreliable, 0);
                }

                // 推送新玩家的数据到之前加入的玩家
                Msg_AgarPlayInfo lp     = new Msg_AgarPlayInfo();
                lp.Operat               = Msg_AgarPlayInfo.Add;
                lp.PlayerId             = uid;
                lp.X                    = rr.X;
                lp.Y                    = rr.Y;
                lp.Radius               = rr.Radius;
                lp.Color                = rr.Color;

                NetOutgoingMessage lom = server.CreateMessage();
                lom.Write(lp.Id);
                lp.W(lom);
                SendToAllExceptOne(server, id, lom, msg.SenderConnection);

                // Player加入Manager
                PlayerBall player       = new PlayerBall();
                player.X                = rr.X;
                player.Y                = rr.Y;
                player.Radius           = rr.Radius;
                player.Color            = rr.Color;
                PlayerBallMgr.Add(uid, player);

                

                // 为新加入的玩家推送FixedBall
                var FixedList           = FixedBallMgr.ToList();
                foreach(var obj in FixedList)
                {
                    Msg_AgarFixedBall rb    = new Msg_AgarFixedBall();
                    rb.Operat               = Msg_AgarFixedBall.Add;
                    rb.BallId               = obj.Key;
                    rb.X                    = obj.Value.X;
                    rb.Y                    = obj.Value.Y;
                    rb.Color                = obj.Value.Color;
                    
                    NetOutgoingMessage bom  = server.CreateMessage();
                    bom.Write(rb.Id);
                    rb.W(bom);
                    server.SendMessage(bom, msg.SenderConnection, NetDeliveryMethod.Unreliable, 0);
                }
                return true;
            }
            else if(id == MsgId.AgarPlayInfo)
            {
                Msg_AgarPlayInfo r          = new Msg_AgarPlayInfo();
                r.R(msg);
                uint uid                    = r.PlayerId;
                if(r.Operat == Msg_AgarPlayInfo.Changed)
                {
                    PlayerBall newBall      = new PlayerBall();
                    newBall.X               = r.X;
                    newBall.Y               = r.Y;
                    newBall.Radius          = r.Radius;
                    newBall.Color           = r.Color;
                    PlayerBallMgr.Change(uid, newBall);
                    Update(uid, newBall);
                }
                else if(r.Operat == Msg_AgarPlayInfo.Remove)
                {
                    PlayerBallMgr.Remove(uid);
                }

                NetOutgoingMessage com = server.CreateMessage();
                com.Write(r.Id);
                r.W(com);
                SendToAllExceptOne(server, id, com, msg.SenderConnection);
                return true;
            }
            return false;
        }

        public static bool CanEat(PlayerBall player, FixedBall ball)
        {
            const float DefaultBallRadius = 5.0f;

            float X_Distance    = player.X - ball.X;
            float Y_Distance    = player.Y - ball.Y;
            float Distance      = (float)Math.Sqrt(X_Distance * X_Distance + Y_Distance * Y_Distance);
            return player.Radius > (Distance + DefaultBallRadius);
        }

        public static void Update(uint id, PlayerBall ball)
        {
            bool FoodRemoveFlag = false;
            foreach(var obj in FixedBallMgr.ToList())
            {
                if(CanEat(ball, obj.Value))
                {
                    FoodRemoveFlag = true;
                    FixedBallMgr.Remove(obj.Key);
                }
            }
            if(FoodRemoveFlag)
            {
                FixedBallMgr.Update();
            }
        }
    }
}
