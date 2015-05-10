using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Net;

namespace CozyKxlol.Network
{
    public class GameServer
    {
        public NetworkSession mSession = null;
        public bool CreateServer()
        {
            mSession = NetworkSession.Create(NetworkSessionType.SystemLink, 4, 16);
            mSession.AllowHostMigration = false;
            mSession.AllowJoinInProgress = false;
            mSession.GamerJoined += GamerJoined;
            mSession.GamerLeft += GamerLeft;
            return true;
        }

        void GamerJoined(object sender, GamerJoinedEventArgs e)
        {

            DebugHelper.Print("GameServer GamerJoined");
        }

        void GamerLeft(object sender, GamerLeftEventArgs e)
        {
            DebugHelper.Print("GameServer GamerLeft");
        }
    }
}
