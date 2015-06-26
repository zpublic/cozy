using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;
using CozyKxlol.Server.Manager;

namespace CozyKxlol.Server
{
    public partial class Program
    {
        private static uint _HappyGameId = 1;
        public static uint HappyGameId
        {
            get
            {
                return _HappyGameId++;
            }
        }

        public static NetServer HappyServer { get; set; }

        public static HappyPlayerManager HappyPlayerMgr = new HappyPlayerManager();
        public static ConnectionManager HappyConnMgr = new ConnectionManager();

        private static void OnHappyServerProgerss()
        {
            NetPeerConfiguration config = new NetPeerConfiguration("CozyKxlol");
            config.MaximumConnections = 10000;
            config.Port = 36048;

            HappyServer = new NetServer(config);
            HappyServer.Start();

            RegisterHappyMessage();
        }

        private static void RegisterHappyMessage()
        {
            HappyPlayerMgr.HappyPlayerQuitMessage += new EventHandler<HappyPlayerManager.HappyPlayerQuitArgs>(OnHappyPlayerQuit);
        }
    }
}
