using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Net;

namespace CozyKxlol.Network
{
    class GameServer
    {
        public NetworkSession mSession = null;
        public bool CreateServer()
        {
            try
            {
                mSession = NetworkSession.Create(NetworkSessionType.SystemLink, 4, 16);
                mSession.AllowHostMigration = false;
                mSession.AllowJoinInProgress = false;
                mSession.GamerJoined += GamerJoined;
                mSession.GamerLeft += GamerLeft;
            }
            catch (System.Exception ex)
            {
                DebugHelper.Print(ex.StackTrace.ToString());
                return false;
            }
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
