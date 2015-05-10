using Microsoft.Xna.Framework.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.Network.Client
{
    public class GameClient
    {
        private NetworkSession mSession = null;
        public bool FindServer()
        {
            AvailableNetworkSessionCollection sessions = NetworkSession.Find(NetworkSessionType.SystemLink, 1, null);
            if (sessions.Count > 0)
            {
                mSession = NetworkSession.Join(sessions[0]);
                mSession.GamerJoined += GamerJoined;
                mSession.GamerLeft += GamerLeft;
                return true;
            }
            return false;
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
