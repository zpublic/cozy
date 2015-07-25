using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkRelayServer;

namespace CozyAnywhere.RelayServerCore
{
    public partial class AnywhereRelayServer
    {
        public ServerRelay server { get; set; }

        public AnywhereRelayServer(int MaxConnections, int Port)
        {
            InitNetwork(MaxConnections, Port);
        }

        private void InitNetwork(int MaxConnections, int Port)
        {
            server = new ServerRelay(MaxConnections, Port);
            InitMessage();
            InitEvent();
        }

        public void Listen()
        {
            if (server != null)
            {
                server.Listen();
            }
        }

        public void Shutdown()
        {
            if (server != null)
            {
                server.Shutdown();
            }
        }

        public void EnterMainLoop()
        {
            if (server != null)
            {
                server.EnterMainLoop();
            }
        }

        public void Update()
        {
            if (server != null)
            {
                server.RecivePacket();
            }
        }
    }
}
