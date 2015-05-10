using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Net;
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
        PacketWriter writer = new PacketWriter();
        PacketReader reader = new PacketReader();

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
            NetworkSession session = null;
            if (server != null)
            {
                session = server.mSession;
            }
            else if (client != null)
            {
                session = client.mSession;
            }

            if (session != null)
            {
                SendData(session);
                ReceiveData(session);
                session.Update();
            }
            gamerServices.Update();
        }

        private void ReceiveData(NetworkSession session)
        {
            foreach (LocalNetworkGamer localGamer in session.LocalGamers)
            {
                while (localGamer.IsDataAvailable)
                {
                    NetworkGamer sender;
                    localGamer.ReceiveData(reader, out sender);
                    DebugHelper.Print(reader.ReadString());
                }
            }
        }

        private int lastTime = 0;
        private Random r = new Random(DateTime.Now.Millisecond);

        private void SendData(NetworkSession session)
        {
            DateTime t = DateTime.Now;
            if (t.Second != lastTime)
            {
                lastTime = t.Second;

                foreach (LocalNetworkGamer localGamer in session.LocalGamers)
                {
                    writer.Write("hehe" + r.Next().ToString());
                    localGamer.SendData(writer, SendDataOptions.ReliableInOrder);
                }
            }
        }
    }
}
