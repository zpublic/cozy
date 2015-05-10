using Microsoft.Xna.Framework.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.Network
{
    public class GameClient
    {
        private NetworkSession mSession = null;
        public bool FindServer()
        {
            AvailableNetworkSessionCollection sessions = NetworkSession.Find(NetworkSessionType.SystemLink, 4, null);
            if (sessions.Count > 0)
            {
                mSession = NetworkSession.Join(sessions[0]);
                mSession.GamerJoined += GamerJoined;
                mSession.GamerLeft += GamerLeft;
                DebugHelper.Print("CurrentGamerCount:" + sessions[0].CurrentGamerCount.ToString());
                return true;
            }
            return false;
        }

        public void Update()
        {
            mSession.Update();
        }

        void GamerJoined(object sender, GamerJoinedEventArgs e)
        {
            DebugHelper.Print("GameClient GamerJoined");
        }

        void GamerLeft(object sender, GamerLeftEventArgs e)
        {
            DebugHelper.Print("GameClient GamerLeft");
        }
    }
}
