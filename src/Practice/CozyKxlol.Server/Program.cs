using CozyKxlol.Network.Msg;
using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CozyKxlol.Server.Manager;

namespace CozyKxlol.Server
{
    public partial class Program
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

        public static NetServer server { get; set; }

        public static FixedBallManager FixedBallMgr                 = new FixedBallManager();
        public static PlayerBallManager PlayerBallMgr               = new PlayerBallManager();
        public static Dictionary<NetConnection, uint> ConnectionMgr = new Dictionary<NetConnection, uint>();
        public static MarkManager MarkMgr                           = new MarkManager();
        public const int GameWidth                                  = 800;
        public const int GameHeight                                 = 610;

        private static void RegisterMessage()
        {
            // 推送FixedBall的增加
            FixedBallMgr.FixedCreateMessage += new EventHandler<FixedBallManager.FixedCreateArgs>(OnFixedCreate);
            // 推送FixedBall的移除
            FixedBallMgr.FixedRemoveMessage += new EventHandler<FixedBallManager.FixedRemoveArgs>(OnFixedRemove);
            // 用户断开链接
            PlayerBallMgr.PlayerExitMessage += new EventHandler<PlayerBallManager.PlayerExitArgs>(OnPlayerExit);

            PlayerBallMgr.PlayerDeadMessage += new EventHandler<PlayerBallManager.PlayerDeadArgs>(OnPlayerDead);

            MarkMgr.MarkChangedMessage += new EventHandler<MarkManager.MarkChangedArgs>(OnMarkChange);
        }

        static void Main(string[] args)
        {
            NetPeerConfiguration config = new NetPeerConfiguration("CozyKxlol");
            config.MaximumConnections   = 10000;
            config.Port                 = 48360;

            server = new NetServer(config);
            server.Start();

            RegisterMessage();

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
                            else if (status == NetConnectionStatus.Disconnected)
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
    }
}
