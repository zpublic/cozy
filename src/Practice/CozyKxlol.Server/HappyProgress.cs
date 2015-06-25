using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;

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

        private static void OnHappyServerProgerss()
        {
            NetPeerConfiguration config = new NetPeerConfiguration("CozyKxlol");
            config.MaximumConnections = 10000;
            config.Port = 36048;

            HappyServer = new NetServer(config);
            HappyServer.Start();
        }
    }
}
