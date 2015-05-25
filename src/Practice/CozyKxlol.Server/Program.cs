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
using CozyKxlol.Server.Model.Impl;

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

        public static FixedBallManager FixedBallMgr                 = new FixedBallManager();
        public static PlayerBallManager PlayerBallMgr               = new PlayerBallManager();
        public static Dictionary<NetConnection, uint> ConnectionMgr = new Dictionary<NetConnection, uint>();
        public static MarkManager MarkMgr                           = new MarkManager();
        public const int GameWidth  = 800;
        public const int GameHeight = 610;
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
                r.Radius                = msg.Ball.Radius;
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

            // 用户断开链接
            PlayerBallMgr.PlayerExitMessage += (sender, msg) =>
            {
                var removeMsg       = new Msg_AgarPlayInfo();
                removeMsg.Operat    = Msg_AgarPlayInfo.Remove;
                removeMsg.UserId    = msg.UserId;

                MarkMgr.Remove(msg.UserId);

                NetOutgoingMessage om = server.CreateMessage();
                om.Write(removeMsg.Id);
                removeMsg.W(om);
                server.SendToAll(om, NetDeliveryMethod.Unreliable);
            };

            PlayerBallMgr.PlayerDeadMessage += (sender, msg) =>
            {
                var conn        = ConnectionMgr.First(obj => obj.Value == msg.UserId).Key;

                MarkMgr.Remove(msg.UserId);

                // 为自己发送死亡信息
                var selfMsg     = new Msg_AgarSelf();
                selfMsg.Operat  = Msg_AgarSelf.Dead;

                NetOutgoingMessage som = server.CreateMessage();
                som.Write(selfMsg.Id);
                selfMsg.W(som);
                server.SendMessage(som, conn, NetDeliveryMethod.Unreliable, 0);

                // 为其他玩家推送玩家死亡信息
                var pubMsg      = new Msg_AgarPlayInfo();
                pubMsg.Operat   = Msg_AgarPlayInfo.Remove;
                pubMsg.UserId   = msg.UserId;

                NetOutgoingMessage pom = server.CreateMessage();
                pom.Write(pubMsg.Id);
                pubMsg.W(pom);
                SendToAllExceptOne(server, pubMsg.Id, pom, conn);
            };

            MarkMgr.MarkChangedMessage += (sender, msg) =>
            {
                var markList =
                    from m
                        in msg.TopMarkList
                        orderby m.Value descending
                    select m;

                var markMsg         = new Msg_AgarMarkListPack();
                var sendList        = markList.Take(5).ToList();
                markMsg.MarkList    = sendList;

                NetOutgoingMessage mom = server.CreateMessage();
                mom.Write(markMsg.Id);
                markMsg.W(mom);
                server.SendToAll(mom, NetDeliveryMethod.Unreliable);

                Console.WriteLine("-----------------------------------------------------------");
                foreach (var obj in sendList)
                {
                    string name = PlayerBallMgr.Get(obj.Key).Name;
                    Console.WriteLine(name + " " + obj.Value);
                }
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
                                Console.WriteLine(server.Connections.Count);
                                ConnectionMgr[msg.SenderConnection] = 0;
                            }
                            else if(status == NetConnectionStatus.Disconnected)
                            {
                                Console.WriteLine(NetUtility.ToHexString(msg.SenderConnection.RemoteUniqueIdentifier) + " disconnect!");
                                uint removeId = ConnectionMgr[msg.SenderConnection];
                                PlayerBallMgr.Remove(removeId);
                                ConnectionMgr.Remove(msg.SenderConnection);
                                Console.WriteLine(server.Connections.Count);
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
                uint uid                = GameId;

                Msg_AgarLogin r         = new Msg_AgarLogin();
                r.R(msg);

                Msg_AgarLoginRsp rr     = new Msg_AgarLoginRsp();
                rr.Uid = uid;
                rr.Width = GameWidth;
                rr.Height = GameHeight;

                NetOutgoingMessage rom  = server.CreateMessage();
                rom.Write(rr.Id);
                rr.W(rom);
                server.SendMessage(rom, msg.SenderConnection, NetDeliveryMethod.Unreliable, 0);

                // 推送之前加入的玩家数据到新玩家
                var PlayerList          = PlayerBallMgr.ToList();
                var PlayerPackList =
                    from p
                    in PlayerList
                    select Tuple.Create<uint, float, float, int, uint, string>
                    (p.Key, p.Value.X, p.Value.Y, p.Value.Radius, p.Value.Color, p.Value.Name);

                var PlayerPack          = new Msg_AgarPlayInfoPack();
                PlayerPack.PLayerList   = PlayerPackList.ToList();
                NetOutgoingMessage pom  = server.CreateMessage();
                pom.Write(PlayerPack.Id);
                PlayerPack.W(pom);
                server.SendMessage(pom, msg.SenderConnection, NetDeliveryMethod.Unreliable, 0);

                // 为新加入的玩家推送FixedBall
                var FixedList           = FixedBallMgr.ToList();
                var FixedPackList = 
                    from f 
                    in FixedList 
                    select Tuple.Create<uint, float, float, int, uint>(f.Key, f.Value.X, f.Value.Y,
                    f.Value.Radius, f.Value.Color);

                var FixedPack           = new Msg_AgarFixBallPack();
                FixedPack.FixedList     = FixedPackList.ToList();
                NetOutgoingMessage bom  = server.CreateMessage();
                bom.Write(FixedPack.Id);
                FixedPack.W(bom);
                server.SendMessage(bom, msg.SenderConnection, NetDeliveryMethod.Unreliable, 0);

                return true;
            }
            else if(id == MsgId.AgarPlayInfo)
            {
                Msg_AgarPlayInfo r          = new Msg_AgarPlayInfo();
                r.R(msg);
                uint uid                    = r.UserId;
                if(r.Operat == Msg_AgarPlayInfo.Changed)
                {
                    PlayerBall newBall      = PlayerBallMgr.Get(uid);
                    uint tag                = r.Tag;
                    if (GameMessageHelper.Is_Changed(tag, GameMessageHelper.POSITION_TAG))
                    {
                        newBall.X       = r.X;
                        newBall.Y       = r.Y;
                    }
                    if (GameMessageHelper.Is_Changed(tag, GameMessageHelper.RADIUS_TAG))
                    {
                        newBall.Radius  = r.Radius;
                    }
                    if (GameMessageHelper.Is_Changed(tag, GameMessageHelper.COLOR_TAG))
                    {
                        newBall.Color   = r.Color;
                    }
                    if (GameMessageHelper.Is_Changed(tag, GameMessageHelper.NAME_TAG))
                    {
                        newBall.Name    = r.Name;
                    }

                    bool RaduisChanged = false;
                    if (UpdateFood(uid, ref newBall))
                    {
                        r.Tag               = r.Tag | GameMessageHelper.RADIUS_TAG;
                        r.Radius            = newBall.Radius;
                        var self            = new Msg_AgarSelf();
                        self.Operat         = Msg_AgarSelf.GroupUp;
                        self.Radius         = newBall.Radius;
                        RaduisChanged       = true;

                        NetOutgoingMessage som = server.CreateMessage();
                        som.Write(self.Id);
                        self.W(som);
                        server.SendMessage(som, msg.SenderConnection, NetDeliveryMethod.Unreliable, 0);
                    }
                    if (UpdatePlayer(uid, ref newBall))
                    {
                        r.Tag               = r.Tag | GameMessageHelper.RADIUS_TAG;
                        r.Radius            = newBall.Radius;
                        var self            = new Msg_AgarSelf();
                        self.Operat         = Msg_AgarSelf.GroupUp;
                        self.Radius         = newBall.Radius;
                        RaduisChanged       = true;

                        NetOutgoingMessage som = server.CreateMessage();
                        som.Write(self.Id);
                        self.W(som);
                        server.SendMessage(som, msg.SenderConnection, NetDeliveryMethod.Unreliable, 0);
                    }
                    PlayerBallMgr.Change(uid, newBall);
                    if(RaduisChanged)
                    {
                        MarkMgr.Update(uid, newBall.Radius);
                    }
                }

                NetOutgoingMessage com = server.CreateMessage();
                com.Write(r.Id);
                r.W(com);
                SendToAllExceptOne(server, id, com, msg.SenderConnection);
                return true;
            }
            else if(id == MsgId.AgarBorn)
            {
                var r = new Msg_AgarBorn();
                r.R(msg);

                uint uid    = r.UserId;
                string name = r.Name;

                int x       = RandomMaker.Next(GameWidth);
                int y       = RandomMaker.Next(GameHeight);
                int radius  = PlayerBall.DefaultPlayerRadius;
                uint c      = CustomColors.RandomColor;

                // 添加Player到Manager
                PlayerBall player   = new PlayerBall();
                player.X            = x;
                player.Y            = y;
                player.Radius       = radius;
                player.Color        = c;
                player.Name         = name;
                PlayerBallMgr.Add(uid, player);

                MarkMgr.Update(uid, radius);

                // 更新链接对应的ID
                ConnectionMgr[msg.SenderConnection] = uid;

                // 向自身发送出生位置等信息
                var selfMsg         = new Msg_AgarSelf();
                selfMsg.Operat      = Msg_AgarSelf.Born;
                selfMsg.X           = x;
                selfMsg.Y           = y;
                selfMsg.Radius      = radius;
                selfMsg.Color       = c;

                NetOutgoingMessage som = server.CreateMessage();
                som.Write(selfMsg.Id);
                selfMsg.W(som);
                server.SendMessage(som, msg.SenderConnection, NetDeliveryMethod.Unreliable, 0);

                // 向之前加入的玩家推送新用户出生信息
                var oMsg            = new Msg_AgarPlayInfo();
                oMsg.Operat         = Msg_AgarPlayInfo.Add;
                oMsg.UserId         = uid;
                oMsg.Tag            = GameMessageHelper.ALL_TAG;
                oMsg.X              = x;
                oMsg.Y              = y;
                oMsg.Radius         = radius;
                oMsg.Color          = c;
                oMsg.Name           = name;

                NetOutgoingMessage oom = server.CreateMessage();
                oom.Write(oMsg.Id);
                oMsg.W(oom);
                SendToAllExceptOne(server, oMsg.Id, oom, msg.SenderConnection);
                return true;
            }
            return false;
        }

        public static bool CanEat(ICircle player, ICircle ball)
        {
            float X_Distance    = player.X - ball.X;
            float Y_Distance    = player.Y - ball.Y;
            float Distance      = (float)Math.Sqrt(X_Distance * X_Distance + Y_Distance * Y_Distance);
            return player.Radius > (Distance + ball.Radius);
        }

        public static bool UpdateFood(uint id, ref PlayerBall ball)
        {
            bool FoodRemoveFlag = false;
            foreach(var obj in FixedBallMgr.ToList())
            {
                if(CanEat(ball, obj.Value))
                {
                    FoodRemoveFlag = true;
                    if(ball.Radius < PlayerBall.DefaultPlayerNoFoodRadius)
                        ball.Radius++;
                    FixedBallMgr.Remove(obj.Key);
                }
            }
            if(FoodRemoveFlag)
            {
                FixedBallMgr.Update();
            }
            return FoodRemoveFlag;
        }

        public static bool UpdatePlayer(uint id, ref PlayerBall ball)
        {
            bool PlayerDeadFlag = false;
            foreach(var obj in PlayerBallMgr.ToList())
            {
                if(obj.Key != id && CanEat(ball, obj.Value))
                {
                    PlayerDeadFlag = true;
                    ball.Radius += obj.Value.Radius;
                    PlayerBallMgr.Dead(obj.Key);
                }
            }
            return PlayerDeadFlag;
        }
    }
}
