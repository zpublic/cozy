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
        GameServer server = new GameServer();
        GameClient client = new GameClient();
        GamerServices gamerServices = null;
        PacketWriter writer = new PacketWriter();
        PacketReader reader = new PacketReader();

        private NetworkState.Status netState = NetworkState.Status.alone;

        public void Init(Game game)
        {
            gamerServices = new GamerServices();
            gamerServices.Init(game);
        }

        public bool CreateServer()
        {
            if (netState == NetworkState.Status.alone)
            {
                if (server.CreateServer())
                {
                    netState = NetworkState.Status.server;
                    return true;
                }
            }
            return false;
        }

        public bool FindServer()
        {
            if (netState == NetworkState.Status.alone)
            {
                if (client.FindServer())
                {
                    netState = NetworkState.Status.client;
                    return true;
                }
            }
            return false;
        }

        public void DisConnect()
        {
            switch (netState)
            {
                case NetworkState.Status.client:
                    if (client.mSession != null)
                    {
                        client.mSession.Dispose();
                        client.mSession = null;
                    }
                    break;
                case NetworkState.Status.server:
                    if (server.mSession != null)
                    {
                        server.mSession.Dispose();
                        server.mSession = null;
                    }
                    break;
            }
        }

        public void Update()
        {
            NetworkSession session = null;
            switch (netState)
            {
                case NetworkState.Status.client:
                    session = client.mSession;
                    break;
                case NetworkState.Status.server:
                    session = server.mSession;
                    break;
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
