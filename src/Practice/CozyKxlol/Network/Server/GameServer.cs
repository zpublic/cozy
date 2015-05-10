using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Net;

namespace CozyKxlol.Network.Server
{
    public class GameServer
    {
        private NetworkSession mSession = null;
        public bool CreateServer()
        {
            mSession = NetworkSession.Create(NetworkSessionType.SystemLink, 1, 4);
            mSession.AllowHostMigration = true;
            mSession.AllowJoinInProgress = false;
            mSession.GamerJoined += GamerJoined;
            mSession.GamerLeft += GamerLeft;
            return true;
        }

        void GamerJoined(object sender, GamerJoinedEventArgs e)
        {
            DebugHelper.Print("GamerJoined");
        }

        void GamerLeft(object sender, GamerLeftEventArgs e)
        {
            DebugHelper.Print("GamerLeft");
        }
    }
}
