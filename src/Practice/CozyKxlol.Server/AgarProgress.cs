using CozyKxlol.Network.Msg;
using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyKxlol.Server.Manager;

namespace CozyKxlol.Server
{
    public partial class Program
    {
        private static uint _AgarGameId = 1;
        public static uint AgarGameId
        {
            get
            {
                return _AgarGameId++;
            }
        }

        public static NetServer AgarServer { get; set; }

        public static FixedBallManager FixedBallMgr = new FixedBallManager();
        public static PlayerBallManager PlayerBallMgr = new PlayerBallManager();
        public static Dictionary<NetConnection, uint> ConnectionMgr = new Dictionary<NetConnection, uint>();
        public static MarkManager MarkMgr = new MarkManager();
        public const int GameWidth = 800;
        public const int GameHeight = 610;

        private static void RegisterAgarMessage()
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

        private static void OnAgarServerProgerss()
        {
            NetPeerConfiguration config = new NetPeerConfiguration("CozyKxlol");
            config.MaximumConnections = 10000;
            config.Port = 48360;

            AgarServer = new NetServer(config);
            AgarServer.Start();

            RegisterAgarMessage();

            FixedBallMgr.Update();
        }
    }
}
