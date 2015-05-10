using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.Network
{
    public class NetworkHelper
    {
        GameServer server = null;
        GameClient client = null;
        GamerServices gamerServices = null;

        public void Init(Game game)
        {
            gamerServices = new GamerServices();
            gamerServices.Init(game);

            client = new GameClient();
            if (client.FindServer())
            {
                DebugHelper.Print("FindServer");
            }
            else
            {
                server = new GameServer();
                server.CreateServer();
            }
        }

        public void Update()
        {
            if (server != null)
            {
                server.Update();
            }
            else if (client != null)
            {
                client.Update();
            }

            gamerServices.Update();
        }
    }
}
